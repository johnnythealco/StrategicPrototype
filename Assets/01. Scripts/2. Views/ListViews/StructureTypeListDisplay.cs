using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JK.GameData;


namespace JK
{
	namespace View
	{


		public class StructureTypeListDisplay : MonoBehaviour
		{

			public Transform target;
			public StructureDisplay structureDisplay;
			protected List<StructureType> workingList;
			protected List<StructureType> filteredList;
			protected List<StructureType> fullList;

			public InputField searchField;
			public Dropdown CategoryFilter;
			public Toggle CategoryToggel;

			StructureType newStructureType;


			List<StructureDisplay> structureDisplayList = new List<StructureDisplay> ();

			#region Delegates & Events

			public delegate void StructureTypeDisplayDelegate (StructureType _structureType);

			public delegate void StructureTypeListDisplayDelegate (List<StructureType>  _structureTypes);

			public event StructureTypeDisplayDelegate onClick;
			public event StructureTypeDisplayDelegate onUpdate;
			public event StructureTypeListDisplayDelegate onFilter;


			public delegate void StructureTypeListDisplayClose ();

			public event StructureTypeListDisplayClose onClose;
			public event StructureTypeListDisplayClose onRemoveFilter;

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
					CategoryFilter.AddOptions (StructureType.getCategories ()); 
					CategoryFilter.interactable = false;
				}
			
			}

			public void Prime (List<StructureType> _structureTypes)
			{
				clearList ();
				Game.Manager.register.structureRegister.SetDefaultIcon ();

				workingList = _structureTypes;
				fullList = workingList;
				foreach (var structure in workingList)
				{
					StructureDisplay listItem = (StructureDisplay)Instantiate (structureDisplay);
					listItem.transform.SetParent (target, false);
					listItem.Prime (structure);
					listItem.gameObject.tag = "StructureDisplay";
					listItem.onClick += OnStructureTypeClick;

					structureDisplayList.Add (listItem);		

				}
			}

			void FilteredPrime (List<StructureType> _structureTypes)
			{
				if (workingList.Count () >= fullList.Count ())
					fullList = workingList;

				clearList ();

				workingList = _structureTypes;
				foreach (var structure in workingList)
				{
					StructureDisplay listItem = (StructureDisplay)Instantiate (structureDisplay);
					listItem.transform.SetParent (target, false);
					listItem.Prime (structure);
					listItem.gameObject.tag = "StructureDisplay";
					listItem.onClick += OnStructureTypeClick;

					structureDisplayList.Add (listItem);		

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
						var _category = (StructureCategory)CategoryFilter.value; 
						filteredList = StructureType.FilterListByCategory (filteredList, _category); 
					}

					if (searchField.text.Length > 0)
					{
						var keyword = searchField.text; 
						filteredList = StructureType.SearchList (filteredList, keyword);

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

			void OnStructureTypeUpdate (StructureType _structureType)
			{
				if (onUpdate != null)
					onUpdate.Invoke (_structureType);

			}

			void OnStructureTypeClick (StructureType _structureType)
			{
				if (onClick != null)
					onClick.Invoke (_structureType);
			}


			#endregion

			void clearList ()
			{
				foreach (var item in structureDisplayList)
				{
					item.onClick -= OnStructureTypeClick;
				}

				for (int i = 0; i < target.childCount; i++)
				{
					if (target.GetChild (i).gameObject.tag == "StructureDisplay")
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
