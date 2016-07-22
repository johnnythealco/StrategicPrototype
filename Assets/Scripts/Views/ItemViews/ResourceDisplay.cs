using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResourceDisplay : MonoBehaviour
{

	protected ResourceType resourceType;
	protected Resource resource;
	public Text displayname;

	public Image icon;
	public Text Category;
	public Text baseprice;
	public Text prosperity;
	public Text health;
	public Text quantity;

	public InputField quantityInput;
	public InputField displaynameInput;
	public Dropdown categoryInput;
	public InputField basepriceInput;
	public InputField prosperityInput;
	public InputField healthInput;

	void Start ()
	{
		if (categoryInput != null)
		{
			categoryInput.ClearOptions ();
			categoryInput.AddOptions (ResourceType.getCategories ());
		}
	}


	#region Delegates & Events

	public delegate void ResourceTypeDisplayDelegate (ResourceType _resource);

	public event ResourceTypeDisplayDelegate onClickType;

	public delegate void ResourceDisplayDelegate (Resource _resource);

	public event ResourceDisplayDelegate onClickResource;

	public delegate void ResourceTypeUpdateDelegate (ResourceType _resourceType);

	public event ResourceTypeUpdateDelegate onUpdateType;

	public delegate void ResourceUpdateDelegate (Resource _resource);

	public event ResourceUpdateDelegate onUpdateResource;

	public delegate void ResourceDeleteDelegate (Resource _resource);

	public event ResourceDeleteDelegate onDeleteResource;

	public delegate void ResourceTypeDeleteDelegate (ResourceType _resourceType);

	public event ResourceTypeDeleteDelegate onDeleteResourceType;



	#endregion

	public void Prime (ResourceType _resourceType)
	{
		if (_resourceType == null || _resourceType.name == null)
			return;

		resourceType = _resourceType;

		if (displayname != null)
			displayname.text = resourceType.name;
		if (icon != null)
			icon.sprite = resourceType.icon;
		if (Category != null)
			Category.text = resourceType.Category.ToString (); 		
		if (baseprice != null)
			baseprice.text = resourceType.baseprice.ToString (); 
		if (prosperity != null)
			prosperity.text = resourceType.prosperity.ToString (); 
		if (health != null)
			health.text = resourceType.health.ToString ();
		
		if (displaynameInput != null)
			displaynameInput.text = resourceType.name;	
		if (basepriceInput != null)
			basepriceInput.text = resourceType.baseprice.ToString (); 
		if (prosperityInput != null)
			prosperityInput.text = resourceType.prosperity.ToString (); 
		if (healthInput != null)
			healthInput.text = resourceType.health.ToString ();
		if (categoryInput != null)
			categoryInput.value = (int)resourceType.Category;
		


	}

	public void Prime (Resource _resource)
	{
		if (_resource == null || _resource.resource == null)
			return;

		resource = _resource;	
		resourceType = _resource.type;

		Prime (resourceType);
	
		if (quantity != null)
			quantity.text = resource.value.ToString ();
		if (quantityInput != null)
			quantityInput.text = resource.value.ToString ();

	}

	public void InputPrime ()
	{
		

		if (displaynameInput != null)
			resourceType.name = displaynameInput.text;	
		if (basepriceInput != null)
			resourceType.baseprice = float.Parse (basepriceInput.text); 
		if (prosperityInput != null)
			resourceType.prosperity = int.Parse (prosperityInput.text); 
		if (healthInput != null)
			resourceType.health = int.Parse (healthInput.text);
		if (quantityInput != null)
			resource.value = int.Parse (quantityInput.text);
		if (categoryInput != null)
			resourceType.Category = (ResourceCategory)categoryInput.value;

		
	}


	public void Click ()
	{
		if (onClickType != null)
			onClickType.Invoke (resourceType);

		if (onClickResource != null)
			onClickResource.Invoke (resource);
	
	}

	public void InputUpdate (string inputString)
	{
		InputPrime ();
		if (onUpdateType != null)
			onUpdateType.Invoke (resourceType);

		if (onUpdateResource != null)
			onUpdateResource.Invoke (resource);
	}

	public void DeleteResource ()
	{
		if (onDeleteResource != null)
			onDeleteResource.Invoke (resource);

		if (onDeleteResourceType != null)
			onDeleteResourceType.Invoke (resourceType);
	}



}
