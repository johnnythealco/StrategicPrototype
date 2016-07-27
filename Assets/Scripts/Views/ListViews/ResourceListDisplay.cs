using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceListDisplay : MonoBehaviour
{

	public Transform target;
	public ResourceDisplay resourceDisplay;
	protected List<Resource> resources;

	List<ResourceDisplay> resourceDisplayList = new List<ResourceDisplay> ();

	#region Delegates & Events


	public delegate void ResourceListDisplayDelegate (Resource _resource);

	public event ResourceListDisplayDelegate onResourceClick;
	public event ResourceListDisplayDelegate onResourceUpdate;
	public event ResourceListDisplayDelegate onResourceDelete;

	public delegate void ResourceListDisplayClose ();

	public event ResourceListDisplayClose onClose;

	public void Close ()
	{
		if (onClose != null)
			onClose.Invoke ();
	}

	#endregion


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
			listItem.onClick += OnResourceClick;
			listItem.onUpdate += OnResoucreUpdate;
			listItem.onDelete += OnDeleteResource;

			resourceDisplayList.Add (listItem);		

		}
	}

	#region Event Callers

	void OnDeleteResource (Resource _resource)
	{
		if (onResourceDelete != null)
			onResourceDelete.Invoke (_resource);
	}



	void OnResoucreUpdate (Resource _resource)
	{
		if (onResourceUpdate != null)
			onResourceUpdate.Invoke (_resource);
		
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
			item.onClick -= OnResourceClick;
			item.onUpdate -= OnResoucreUpdate;
			item.onDelete -= OnDeleteResource;

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
		clearList ();
		Destroy (gameObject);
	}


}
