using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ResourceListBuilder : MonoBehaviour
{
	public Register register;
	public ResourceTypeListDisplay resourceListDisplayPrefab;
	public Transform target;
	public ResourceDisplay resourceDisplay;

	ResourceTypeListDisplay TypeSelect;
	List<ResourceDisplay> resourceDisplayList = new List<ResourceDisplay> ();
	List<Resource> resources = new List<Resource> ();
	List<ResourceType> availableResourceTypes;

	#region Delegates & Events

	public delegate void ResourceListBuilderDelegate (List<Resource> _resources);

	public event ResourceListBuilderDelegate onResourceUpdate;

	public delegate void ResourceListDisplayDelegate (Resource _resource);

	public event ResourceListDisplayDelegate onResourceClick;


	#endregion

	#region Event Callers


	void OnResourceClick (Resource _resource)
	{
		if (onResourceClick != null)
			onResourceClick.Invoke (_resource);
	}

	void OnResourceUpdate (Resource _resource)
	{

		foreach (var item in resources)
		{
			if (item.resource == _resource.resource)
			{
				item.value = _resource.value;
			} 
		}
		if (onResourceUpdate != null)
			onResourceUpdate.Invoke (resources);

	}

	void OnTypeSelect (ResourceType _resourceType)
	{
		var resource = new Resource (_resourceType.name, 0);
		resources.Add (resource);
	
		TypeSelect.onClick -= OnTypeSelect;
		TypeSelect.onClose -= closeTypeSelect;
		TypeSelect.destroy ();
		Prime (resources);
	}



	#endregion

	public void SetResourceTypes (List<ResourceType> _ResourceTypes)
	{
		availableResourceTypes.Clear ();
		availableResourceTypes.AddRange (_ResourceTypes);
	}

	public void Prime (List<Resource> _resources)
	{
		clearList ();
		resources = _resources;
		foreach (var resource in resources)
		{
			ResourceDisplay listItem = (ResourceDisplay)Instantiate (resourceDisplay);
			listItem.transform.SetParent (target, false);
			listItem.Prime (resource);
			listItem.gameObject.tag = "ResourceDisplay";		
			listItem.onUpdate += OnResourceUpdate;
			listItem.onDelete += onDeleteResource;
			resourceDisplayList.Add (listItem);		
		}			

		if (onResourceUpdate != null)
			onResourceUpdate.Invoke (resources);
	}



	void onDeleteResource (Resource _resource)
	{
		resources.Remove (_resource);
		Prime (resources);
		if (onResourceUpdate != null)
			onResourceUpdate.Invoke (resources);
		
	}

	public void AddResourceType ()
	{
		TypeSelect = (ResourceTypeListDisplay)Instantiate (resourceListDisplayPrefab);
		List<ResourceType> _resourceTypes = new List<ResourceType> (availableResourceTypes);

		TypeSelect.Prime (_resourceTypes);
		TypeSelect.onClick += OnTypeSelect;
		TypeSelect.onClose += closeTypeSelect;

	}

	void closeTypeSelect ()
	{
		TypeSelect.onClick -= OnTypeSelect;
		TypeSelect.onClose -= closeTypeSelect;
		TypeSelect.destroy ();
	}

	void Awake ()
	{
		if (availableResourceTypes == null)
			availableResourceTypes = new List<ResourceType> ();
		
		availableResourceTypes.AddRange (register.AllResources);
	}

	void clearList ()
	{
		foreach (var resource in resourceDisplayList)
		{
			resource.onDelete -= onDeleteResource;
			resource.onUpdate -= OnResourceUpdate;

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

}
