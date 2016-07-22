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
	public StructureCategory category;
	public int cost;
	public int influence;
	public Resources resourceCost;

	public CoreResource directEffect;

	public Resources inputs;
	public Resources outputs;

	public StructureType ()
	{
		this.name = "Untitled";
		this.descriptions = "";
		this.cost = 0;
		this.influence = 0;
		this.resourceCost = new Resources ();
		this.directEffect = new CoreResource ();
		this.inputs = new Resources ();
		this.outputs = new Resources ();

	}

	public static Boolean IsValid (StructureType _structureType)
	{

		if (_structureType.name == null || _structureType.name == "Untitled")
			return false;
		if (_structureType.directEffect == null)
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
	
}

public enum StructureCategory
{
	Core = 0,
	Farming = 1,
	Production = 2,
	Specialists = 3
}
#endregion