using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace JK
{
	namespace GameData
	{


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


			public void RenameResource (string originalName, string newName)
			{
				var structures = structureRegister.MasterList;
				var regions = regionTypeRegister.MasterList;

				foreach (var structure in structures)
				{
					foreach (var resource in structure.inputs.list)
					{
						if (resource.resource == originalName)
							resource.resource = newName;
					}

					foreach (var resource in structure.outputs.list)
					{
						if (resource.resource == originalName)
							resource.resource = newName;
					}

					foreach (var resource in structure.resourceCost.list)
					{
						if (resource.resource == originalName)
							resource.resource = newName;
					}
				
				}

				foreach (var region in regions)
				{
					if (region.availableResources.Contains (originalName))
					{
						region.availableResources.Remove (originalName);
						region.availableResources.Add (newName);
					}	
				}
			}

			public void RenameStructure (string originalName, string newName)
			{

				var regions = regionTypeRegister.MasterList;

				foreach (var region in regions)
				{
					if (region.availableStructures.Contains (originalName))
					{
						region.availableStructures.Remove (originalName);
						region.availableStructures.Add (newName);
					}

					if (region.defaultStructures.Contains (originalName))
					{
						region.defaultStructures.Remove (originalName);
						region.defaultStructures.Add (newName);
					}	
				}
			}

			public void RenameRegion (string originalName, string newName)
			{

				var regions = regionTypeRegister.MasterList;

				foreach (var region in regions)
				{
					if (region.availableUpgrades.Contains (originalName))
					{
						region.availableUpgrades.Remove (originalName);
						region.availableUpgrades.Add (newName);
					}

				}
			}

			#endregion
		}

	}
}





