using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using JK.GameData;


namespace JK
{
	namespace Controller
	{


		public class RegionTypeEditor : MonoBehaviour
		{
			protected RegionType regiontype;
			public Register register;
			public InputField displaynameInput;
			public InputField descriptionInput;
			public Dropdown categoryInput;
			public Dropdown sizeInput;
			public ResourceTypeListBuilder avaiableResourcesBuilder;
			public StructureTypeListBuilder availableStructuresBuilder;
			public StructureTypeListBuilder defaultStructureBuilder;
			public RegionTypeListBuilder upgradesBuilder;


			void Start ()
			{
				if (categoryInput != null)
				{
					categoryInput.ClearOptions ();
					categoryInput.AddOptions (RegionType.getCategories ());
				}
			}

			public void Prime (RegionType _regionType)
			{
				regiontype = _regionType; 

				if (displaynameInput != null)
					displaynameInput.text = regiontype.name;
				if (descriptionInput != null)
					descriptionInput.text = regiontype.descriptions;
				if (categoryInput != null)
					categoryInput.value = (int)regiontype.Category;
				if (sizeInput != null)
					sizeInput.value = regiontype.size;
		

				if (avaiableResourcesBuilder != null)
				{
					var availableResourcesTypes = register.resourceTypeRegister.getResourceTypes (regiontype.availableResources);
					avaiableResourcesBuilder.Prime (availableResourcesTypes); 
					avaiableResourcesBuilder.onListUpdate += AvaiableResourcesUpdate;
				}
				if (availableStructuresBuilder != null)
				{
					var availableStructures = register.structureRegister.getStructureTypes (regiontype.availableStructures);
					availableStructuresBuilder.Prime (availableStructures);
					availableStructuresBuilder.onListUpdate += AvailableStructuresUpdate;
				}
				if (defaultStructureBuilder != null)
				{
					var defaultStructures = register.structureRegister.getStructureTypes (regiontype.defaultStructures);
					defaultStructureBuilder.Prime (defaultStructures);
					defaultStructureBuilder.onListUpdate += DefaultStructureUpdate;
				}

				if (upgradesBuilder != null)
				{
					var upgradess = register.regionTypeRegister.getRegionTypes (regiontype.availableUpgrades);
					upgradesBuilder.Prime (upgradess);
					upgradesBuilder.onListUpdate += UpgradesUpdate;
				}

			}

			void UpgradesUpdate (List<RegionType> _regionTypes)
			{
				if (_regionTypes != null)
					regiontype.availableUpgrades = Register.getAssetNames (_regionTypes);
			}

			public void getInput ()
			{
				if (descriptionInput.text != null)
					regiontype.descriptions = descriptionInput.text;

				if (categoryInput != null)
					regiontype.Category = (RegionCategory)categoryInput.value;

				if (sizeInput != null)
					regiontype.size = sizeInput.value;
		
			}


			public void GetnameInput ()
			{
				if (displaynameInput != null)
				{
					if (displaynameInput.text != regiontype.name)
					{
						var originalName = regiontype.name;
						var newName = displaynameInput.text;
						Game.Manager.register.RenameRegion (originalName, newName);
						regiontype.name = displaynameInput.text;
					}
				}
			}


			void DefaultStructureUpdate (List<StructureType> _structureTypes)
			{
				if (_structureTypes != null)
					regiontype.defaultStructures = Register.getAssetNames (_structureTypes); 
			}

			void AvailableStructuresUpdate (List<StructureType> _structureTypes)
			{
				if (_structureTypes != null)
					regiontype.availableStructures = Register.getAssetNames (_structureTypes); 
			}

			void AvaiableResourcesUpdate (List<ResourceType> _resourceTypes)
			{
				if (_resourceTypes != null)
					regiontype.availableResources = Register.getAssetNames (_resourceTypes);
		
			}

			public void destroy ()
			{
				Destroy (gameObject);
			}

			void unsubscribe ()
			{
				avaiableResourcesBuilder.onListUpdate -= AvaiableResourcesUpdate;
				availableStructuresBuilder.onListUpdate -= AvailableStructuresUpdate;
				defaultStructureBuilder.onListUpdate -= DefaultStructureUpdate;
				upgradesBuilder.onListUpdate -= UpgradesUpdate;
			}

			void OnDestroy ()
			{
				unsubscribe ();
			}

		}
	}
}

