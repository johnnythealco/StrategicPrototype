using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace JK
{
	namespace GameData
	{

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
			public int size;
			public RegionCategory Category;

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

			public static List<string> getCategories ()
			{
				return Enum.GetNames (typeof(RegionCategory)).ToList ();
			}

			public static List<string> GetNames (List<RegionType> _regionTypeList)
			{
				var result = new List<string> ();

				foreach (var item  in _regionTypeList)
				{			
					result.Add (item.name);
				}
				return result;
			}

			public static List<RegionType> FilterListByCategory (List<RegionType> _regionTypeList, RegionCategory _category)
			{
				var result = new List<RegionType> ();

				foreach (var item  in _regionTypeList)
				{
					if (item.Category == _category)
						result.Add (item);

				}
				return result;
			}

			public static List<RegionType>  SearchList (List<RegionType> _List, string _keyword)
			{
				var result = new List<RegionType> ();

				var resultList = GetNames (_List).FindAll (delegate(string _string)
				{
					return  _string.Contains (_keyword);
				});

				foreach (var name in resultList)
				{
					foreach (var region in _List)
					{
						if (region.name == name)
							result.Add (region);
					}
				}

				return result;


			}

	
		}

		public enum RegionCategory
		{
			Space = 0,
			Asteroid = 1,
			Rocky = 2,
			Atmospheric = 3,
			Oceanic = 4,
			Grassland = 5,
			Desert = 6,
			Arctic = 7,
			Volcanic = 8
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

				var resultList = GetNames (_List).FindAll (delegate(string _string)
				{
					return  _string.Contains (_keyword);
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

	}
}