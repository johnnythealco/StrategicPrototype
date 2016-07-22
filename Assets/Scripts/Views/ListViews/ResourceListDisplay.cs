using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceListDisplay : MonoBehaviour
{

	public Transform target;
	public ResourceDisplay resourceDisplay;
	protected List<ResourceType> resourceTypes;
	protected List<Resource> resources;

	List<ResourceDisplay> resourceDisplayList = new List<ResourceDisplay> ();

	#region Delegates & Events

	public delegate void ResourceTypeListDisplayDelegate (ResourceType _resourceType);

	public event ResourceTypeListDisplayDelegate onResourceTypeClick;
	public event ResourceTypeListDisplayDelegate onResourceTypeUpdate;

	public delegate void ResourceListDisplayDelegate (Resource _resource);

	public event ResourceListDisplayDelegate onResourceClick;
	public event ResourceListDisplayDelegate onResourceUpdate;

	public delegate void ResourceTypeListDisplayClose ();

	public event ResourceTypeListDisplayClose onClose;

	public void Close ()
	{
		if (onClose != null)
			onClose.Invoke ();
	}

	#endregion

	public void Prime (List<ResourceType> _resourceTypes)
	{
		clearList ();

		resourceTypes = _resourceTypes;
		foreach (var resource in resourceTypes)
		{
			ResourceDisplay listItem = (ResourceDisplay)Instantiate (resourceDisplay);
			listItem.transform.SetParent (target, false);
			listItem.Prime (resource);
			listItem.gameObject.tag = "ResourceDisplay";
			listItem.onClickType += OnResourceTypeClick;
			listItem.onUpdateType += OnResourceTypeUpdate;

			resourceDisplayList.Add (listItem);		
			
		}
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
			listItem.onClickResource += OnResourceClick;
			listItem.onUpdateResource += OnResoucreUpdate;

			resourceDisplayList.Add (listItem);		

		}
	}

	#region Event Callers

	void OnResourceTypeUpdate (ResourceType _resourceType)
	{
		if (onResourceTypeUpdate != null)
			onResourceTypeUpdate.Invoke (_resourceType);

	}

	void OnResoucreUpdate (Resource _resource)
	{
		if (onResourceUpdate != null)
			onResourceUpdate.Invoke (_resource);
		
	}

	void OnResourceTypeClick (ResourceType _resourceType)
	{
		if (onResourceTypeClick != null)
			onResourceTypeClick.Invoke (_resourceType);
	}

	void OnResourceClick (Resource _resource)
	{
		if (onResourceClick != null)
			onResourceClick.Invoke (_resource);
	}

	#endregion

	void clearList ()
	{
		foreach (var item in resourceDisplayList)
		{
			item.onClickType -= OnResourceTypeClick;
			item.onClickResource -= OnResourceClick;
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
		foreach (var resource in resourceDisplayList)
		{
			//Unsubscribe form events

			resource.onClickType -= OnResourceTypeClick;
			resource.onUpdateType -= OnResourceTypeUpdate;


			resource.onClickResource -= OnResourceClick;
			resource.onUpdateResource -= OnResoucreUpdate;


		}
	}



	public void destroy ()
	{
		clearList ();
		Destroy (gameObject);
	}


}
