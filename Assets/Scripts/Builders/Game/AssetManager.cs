using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class AssetManager : MonoBehaviour
{
	public Register register;

	public StructureTypeListEditor structureList;
	public RegionTypeListEditor regionList;
	public ResourceTypeListEditor resourceList;

	void Start ()
	{

	}

	public void PrimeRegions ()
	{
		var regions = register.regionTypeRegister.MasterList;
		regionList.Prime (regions);
		regionList.gameObject.SetActive (true);
		structureList.gameObject.SetActive (false);
		resourceList.gameObject.SetActive (false);

	}

	public void PrimeResources ()
	{
		var resources = register.resourceTypeRegister.MasterList; 
		resourceList.Prime (resources);
		regionList.gameObject.SetActive (false);
		structureList.gameObject.SetActive (false);
		resourceList.gameObject.SetActive (true);
	}

	public void PrimeStructures ()
	{
		var structures = register.structureRegister.MasterList;
		structureList.Prime (structures);
		regionList.gameObject.SetActive (false);
		structureList.gameObject.SetActive (true);
		resourceList.gameObject.SetActive (false);
	}

	public void NewResource ()
	{
		PrimeResources ();
		resourceList.CreateResourceType ();
	}

	public void NewStructure ()
	{
		PrimeStructures ();
		structureList.CreateStructureType ();
	}

	public void NewRegion ()
	{
		PrimeRegions ();
		regionList.CreateRegionType ();
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public void Save ()
	{
		var SaveData = new AssetData ();
		SaveData.ResourceList = register.resourceTypeRegister.MasterList;
		SaveData.RegionList = register.regionTypeRegister.MasterList;
		SaveData.StructureList = register.structureRegister.MasterList;

		var JSON = JsonUtility.ToJson (SaveData, true);
		File.WriteAllText (Application.dataPath + "/JSON/AssetData.JSON", JSON);
	}

	public void Load ()
	{
		if (File.Exists (Application.dataPath + "/JSON/AssetData.JSON"))
		{
			var JSON = File.ReadAllText (Application.dataPath + "/JSON/AssetData.JSON");
			var SaveData = JsonUtility.FromJson < AssetData > (JSON);

			register.resourceTypeRegister.MasterList = SaveData.ResourceList;
			register.regionTypeRegister.MasterList = SaveData.RegionList;
			register.structureRegister.MasterList = SaveData.StructureList;

		}
	}

}
