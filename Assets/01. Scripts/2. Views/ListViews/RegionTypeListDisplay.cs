using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using JK.GameData;


namespace JK
{
	namespace View
	{


		public class RegionTypeListDisplay : MonoBehaviour
		{
			public Transform target;
			public RegionDisplay regionDisplay;
			protected List<RegionType> workingList;
			protected List<RegionType> filteredList;
			protected List<RegionType> fullList;

			public InputField searchField;
			public Dropdown CategoryFilter;
			public Toggle CategoryToggel;

			RegionType newRegionType;


			List<RegionDisplay> regionDisplayList = new List<RegionDisplay> ();

			#region Delegates & Events

			public delegate void RegionTypeDisplayDelegate (RegionType _regionType);

			public delegate void RegionTypeListDisplayDelegate (List<RegionType>  _regionTypes);

			public event RegionTypeDisplayDelegate onClick;
			public event RegionTypeDisplayDelegate onUpdate;
			public event RegionTypeListDisplayDelegate onFilter;


			public delegate void RegionTypeListDisplayClose ();

			public event RegionTypeListDisplayClose onClose;
			public event RegionTypeListDisplayClose onRemoveFilter;

			public void Close ()
			{
				if (onClose != null)
					onClose.Invoke ();
			}

			#endregion

			void Start ()
			{
				if (CategoryFilter != null)
				{
					CategoryFilter.ClearOptions (); 
					CategoryFilter.AddOptions (RegionType.getCategories ()); 
					CategoryFilter.interactable = false;
				}

			}

			public void Prime (List<RegionType> _regionTypes)
			{
				clearList ();
				Game.Manager.register.regionTypeRegister.SetDefaultIcon ();

				workingList = _regionTypes;
				fullList = workingList;
				foreach (var region in workingList)
				{
					RegionDisplay listItem = (RegionDisplay)Instantiate (regionDisplay);
					listItem.transform.SetParent (target, false);
					listItem.Prime (region);
					listItem.gameObject.tag = "RegionDisplay";
					listItem.onClick += OnRegionTypeClick;

					regionDisplayList.Add (listItem);		

				}
			}

			void FilteredPrime (List<RegionType> _regionTypes)
			{
				if (workingList.Count () >= fullList.Count ())
					fullList = workingList;

				clearList ();

				workingList = _regionTypes;
				foreach (var region in workingList)
				{
					RegionDisplay listItem = (RegionDisplay)Instantiate (regionDisplay);
					listItem.transform.SetParent (target, false);
					listItem.Prime (region);
					listItem.gameObject.tag = "RegionDisplay";
					listItem.onClick += OnRegionTypeClick;

					regionDisplayList.Add (listItem);		

				}

				searchField.text = "";

			}

			public void FilterList ()
			{
				filteredList = fullList;
				if (CategoryFilter == null || CategoryToggel == null)
				{
					Debug.Log ("Filter Reference nt Found!"); 
					return;
				}


				if (CategoryToggel.isOn || searchField.text.Length > 0)
				{	
					if (CategoryToggel.isOn)
					{
						var _category = (RegionCategory)CategoryFilter.value; 
						filteredList = RegionType.FilterListByCategory (filteredList, _category); 
					}

					if (searchField.text.Length > 0)
					{
						var keyword = searchField.text; 
						filteredList = RegionType.SearchList (filteredList, keyword);

					}
					FilteredPrime (filteredList);

					if (onFilter != null)
						onFilter.Invoke (filteredList);

					return;
				} else
				{
					Prime (fullList);

					if (onRemoveFilter != null)
						onRemoveFilter.Invoke ();
				}


			}

			public void GetCategoryToggel ()
			{
				if (CategoryToggel.isOn)
				{
					CategoryFilter.interactable = true;
					FilterList ();
				} else
				{
					CategoryFilter.interactable = false;
					FilterList ();
				}
			}





			#region Event Callers

			void OnRegionTypeUpdate (RegionType _regionType)
			{
				if (onUpdate != null)
					onUpdate.Invoke (_regionType);

			}

			void OnRegionTypeClick (RegionType _regionType)
			{
				if (onClick != null)
					onClick.Invoke (_regionType);
			}


			#endregion

			void clearList ()
			{
				foreach (var item in regionDisplayList)
				{
					item.onClick -= OnRegionTypeClick;
				}

				for (int i = 0; i < target.childCount; i++)
				{
					if (target.GetChild (i).gameObject.tag == "RegionDisplay")
					{
						Destroy (target.GetChild (i).gameObject);
					}
				}

			}

			void OnDestroy ()
			{
				clearList ();
			}

			public void destroy ()
			{

				Destroy (gameObject);
			}


		}
	}
}
