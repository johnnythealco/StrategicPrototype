using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public enum ResourceCategory
{
	Strategic = 0,
	Population = 1,
	Nutrition = 2,
	Shelter = 3,
	Comfort = 4
}

[System.Serializable]
public class ResourceType : Asset
{

	public ResourceCategory Category;
	public int level;

	public ResourceType ()
	{
		this.name = "";
		this.descriptions = "";
		this.Category = ResourceCategory.Strategic;
		this.level = 0;
	}

<<<<<<< HEAD
=======


>>>>>>> origin/master
	public static List<string> getCategories ()
	{
		return Enum.GetNames (typeof(ResourceCategory)).ToList ();
	}

	public static List<string> getLevels ()
	{
		var result = new List<string> ();
		for (int i = 0; i < 8; i++)
		{
			result.Add ("Level " + i.ToString ());
		}

		return result;
	}

<<<<<<< HEAD
	public static List<string> GetNames (List<ResourceType> _resourceTypeList)
	{
		var result = new List<string> ();

		foreach (var item  in _resourceTypeList)
		{			
			result.Add (item.name);
		}
		return result;
	}

=======
>>>>>>> origin/master
	public static List<ResourceType> FilterListByCategory (List<ResourceType> _resourceTypeList, ResourceCategory _category)
	{
		var result = new List<ResourceType> ();

		foreach (var item  in _resourceTypeList)
		{
			if (item.Category == _category)
				result.Add (item);

		}
		return result;
	}

	public static List<ResourceType> FilterListByLevel (List<ResourceType> _resourceTypeList, int _level)
	{
		var result = new List<ResourceType> ();

		foreach (var item  in _resourceTypeList)
		{
			if (item.level == _level)
				result.Add (item);

		}
		return result;
	}
<<<<<<< HEAD

	public static List<ResourceType>  SearchList (List<ResourceType> _List, string _keyword)
	{
		var result = new List<ResourceType> ();
	
		var resultList = GetNames (_List).FindAll (delegate(string s)
		{
			return s.Contains (_keyword);
		});

		foreach (var name in resultList)
		{
			foreach (var resource in _List)
			{
				if (resource.name == name)
					result.Add (resource);
			}
		}

		return result;
	
	
	}
=======
>>>>>>> origin/master
}

[System.Serializable]
public class Resource
{
	//Resouce is used to lookup the Register so it needs to match
	public string resource;
	public int value;

	public Resource (string _resource, int _value)
	{
		this.resource = _resource;
		this.value = _value;
	}

	public ResourceType type{ get { return Game.Manager.register.resourceTypeRegister.getResourceType (resource); } }


	public override string ToString ()
	{
		var _name = resource + ": ";

		var _value = value.ToString ();

		return string.Format (_name + _value);
	}
}

[System.Serializable]
public class Resources : System.Object
{
	public List<Resource> list = new List<Resource> ();

	public Resources (List<Resource> _resources)
	{
		this.list = _resources;
	}

	public Resources ()
	{
		this.list = new List<Resource> ();
	}


	public void Add (string _resource, int quantity)
	{

		foreach (var listItem in list)
		{
			if (listItem.resource == _resource)
			{
				var number = listItem.value;
				number = number + quantity;
				return;
			} 
		}

		list.Add (new Resource (_resource, quantity));


	}

	public void Add (Resources _resources)
	{
		foreach (var newitem in _resources.list)
		{
			var newResource = newitem.resource;
			var newValue = newitem.value;

			foreach (var resource in this.list)
			{
				//If the resource is in the list update the value.
				if (resource.resource == newResource)
				{
					var value = resource.value;
					value = value + newValue;
				} 
				//If the resouce is not in the list add it.
				else
				{
					list.Add (new Resource (newResource, newValue));
				}
	
	
			}
	
		}


	}


	public override string ToString ()
	{
		string output = "";
		foreach (var item in list)
		{
			output = output + item.ToString () + System.Environment.NewLine;
		}
		return output;
	}



}



