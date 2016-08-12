using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
	protected Resource resource;

	public Text displayname;
	public Image icon;
	public Text value;
	public InputField valueInput;
	public Button increaseButton;
	public Button decreaseButton;


	#region Delegates & Events

	public delegate void ResourceEditorDelegate (Resource _resource);

	public event ResourceEditorDelegate onClick;

	public event ResourceEditorDelegate onUpdate;

	public event ResourceEditorDelegate onDelete;

	#endregion

	public void Prime (Resource _resource)
	{
		resource = _resource;

		if (displayname != null)
			displayname.text = resource.resource;
		if (icon != null && resource.type.smallImage != null)
			icon.sprite = resource.type.smallImage;
		if (value != null)
			value.text = resource.value.ToString ();
		if (valueInput != null)
			valueInput.text = resource.value.ToString ();


	}

	public void GetInput ()
	{

		if (valueInput != null)
			resource.value = int.Parse (valueInput.text);
		
		InputUpdate ();
	}


	public void Click ()
	{
		if (onClick != null)
			onClick.Invoke (resource);

	}

	void InputUpdate ()
	{
		if (onUpdate != null)
			onUpdate.Invoke (resource);
	}

	public void DeleteResource ()
	{

		if (onDelete != null)
			onDelete.Invoke (resource);
	}

	public void IncreaseValue ()
	{
		resource.value = resource.value + 1;
		InputUpdate ();
	}


	public void DecreaseValue ()
	{
		resource.value = resource.value - 1;
		InputUpdate ();
	}


}
