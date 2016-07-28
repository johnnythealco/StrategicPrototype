using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StructureTypeEditor : MonoBehaviour
{
	public Register register;
	public InputField displaynameInput;
	public InputField descriptionInput;
	public Dropdown categoryInput;
	public ResourceListBuilder resourceCostBuilder;
	public ResourceListBuilder InputsListBuilder;
	public ResourceListBuilder OutputsListBuilder;

	protected StructureType structureType;



	#region Delegates & Events

	public delegate void StructureTypeUpdateDelegate (StructureType _structureType);

	public event StructureTypeUpdateDelegate onUpdateType;

	public delegate void StructureTypeUpdateCompleteDelegate (StructureType _structureType);

	public event StructureTypeUpdateCompleteDelegate onComplete;

	public delegate void StructureTypeUpdateCancelDelegate (StructureType _structureType);

	public event StructureTypeUpdateCancelDelegate onCancel;

	public delegate void StructureTypeNextDelegate (StructureType _structureType);

	public event StructureTypeNextDelegate onNext;

	public delegate void StructureTypePreviousDelegate (StructureType _structureType);

	public event StructureTypePreviousDelegate onPrevious;

	#endregion


	void Start ()
	{
		if (categoryInput != null)
		{
			categoryInput.ClearOptions ();
			categoryInput.AddOptions (StructureType.getCategories ());
		}
	}

	public void Prime (StructureType _structureType)
	{
		structureType = _structureType; 

		if (displaynameInput != null)
			displaynameInput.text = structureType.name;
		if (descriptionInput != null)
			descriptionInput.text = structureType.descriptions;
		if (categoryInput != null)
			categoryInput.value = (int)structureType.Category;

		if (resourceCostBuilder != null)
		{
			resourceCostBuilder.Prime (structureType.resourceCost.list);
			resourceCostBuilder.SetResourceTypes (register.StrategicResources);
			resourceCostBuilder.onResourceUpdate += onResourceCostUpdat;
		}
		if (InputsListBuilder != null)
		{
			InputsListBuilder.Prime (structureType.inputs.list);
			InputsListBuilder.onResourceUpdate += onUpdateInputs;
		}
		if (OutputsListBuilder != null)
		{
			OutputsListBuilder.Prime (structureType.outputs.list);
			OutputsListBuilder.onResourceUpdate += onUpdateOutputs;
		}
	
	}

	void onResourceCostUpdat (List<Resource> _resources)
	{
		structureType.resourceCost.list = _resources;
		if (onUpdateType != null)
			onUpdateType.Invoke (structureType);
	}

	void onUpdateOutputs (List<Resource> _resources)
	{
		structureType.outputs.list = _resources;
		if (onUpdateType != null)
			onUpdateType.Invoke (structureType);
	}

	void onUpdateInputs (List<Resource> _resources)
	{
		structureType.inputs.list = _resources;
		if (onUpdateType != null)
			onUpdateType.Invoke (structureType);
	}

	public void GetInput ()
	{
		if (displaynameInput != null)
			structureType.name = displaynameInput.text;
		if (descriptionInput != null)
			structureType.descriptions = descriptionInput.text;
		if (categoryInput != null)
			structureType.Category = (StructureCategory)categoryInput.value;
	}

	public void complete ()
	{
		if (onComplete != null)
			onComplete.Invoke (structureType); 
	}

	public void cancel ()
	{
		if (onCancel != null)
			onCancel.Invoke (structureType);
	}

	void OnDestroy ()
	{
		InputsListBuilder.onResourceUpdate -= onUpdateInputs;
		OutputsListBuilder.onResourceUpdate -= onUpdateOutputs;
	}

	public void destroy ()
	{
		Destroy (gameObject);
	}

	public void Next ()
	{
		if (onNext != null)
			onNext.Invoke (structureType);
	}

	public void Previous ()
	{
		if (onPrevious != null)
			onPrevious.Invoke (structureType);
	}





	

	 
}
