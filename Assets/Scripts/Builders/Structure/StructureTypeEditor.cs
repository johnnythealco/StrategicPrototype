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


	void OnDestroy ()
	{
		InputsListBuilder.onResourceUpdate -= onUpdateInputs;
		OutputsListBuilder.onResourceUpdate -= onUpdateOutputs;
	}

	public void destroy ()
	{
		Destroy (gameObject);
	}
	 
}
