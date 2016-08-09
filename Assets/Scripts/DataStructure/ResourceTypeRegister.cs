using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceTypeRegister : ScriptableObject
{
	public List<ResourceType> MasterList = new List<ResourceType> ();

	public List<ResourceType> StrategicResources { get { return getResourceListByCategory (ResourceCategory.Strategic); } }

	List<ResourceType> getResourceListByCategory (ResourceCategory category)
	{
		List<ResourceType> result = new List<ResourceType> ();

		foreach (var item in MasterList)
		{
			if (item.Category == category)
			{
				result.Add (item);
			}
		}

		return result;
	}

	public ResourceType getResourceType (string _name)
	{


		foreach (var item in MasterList)
		{
			if (item.name == _name)
			{
				return item;
			}
		}

		return null;
	}

	public List<ResourceType> getResourceTypes (List<string> _resourceNames)
	{
		var result = new List<ResourceType> ();
		foreach (var item in _resourceNames)
		{
			var _resourceType = getResourceType (item);
			if (_resourceType != null)
				result.Add (_resourceType);
		}
		return result;
	}

}