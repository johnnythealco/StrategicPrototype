using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Register : ScriptableObject
{
	public  ResourceTypeRegister resourceTypeRegister;
	public  StructureRegister structureRegister;
	public RegionTypeRegister regionTypeRegister;

	public List<ResourceType> AllResources{ get { return resourceTypeRegister.MasterList; } }

	public List<ResourceType> StrategicResources{ get { return resourceTypeRegister.StrategicResources; } }

	#region utility methods

	public static List<string> getAssetNames (List<RegionType> _regions)
	{
		var result = new List<string> ();
		foreach (var item in _regions)
		{
			if (item.name != null)
				result.Add (item.name);
		}
		return result;
	}

	public static List<string> getAssetNames (List<ResourceType> _resources)
	{
		var result = new List<string> ();
		foreach (var item in _resources)
		{
			if (item.name != null)
				result.Add (item.name);
		}
		return result;

	}

	public static List<string> getAssetNames (List<StructureType> _Structures)
	{
		var result = new List<string> ();
		foreach (var item in _Structures)
		{
			if (item.name != null)
				result.Add (item.name);
		}
		return result;

	}

	#endregion
}

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

public class StructureRegister : ScriptableObject
{
	public Sprite defaultIcon;
	
	public List<StructureType> MasterList = new List<StructureType> ();

	public StructureType getStructureType (string _name)
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

	public List<StructureType> getStructureTypes (List<string> _structureNames)
	{
		var result = new List<StructureType> ();
		foreach (var item in _structureNames)
		{
			var _structureType = getStructureType (item);
			if (_structureType != null)
				result.Add (_structureType);
		}
		return result;
	}

	void OnEnable ()
	{
		if (defaultIcon == null)
			return;
		
		foreach (var item in MasterList)
		{
			if (item.smallImage == null)
			{
				item.smallImage = defaultIcon;
			}

			if (item.largeImage == null)
			{
				item.largeImage = defaultIcon;
			}
		}
		
	}
 

}

[System.Serializable]
public class RegionTypeRegister : ScriptableObject
{
	public Sprite defaultIcon;

	[SerializeField]
	public List<RegionType> MasterList = new List<RegionType> ();

	public RegionType getRegionType (string _name)
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

	public List<RegionType> getRegionTypes (List<string> _regionNames)
	{
		var result = new List<RegionType> ();
		foreach (var item in _regionNames)
		{
			var _regionType = getRegionType (item);
			if (_regionType != null)
				result.Add (_regionType);
		}
		return result;
	}

	void OnEnable ()
	{
		if (defaultIcon == null)
			return;

		foreach (var item in MasterList)
		{
			if (item.smallImage == null)
			{
				item.smallImage = defaultIcon;
			}

			if (item.largeImage == null)
			{
				item.largeImage = defaultIcon;
			}
		}

	}




}


