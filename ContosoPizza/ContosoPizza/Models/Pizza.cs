//Adding a Data Store
//Before you start to implement a web API for pizza, you need to have a data store on which you can perform operations.
//You need a model class to represent a pizza in inventory.
//The model contains properties that represent the characteristics of a pizza.
//The model is used to pass data in the web API and to persist pizza options in the data store.
//In this unit, that data store is a simple local in-memory caching service.
//In a real-world application, you would consider using a database, such as SQL Server, with Entity Framework Core.


using System;

namespace ContosoPizza.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Pizza
{
	//public Pizza()
	//{
	//	//
	//	// TODO: Add constructor logic here
	//	//
	//}

	public int Id { get; set; }	
	public string Name { get; set; }
	public bool IsGlutenFree { get; set; }

	

}
