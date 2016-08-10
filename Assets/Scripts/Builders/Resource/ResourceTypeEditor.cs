using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceTypeEditor : MonoBehaviour
{
	protected ResourceType resourceType;

	public InputField displaynameInput;
	public InputField descriptionInput;
	public Dropdown categoryInput;
	public Dropdown levelInput;

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

	public event ResourceTypeDisplayDelegate onUpdateType;

	public event ResourceTypeDisplayDelegate onDeleteResourceType;


	#endregion

	public void Prime (ResourceType _resourceType)
	{
		resourceType = _resourceType;

		if (displaynameInput != null)
			displaynameInput.text = resourceType.name;
		if (descriptionInput != null)
			descriptionInput.text = resourceType.descriptions;
		if (categoryInput != null)
			categoryInput.value = (int)resourceType.Category;
		if (levelInput != null)
			levelInput.value = resourceType.level;

	}

	public void GetInput ()
	{


		if (descriptionInput != null)
			resourceType.descriptions = descriptionInput.text;
		if (categoryInput != null)
			resourceType.Category = (ResourceCategory)categoryInput.value;
		if (levelInput != null)
			resourceType.level = levelInput.value;

		InputUpdate ();
	}

	public void GetNameInput ()
	{
		if (displaynameInput != null)
		{
			if (displaynameInput.text != resourceType.name)
			{
				var originalName = resourceType.name;
				var newName = displaynameInput.text;
				Game.Manager.register.RenameResource (originalName, newName);
				resourceType.name = displaynameInput.text;
			}
		}
	}


	public void Click ()
	{
		if (onClickType != null)
			onClickType.Invoke (resourceType);

	}

	void InputUpdate ()
	{
		if (onUpdateType != null)
			onUpdateType.Invoke (resourceType);
	}

	public void DeleteResource ()
	{

		if (onDeleteResourceType != null)
			onDeleteResourceType.Invoke (resourceType);
	}



}
