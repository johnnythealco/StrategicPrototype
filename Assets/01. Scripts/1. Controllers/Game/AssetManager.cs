﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using JK.GameData;
using JK.View;

namespace JK
{
	namespace Controller
	{

		public class AssetManager : MonoBehaviour
		{
			public Register register;

			public StructureTypeListEditor structureList;
			public RegionTypeListEditor regionList;
			public ResourceTypeListEditor resourceList;

			public Transform fileMenu;
			public Transform resourceMenu;
			public Transform structureMenu;
			public Transform regionMenu;
			public CanvasGroup canvasGroup;

			Transform activeMenu;


			void Start ()
			{
				fileMenu.gameObject.SetActive (false);
				resourceMenu.gameObject.SetActive (false);
				structureMenu.gameObject.SetActive (false);
				regionMenu.gameObject.SetActive (false);
				canvasGroup.blocksRaycasts = false;
			}


			public void toggleFileMenu ()
			{
				if (fileMenu.gameObject.activeInHierarchy)
				{
					fileMenu.gameObject.SetActive (false);
					activeMenu = null;
					canvasGroup.blocksRaycasts = false;
				} else
				{
					hideActiveMenu ();
					fileMenu.gameObject.SetActive (true);
					activeMenu = fileMenu;
					canvasGroup.blocksRaycasts = true;
				}
			}

			public void toggleResourceMenu ()
			{
				if (resourceMenu.gameObject.activeInHierarchy)
				{
					resourceMenu.gameObject.SetActive (false);
					activeMenu = null;
					canvasGroup.blocksRaycasts = false;
				} else
				{
					hideActiveMenu ();
					resourceMenu.gameObject.SetActive (true);
					activeMenu = resourceMenu;
					canvasGroup.blocksRaycasts = true;
				}
			}

			public void hideActiveMenu ()
			{
				if (activeMenu != null)
				{
					activeMenu.gameObject.SetActive (false);
					activeMenu = null;
					canvasGroup.blocksRaycasts = false;
				}
			}

			public void toggleStructureMenu ()
			{
				if (structureMenu.gameObject.activeInHierarchy)
				{
					structureMenu.gameObject.SetActive (false);
					activeMenu = null;
					canvasGroup.blocksRaycasts = false;
				} else
				{
					hideActiveMenu ();
					structureMenu.gameObject.SetActive (true);
					activeMenu = structureMenu;
					canvasGroup.blocksRaycasts = true;
				}
			}

			public void toggleRegionMenu ()
			{
				if (regionMenu.gameObject.activeInHierarchy)
				{
					regionMenu.gameObject.SetActive (false);
					activeMenu = null;
					canvasGroup.blocksRaycasts = false;
				} else
				{
					hideActiveMenu ();
					regionMenu.gameObject.SetActive (true);
					activeMenu = regionMenu;
					canvasGroup.blocksRaycasts = true;
				}
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

				var saveDiag = new System.Windows.Forms.SaveFileDialog ();
				saveDiag.FileName = "Export GameData.json";
				saveDiag.Filter = "json files (*.json|*.json|All files(*.*)|*.*";
				saveDiag.FilterIndex = 1;
				saveDiag.InitialDirectory = Application.dataPath + "/JSON/";

				saveDiag.ShowDialog ();

				if (saveDiag.FileName != "")
				{
					System.IO.StreamWriter writer = new System.IO.StreamWriter (saveDiag.OpenFile ());
					writer.WriteLine (JSON);
					writer.Dispose ();
					writer.Close ();
				}
			}

			public void Load ()
			{
				var loadDiag = new System.Windows.Forms.OpenFileDialog ();
				loadDiag.Filter = "json files (*.json|*.json|All files(*.*)|*.*";
				loadDiag.FilterIndex = 1;
				loadDiag.InitialDirectory = Application.dataPath + "/JSON/";

				loadDiag.ShowDialog ();

				if (loadDiag.FileName != "")
				{
					System.IO.StreamReader reader = new System.IO.StreamReader (loadDiag.OpenFile ());
					var JSON = reader.ReadToEnd ();
					reader.Dispose ();
					reader.Close ();
		

					var SaveData = JsonUtility.FromJson < AssetData > (JSON);

					register.resourceTypeRegister.MasterList = SaveData.ResourceList;
					register.regionTypeRegister.MasterList = SaveData.RegionList;
					register.structureRegister.MasterList = SaveData.StructureList;
				}

			}

		}
	}
}
