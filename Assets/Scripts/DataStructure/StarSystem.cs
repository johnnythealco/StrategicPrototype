using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


#region Star System Class

[System.Serializable]
public class StarSystem : Asset
{
	public List<Planet> planets;
	public List<RegionType> localRegions;
}
#endregion

#region Planet Class
[System.Serializable]
public class Planet : Asset
{
	
	public string classification;
	public List<RegionType> regions;



}
#endregion

#region Region Class
[System.Serializable]
public class RegionType : Asset
{
	public int cost;

	public List<string> availableStructures;
	public List<string> defaultStructures;
	public List<string> availableResources;
	public List<string> availableUpgrades;

	public RegionType ()
	{
		this.name = "Untitled";
		this.descriptions = "";
		availableStructures = new List<string> ();
		defaultStructures = new List<string> ();
		availableResources = new List<string> ();
		availableUpgrades = new List<string> ();

	}
	
}
#endregion

#region Structure Class
[System.Serializable]
public class StructureType : Asset
{
	public StructureCategory Category;
	public Resources resourceCost;

	public Resources inputs;
	public Resources outputs;

	public StructureType ()
	{
		this.name = "Untitled";
		this.descriptions = "";
		this.resourceCost = new Resources ();
		this.inputs = new Resources ();
		this.outputs = new Resources ();

	}

	public static Boolean IsValid (StructureType _structureType)
	{

		if (_structureType.name == null || _structureType.name == "Untitled")
			return false;
		if (_structureType.inputs == null)
			return false;
		if (_structureType.outputs == null)
			return false;
		else
			return true;


	}

	public static List<string> getCategories ()
	{
		return Enum.GetNames (typeof(StructureCategory)).ToList ();
	}

	public static List<string> GetNames (List<StructureType> _structureTypeList)
	{
		var result = new List<string> ();

		foreach (var item  in _structureTypeList)
		{			
			result.Add (item.name);
		}
		return result;
	}

	public static List<StructureType> FilterListByCategory (List<StructureType> _structureTypeList, StructureCategory _category)
	{
		var result = new List<StructureType> ();

		foreach (var item  in _structureTypeList)
		{
			if (item.Category == _category)
				result.Add (item);

		}
		return result;
	}

	public static List<StructureType>  SearchList (List<StructureType> _List, string _keyword)
	{
		var result = new List<StructureType> ();

		var resultList = GetNames (_List).FindAll (delegate(string s)
		{
			return s.Contains (_keyword);
		});

		foreach (var name in resultList)
		{
			foreach (var structure in _List)
			{
				if (structure.name == name)
					result.Add (structure);
			}
		}

		return result;


	}
	
}

public enum StructureCategory
{
	Core = 0,
	Farming = 1,
	Production = 2,
	Specialists = 3
}
#endregion