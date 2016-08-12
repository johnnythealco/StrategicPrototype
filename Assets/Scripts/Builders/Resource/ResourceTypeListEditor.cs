using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ResourceTypeListEditor : MonoBehaviour
{
	protected List<ResourceType> workingList;
	protected List<ResourceType> mainList;

	public ResourceTypeEditor resourceTypeEditor;
	public ResourceTypeListDisplay resourceListDisplay;
	public GameObject navagationBar;

	ResourceType newResourceType;
	ResourceType workingResourceType;
	ResourceType selectedResourceType;
	ResourceType backupResourceType;

	public void Prime (List<ResourceType> _resourceTypeList)
	{	
		
		mainList = _resourceTypeList;
		workingList = _resourceTypeList;
		resourceTypeEditor.gameObject.SetActive (false);
		resourceListDisplay.gameObject.SetActive (true);
		workingList.Sort ();
		resourceListDisplay.Prime (workingList);
		resourceListDisplay.onClick += onClickResource;
		resourceListDisplay.onFilter += onFilterResourceList;
		navagationBar.SetActive (false);
	}

	void onFilterResourceList (List<ResourceType> _resourceTypes)
	{			
		workingList = _resourceTypes;
	}

	void onClickResource (ResourceType _resourceType)
	{
		selectedResourceType = _resourceType;
		var index = workingList.IndexOf (_resourceType);
		resourceListDisplay.gameObject.SetActive (false);
		resourceTypeEditor.gameObject.SetActive (true);
		resourceTypeEditor.Prime (workingList [index]);
		navagationBar.SetActive (true);
	}



	public void CreateResourceType ()
	{
		newResourceType = new ResourceType ();	
		workingList.Add (newResourceType);
		resourceListDisplay.gameObject.SetActive (false);	
		resourceTypeEditor.gameObject.SetActive (true);	

		resourceTypeEditor.Prime (workingList [workingList.Count () - 1]);
		selectedResourceType = workingList [workingList.Count () - 1];
		navagationBar.SetActive (true);
	}

	public void Previous ()
	{
		var index = workingList.IndexOf (selectedResourceType);

		if (index > 0)
		{
			resourceTypeEditor.Prime (workingList [index - 1]);
			selectedResourceType = workingList [index - 1];
		} else
		{
			resourceTypeEditor.Prime (workingList [workingList.Count () - 1]);
			selectedResourceType = workingList [workingList.Count () - 1];
		}
	}

	public void Next ()
	{
		var index = workingList.IndexOf (selectedResourceType);

		if (index < workingList.Count () - 1)
		{
			resourceTypeEditor.Prime (workingList [index + 1]);
			selectedResourceType = workingList [index + 1];

		} else
		{
			resourceTypeEditor.Prime (workingList [0]);
			selectedResourceType = workingList [0];
		}

	}

	public void Cancel ()
	{
		resourceTypeEditor.gameObject.SetActive (false);
		resourceListDisplay.gameObject.SetActive (true);
		resourceListDisplay.Prime (workingList);
		navagationBar.SetActive (false);
	}

	public void Complete ()
	{		
		resourceTypeEditor.gameObject.SetActive (false);
		resourceListDisplay.gameObject.SetActive (true);
		resourceListDisplay.Prime (workingList);
		navagationBar.SetActive (false);

		foreach (var item in workingList)
		{
			if (!mainList.Contains (item))
				mainList.Add (item);
		}

	}

	public void Delete ()
	{		
		var index = workingList.IndexOf (selectedResourceType);
		workingList.Remove (selectedResourceType);

		if (mainList.Contains (selectedResourceType))
			mainList.Remove (selectedResourceType);

		if (index < workingList.Count () - 1)
		{
			resourceTypeEditor.Prime (workingList [index + 1]);
			selectedResourceType = workingList [index + 1];

		} else
		{
			resourceTypeEditor.Prime (workingList [0]);
			selectedResourceType = workingList [0];
		}


	}

	void clearList ()
	{
		resourceListDisplay.onClick -= onClickResource;
		resourceListDisplay.onFilter -= onFilterResourceList;
	}

	void OnDestroy ()
	{
		clearList ();
	}




}
