using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceTypeListDisplay : MonoBehaviour
{

	public Transform target;
	public ResourceTypeDisplay resourceDisplay;
	protected List<ResourceType> resourceTypes;

	List<ResourceTypeDisplay> resourceDisplayList = new List<ResourceTypeDisplay> ();

	#region Delegates & Events

	public delegate void ResourceTypeListDisplayDelegate (ResourceType _resourceType);

	public event ResourceTypeListDisplayDelegate onClick;
	public event ResourceTypeListDisplayDelegate onUpdate;

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
			ResourceTypeDisplay listItem = (ResourceTypeDisplay)Instantiate (resourceDisplay);
			listItem.transform.SetParent (target, false);
			listItem.Prime (resource);
			listItem.gameObject.tag = "ResourceDisplay";
			listItem.onClick += OnResourceTypeClick;
			listItem.onUpdate += OnResourceTypeUpdate;

			resourceDisplayList.Add (listItem);		

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
