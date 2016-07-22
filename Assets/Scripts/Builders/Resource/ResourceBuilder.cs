using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceBuilder : MonoBehaviour
{
	public ResourceDisplay resourceDisplay;
	ResourceType resourceType = new ResourceType ();
	ResourceTypeRegister allResources;

	public delegate void ResourceTypeBuilderDelegate ();

	public event ResourceTypeBuilderDelegate onComplete;

	void Start ()
	{
		resourceDisplay.Prime (resourceType);
		allResources = Game.Manager.register.resourceTypeRegister;
	}

	public void CreateResourceType ()
	{
		
		if (resourceDisplay.displaynameInput != null)
			resourceType.name = resourceDisplay.displaynameInput.text;	
		if (resourceDisplay.basepriceInput != null)
			resourceType.baseprice = float.Parse (resourceDisplay.basepriceInput.text); 
		if (resourceDisplay.prosperityInput != null)
			resourceType.prosperity = int.Parse (resourceDisplay.prosperityInput.text); 
		if (resourceDisplay.healthInput != null)
			resourceType.health = int.Parse (resourceDisplay.healthInput.text);
		if (resourceDisplay.categoryInput != null)
			resourceType.Category = (ResourceCategory)resourceDisplay.categoryInput.value;

		if (resourceType.name != null && allResources.getResourceType (resourceType.name) == null)
		{
			allResources.MasterList.Add (resourceType);
			if (onComplete != null)
				onComplete.Invoke ();
		}


	}

	public void Cancel ()
	{
		if (onComplete != null)
			onComplete.Invoke ();
	}

	public void destroy ()
	{
		Destroy (gameObject);
	}




}
