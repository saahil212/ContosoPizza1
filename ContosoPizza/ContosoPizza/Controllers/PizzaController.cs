
//A controller is a public class with one or more public methods known as actions.By convention,
//a controller is placed in the project root's Controllers directory.
//The actions are exposed as HTTP endpoints inside the web API controller.

//Don't create a web API controller by deriving from the Controller class.
//Controller derives from ControllerBase and adds support for views,
//so it's for handling webpages, not web API requests.

///this class derives from ControllerBase, the base class for working with HTTP requests in ASP.NET Core.
///It also includes the two standard attributes you've learned about, [ApiController] and [Route]. 
///As before, the [Route] attribute defines a mapping to the [controller] token. 
///Because this controller class is named PizzaController, this controller handles requests 
///to https://localhost:{PORT}/pizza.
///[ApiController] enables opinionated behaviors that make it easier to build web APIs. 
///Some behaviors include parameter source inference, attribute routing as a requirement, 
///and model validation error-handling enhancements.
//[Route] defines the routing pattern [controller].
//The[controller] token is replaced by the controller's name (case-insensitive,
//without the Controller suffix). This controller handles requests to https://localhost:{PORT}/weatherforecast.


using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    //ControllerBase has utility methods that will create the appropriate HTTP response codes and messages for
    public class PizzaController :ControllerBase
    {
        public PizzaController()
        {

        }

        // GET all action
        //this method Responds only to the HTTP GET verb, as denoted by the[HttpGet] attribute.
        //Queries the service for all pizza and automatically returns data with a Content-Type value of application/json.
        //when you use ActionResult you can return only predefined ones for returning a View or a resource

        [HttpGet]
        //public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();
        // OR write method using return inside { } as below
        public ActionResult<List<Pizza>> GetAll()
        {
            return PizzaService.GetAll();
        }

        // GET by Id actions
        // This method Responds only to the HTTP GET verb, as denoted by the[HttpGet] attribute.
        //Requires that the id parameter's value is included in the URL segment after pizza/. Remember, the controller-level [Route] attribute defined the /pizza pattern.
        //Queries the database for a pizza that matches the provided id parameter.
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }



        // POST action
        //To enable users to add a new item to the endpoint, you must implement the POST action by using the [HttpPost] attribute.
        //When you pass the item(in this example, a pizza) into the method as a parameter,
        //ASP.NET Core will automatically convert any application/JSON that's sent to the endpoint into a populated .NET Pizza object.

        //The[HttpPost] attribute will map HTTP POST requests sent to http://localhost:5000/pizza by using the Create() method.
        //Instead of returning a list of pizzas, as we saw with the Get() method, this method returns an IActionResult response.
        
        //IActionResult is an interface, we can create a custom response as a return,
        //ActionResult is an implementation of IActionResult Interface

        //IActionResult lets the client know if the request succeeded and provides the ID of the newly created pizza.
        //IActionResult does this by using standard HTTP status codes, so it can easily integrate with clients regardless of the language or platform they're running on.
        
        //Because the controller is annotated with the [ApiController] attribute, it's implied that the Pizza parameter will be found in the request body.

        [HttpPost]
        // This code will save the pizza and return a result
        public IActionResult Create(Pizza pizza)
        {
            //The first parameter in the CreatedAtAction method call represents an action name.
            //The nameof keyword is used to avoid hard-coding the action name.
            //CreatedAtAction uses the action name to generate a location HTTP response header with a URL to the newly created pizza,
            // eg Output is as follows

            // HTTP / 1.1 201 Created
            // Content - Type: application / json; charset = utf - 8
            // Date: Fri, 02 Apr 2021 23:23:09 GMT
            // Location: https://localhost:{PORT}/Pizza?id=3
            // Server: Kestrel
            // Transfer - Encoding: chunked

           PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create ),new { id = pizza.Id },pizza );
        }



        // PUT action
        [HttpPut("{id}")]
       // The preceding action:
        //Responds only to the HTTP PUT verb, as denoted by the[HttpPut] attribute.
        //Requires that the id parameter's value is included in the URL segment after pizza/.
        //Returns IActionResult because the ActionResult return type isn't known until runtime.
        //The BadRequest, NotFound, and NoContent methods return BadRequestResult, NotFoundResult, and NoContentResult types, respectively.
        
        //IActionResult can be used to return any type of Results
        //ActionResult can be used to return only one type 
        public IActionResult Update(int id, Pizza pizza)
        {
            // This code will update the pizza and return a result
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);
            return NoContent();
        }


        // DELETE action

        [HttpDelete("{id}")]
        // The preceding action:
        //Responds only to the HTTP DELETE verb, as denoted by the[HttpDelete] attribute.
        //Requires that the id parameter's value is included in the URL segment after pizza/.
        //Returns IActionResult because the ActionResult return type isn't known until runtime.
        //The NotFound and NoContent methods return NotFoundResult and NoContentResult types, respectively.
        //Queries the in-memory cache for a pizza that matches the provided id parameter.
        public IActionResult Delete(int id)
        {
            // This code will delete the pizza and return a result
            var pizza = PizzaService.Get(id);
            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);
            return NoContent();

        }
    }








}
