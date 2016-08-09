using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetData
{
	public List<ResourceType> ResourceList;
	public List<StructureType> StructureList;
	public List<RegionType> RegionList;

	public AssetData ()
	{
		ResourceList = new List<ResourceType> ();
		StructureList = new List<StructureType> ();
		RegionList = new List<RegionType> ();
	}



}
