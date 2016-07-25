using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResourceTypeListEditor : MonoBehaviour
{
	protected List<ResourceType> resourceTypeList;

	public ResourceTypeEditor resourceTypeEditor;
	public ResourceTypeListDisplay resourceListDisplay;
	public GameObject navagationBar;

	ResourceType newResourceType;
	ResourceType selectedResourceType;


	public void Prime (List<ResourceType> _resourceTypeList)
	{
		resourceTypeList = _resourceTypeList;
		resourceTypeEditor.gameObject.SetActive (false);
		resourceListDisplay.gameObject.SetActive (true);
		resourceListDisplay.Prime (resourceTypeList);
		resourceListDisplay.onClick += onClickResource;
		navagationBar.SetActive (false);

	}

	void onClickResource (ResourceType _resourceType)
	{
		selectedResourceType = _resourceType;
		var index = resourceTypeList.IndexOf (_resourceType);
		resourceListDisplay.gameObject.SetActive (false);
		resourceTypeEditor.gameObject.SetActive (true);
		resourceTypeEditor.Prime (resourceTypeList [index]);
		navagationBar.SetActive (true);
		resourceTypeEditor.onUpdateType += onUpdateResourceType;
	}

	void onUpdateResourceType (ResourceType _resource)
	{
		
	}

	public void CreateResourceType ()
	{
		newResourceType = new ResourceType ();	
		resourceTypeList.Add (newResourceType);

		resourceListDisplay.gameObject.SetActive (false);	
		resourceTypeEditor.gameObject.SetActive (true);	

		resourceTypeEditor.Prime (resourceTypeList [resourceTypeList.Count () - 1]);
		selectedResourceType = resourceTypeList [resourceTypeList.Count () - 1];
		navagationBar.SetActive (true);


	}

	public void Previous ()
	{
		var index = resourceTypeList.IndexOf (selectedResourceType);

		if (index > 0)
		{
			resourceTypeEditor.Prime (resourceTypeList [index - 1]);
			selectedResourceType = resourceTypeList [index - 1];
		} else
		{
			resourceTypeEditor.Prime (resourceTypeList [resourceTypeList.Count () - 1]);
			selectedResourceType = resourceTypeList [resourceTypeList.Count () - 1];
		}
	}

	public void Next ()
	{
		var index = resourceTypeList.IndexOf (selectedResourceType);

		if (index < resourceTypeList.Count () - 1)
		{
			resourceTypeEditor.Prime (resourceTypeList [index + 1]);
			selectedResourceType = resourceTypeList [index + 1];

		} else
		{
			resourceTypeEditor.Prime (resourceTypeList [0]);
			selectedResourceType = resourceTypeList [0];
		}

	}

	public void Cancel ()
	{
		resourceListDisplay.gameObject.SetActive (false);
		navagationBar.SetActive (false);
		resourceListDisplay.Prime (resourceTypeList);
		resourceListDisplay.gameObject.SetActive (true);
	}

	public void Complete ()
	{		
		resourceTypeEditor.gameObject.SetActive (false);
		navagationBar.SetActive (false);
		resourceListDisplay.Prime (resourceTypeList);
		resourceListDisplay.gameObject.SetActive (true);

	}

	public void Delete ()
	{		
		var index = resourceTypeList.IndexOf (selectedResourceType);
		resourceTypeList.Remove (selectedResourceType);

		if (index < resourceTypeList.Count () - 1)
		{
			resourceTypeEditor.Prime (resourceTypeList [index + 1]);
			selectedResourceType = resourceTypeList [index + 1];

		} else
		{
			resourceTypeEditor.Prime (resourceTypeList [0]);
			selectedResourceType = resourceTypeList [0];
		}


	}





}
