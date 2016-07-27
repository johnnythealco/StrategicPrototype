using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ResourceTypeListDisplay : MonoBehaviour
{

	public Transform target;
	public ResourceTypeDisplay resourceDisplay;
	protected List<ResourceType> resourceTypes;
	protected List<ResourceType> filteredList;
	protected List<ResourceType> fullList;

	public Dropdown CategoryFilter;
	public Toggle CategoryToggel;
	public Dropdown LevelFilter;
	public Toggle LevelToggel;

	List<ResourceTypeDisplay> resourceDisplayList = new List<ResourceTypeDisplay> ();

	#region Delegates & Events

	public delegate void ResourceTypeDisplayDelegate (ResourceType _resourceType);

	public delegate void ResourceTypeListDisplayDelegate (List<ResourceType>  _resourceTypes);

	public event ResourceTypeDisplayDelegate onClick;
	public event ResourceTypeDisplayDelegate onUpdate;
	public event ResourceTypeListDisplayDelegate onFilter;


	public delegate void ResourceTypeListDisplayClose ();

	public event ResourceTypeListDisplayClose onClose;
	public event ResourceTypeListDisplayClose onRemoveFilter;

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
			CategoryFilter.AddOptions (ResourceType.getCategories ()); 
			CategoryFilter.interactable = false;
		}

		if (LevelFilter != null)
		{
			LevelFilter.ClearOptions ();
			LevelFilter.AddOptions (ResourceType.getLevels ());
			LevelFilter.interactable = false;
		}
	}

	public void Prime (List<ResourceType> _resourceTypes)
	{
		clearList ();

		resourceTypes = _resourceTypes;
		fullList = resourceTypes;
		foreach (var resource in resourceTypes)
		{
			ResourceTypeDisplay listItem = (ResourceTypeDisplay)Instantiate (resourceDisplay);
			listItem.transform.SetParent (target, false);
			listItem.Prime (resource);
			listItem.gameObject.tag = "ResourceDisplay";
			listItem.onClick += OnResourceTypeClick;
			listItem.onUpdate += OnResourceTypeUpdate;

			resourceDisplayList.Add (listItem);		

		}
	}


	void FilteredPrime (List<ResourceType> _resourceTypes)
	{
		if (resourceTypes.Count () >= fullList.Count ())
			fullList = resourceTypes;

		clearList ();

		resourceTypes = _resourceTypes;
		foreach (var resource in resourceTypes)
		{
			ResourceTypeDisplay listItem = (ResourceTypeDisplay)Instantiate (resourceDisplay);
			listItem.transform.SetParent (target, false);
			listItem.Prime (resource);
			listItem.gameObject.tag = "ResourceDisplay";
			listItem.onClick += OnResourceTypeClick;
			listItem.onUpdate += OnResourceTypeUpdate;

			resourceDisplayList.Add (listItem);		

		}

	}

	public void FilterList ()
	{
		filteredList = fullList;
		if (CategoryFilter == null || LevelFilter == null)
		{
			Debug.Log ("Filter Reference nt Found!"); 
			return;
		}

		if (CategoryToggel == null || LevelToggel == null)
		{
			Debug.Log ("Toggel Reference nt Found!"); 
			return;
		}

		if (CategoryToggel.isOn || LevelToggel.isOn)
		{	
			if (CategoryToggel.isOn)
			{
				var _category = (ResourceCategory)CategoryFilter.value; 
				filteredList = ResourceType.FilterListByCategory (filteredList, _category); 
			}
			if (LevelToggel.isOn)
			{
				var _level = LevelFilter.value;  
				filteredList = ResourceType.FilterListByLevel (filteredList, _level); 
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

	public void GetLevelToggel ()
	{
		if (LevelToggel.isOn)
		{
			LevelFilter.interactable = true;
			FilterList ();
		} else
		{
			LevelFilter.interactable = false;
			FilterList ();
		}
	}


	#region Event Callers

	void OnResourceTypeUpdate (ResourceType _resourceType)
	{
		if (onUpdate != null)
			onUpdate.Invoke (_resourceType);

	}

	void OnResourceTypeClick (ResourceType _resourceType)
	{
		if (onClick != null)
			onClick.Invoke (_resourceType);
	}


	#endregion

	void clearList ()
	{
		foreach (var item in resourceDisplayList)
		{
			item.onClick -= OnResourceTypeClick;
		}

		for (int i = 0; i < target.childCount; i++)
		{
			if (target.GetChild (i).gameObject.tag == "ResourceDisplay")
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
