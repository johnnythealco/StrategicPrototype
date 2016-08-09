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






