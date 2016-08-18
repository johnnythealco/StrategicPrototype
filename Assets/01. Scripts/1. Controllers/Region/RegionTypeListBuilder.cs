using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JK.GameData;
using JK.View;

namespace JK
{
	namespace Controller
	{
		


		public class RegionTypeListBuilder : MonoBehaviour
		{
			protected List<RegionType> regionTypes;
			public Register register;
			public RegionTypeListDisplay regionTypeListPrefab;

			public Transform target;
			public RegionDisplay regionTypeDisplay;

			RegionTypeListDisplay TypeSelect;

			List<RegionDisplay> regionDisplayList = new List<RegionDisplay> ();
			List<RegionType> availableRegionTypes;



			public delegate void RegionTypeListUpdate (List<RegionType> _regionTypes);

			public event RegionTypeListUpdate onListUpdate;

			void ListUpdate ()
			{
				if (onListUpdate != null)
					onListUpdate.Invoke (regionTypes);
			}

			public void Prime (List<RegionType> _regionTypes)
			{
				clearList ();
				regionTypes = _regionTypes;
				foreach (var region in regionTypes)
				{
					RegionDisplay listItem = (RegionDisplay)Instantiate (regionTypeDisplay);
					listItem.transform.SetParent (target, false);
					listItem.Prime (region);
					listItem.gameObject.tag = "RegionDisplay";
					regionDisplayList.Add (listItem);
					listItem.onDelete += onDeleteRegionType;
				}

				ListUpdate ();
			}

			public void AddRegionType ()
			{
				if (availableRegionTypes == null)
					availableRegionTypes = register.regionTypeRegister.MasterList;
		
				TypeSelect = (RegionTypeListDisplay)Instantiate (regionTypeListPrefab);
				TypeSelect.Prime (availableRegionTypes);
				TypeSelect.onClick += onTypeSelect;
				TypeSelect.onClose += closeTypeSelect;

			}

			void closeTypeSelect ()
			{
				TypeSelect.onClick -= onTypeSelect;
				TypeSelect.onClose -= closeTypeSelect;
				TypeSelect.destroy ();
			}


			void onTypeSelect (RegionType _regionType)
			{
				if (regionTypes == null)
					regionTypes = new List<RegionType> ();
		
				if (!regionTypes.Contains (_regionType))
					regionTypes.Add (_regionType);

				TypeSelect.onClick -= onTypeSelect;
				TypeSelect.onClose += closeTypeSelect;
				TypeSelect.destroy ();
				Prime (regionTypes);
			}

			void onDeleteRegionType (RegionType _regionType)
			{
				if (regionTypes.Contains (_regionType))
					regionTypes.Remove (_regionType);


				Prime (regionTypes);
			}

			void clearList ()
			{
				foreach (var item in regionDisplayList)
				{
					item.onDelete -= onDeleteRegionType;
				}

				for (int i = 0; i < target.childCount; i++)
				{
					if (target.GetChild (i).gameObject.tag == "RegionDisplay")
					{
						Destroy (target.GetChild (i).gameObject);
					}
				}

			}

			void Awake ()
			{
				if (availableRegionTypes == null)
					availableRegionTypes = new List<RegionType> ();
				availableRegionTypes.AddRange (register.regionTypeRegister.MasterList);
			}
		
		}
	}
}
