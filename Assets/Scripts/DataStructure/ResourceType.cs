using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public enum ResourceCategory
{
	Strategic = 0,
	BasicGoods = 1,
	LuxuryGoods = 2,
	SpecialistsGoods = 3
}

[System.Serializable]
public class ResourceType : System.Object
{
	public string name;
	public Sprite icon;
	public ResourceCategory Category;
	public float baseprice;
	public int prosperity;
	public int health;


	public static List<string> getCategories ()
	{
		return Enum.GetNames (typeof(ResourceCategory)).ToList ();
	}
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



