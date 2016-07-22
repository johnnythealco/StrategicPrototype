using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceTypeSelect : MonoBehaviour
{
	public ResourceListDisplay ResourceTypeList;

	List<ResourceType> availableResources = new List<ResourceType> ();
	List<ResourceType> selectedResources = new List<ResourceType> ();

	public delegate void ResourceTypeListDelegate (List<ResourceType> _resourceTypes);

	public event ResourceTypeListDelegate onSelect;

	public void Prime (List<ResourceType> _availableResources)
	{
		availableResources.AddRange (_availableResources);
		ResourceTypeList.Prime (availableResources);
		ResourceTypeList.onResourceTypeClick += ResourceTypeClick;
	}

	void Select ()
	{
		if (onSelect != null)
			onSelect.Invoke (selectedResources);
	}

	void OnDestroy ()
	{
		ResourceTypeList.onResourceTypeClick -= ResourceTypeClick;

	}

	void ResourceTypeClick (ResourceType _resource)
	{
		availableResources.Remove (_resource);
		selectedResources.Add (_resource);
		ResourceTypeList.Prime (availableResources);
	}

	public void EndSelection ()
	{
		Select ();
		Destroy (gameObject);
	}

}
