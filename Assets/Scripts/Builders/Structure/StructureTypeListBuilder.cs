using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StructureTypeListBuilder : MonoBehaviour
{
	protected List<StructureType> structureTypes;
	public Register register;
	public StructureTypeListDisplay structureTypeListPrefab;
	public Transform target;
	public StructureDisplay structureTypeDeleteDisplay;

	StructureTypeListDisplay TypeSelect;
	List<StructureDisplay> structureDisplayList = new List<StructureDisplay> ();
	List<StructureType> availableStructureTypes;

	public delegate void StructureTypeListUpdate (List<StructureType> _structureTypes);

	public event StructureTypeListUpdate onListUpdate;

	void ListUpdate ()
	{
		if (onListUpdate != null)
			onListUpdate.Invoke (structureTypes);
	}

	public void Prime (List<StructureType> _structureTypes)
	{
		clearList ();
		structureTypes = _structureTypes;
		foreach (var structure in structureTypes)
		{
			StructureDisplay listItem = (StructureDisplay)Instantiate (structureTypeDeleteDisplay);
			listItem.transform.SetParent (target, false);
			listItem.Prime (structure);
			listItem.gameObject.tag = "StructureDisplay";
			structureDisplayList.Add (listItem);
			listItem.onDelete += onDeleteStructureType;
		}

		ListUpdate ();
	}

	void onDeleteStructureType (StructureType _structureType)
	{
		if (structureTypes.Contains (_structureType))
			structureTypes.Remove (_structureType);

		Prime (structureTypes);
	}

	public void AddStructureType ()
	{
		TypeSelect = (StructureTypeListDisplay)Instantiate (structureTypeListPrefab);
		TypeSelect.Prime (availableStructureTypes);
		TypeSelect.onClick += onTypeSelect;
		TypeSelect.onClose += closeTypeSelect;

	}

	void closeTypeSelect ()
	{
		TypeSelect.onClick -= onTypeSelect;
		TypeSelect.onClose -= closeTypeSelect;
		TypeSelect.destroy ();
	}

	void onTypeSelect (StructureType _structureType)
	{
		if (structureTypes == null)
			structureTypes = new List<StructureType> ();
		
		if (!structureTypes.Contains (_structureType))
			structureTypes.Add (_structureType);

		TypeSelect.onClick -= onTypeSelect;
		TypeSelect.onClose -= closeTypeSelect;
		TypeSelect.destroy ();
		Prime (structureTypes);


	}

	void clearList ()
	{
		foreach (var item in structureDisplayList)
		{
			item.onDelete -= onDeleteStructureType;
		}

		for (int i = 0; i < target.childCount; i++)
		{
			if (target.GetChild (i).gameObject.tag == "StructureDisplay")
			{
				Destroy (target.GetChild (i).gameObject);
			}
		}

	}

	void Awake ()
	{
		availableStructureTypes = new List<StructureType> ();
		availableStructureTypes.AddRange (register.structureRegister.MasterList);
	}



}
