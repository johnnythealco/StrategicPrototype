using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StructureTypeListEditor : MonoBehaviour
{
	protected List<StructureType> workingList;
	protected List<StructureType> mainList;

	public StructureTypeEditor structureTypeEditor;
	public StructureTypeListDisplay structureListDisplay;
	public GameObject navagationBar;

	StructureType newStructureType;
	StructureType selectedStructureType;
	StructureType backupStructureType;

	public void Prime (List<StructureType> _structureTypeList)
	{	

		mainList = _structureTypeList;
		workingList = _structureTypeList;
		structureTypeEditor.gameObject.SetActive (false);
		structureListDisplay.gameObject.SetActive (true);
		structureListDisplay.Prime (workingList);
		structureListDisplay.onClick += onClickStructure;
		structureListDisplay.onFilter += onFilterStructureList;
		navagationBar.SetActive (false);
	}

	void onFilterStructureList (List<StructureType> _structureTypes)
	{			
		workingList = _structureTypes;
	}


	void onClickStructure (StructureType _structureType)
	{
		selectedStructureType = _structureType;
		var index = workingList.IndexOf (_structureType);
		structureListDisplay.gameObject.SetActive (false);
		structureTypeEditor.gameObject.SetActive (true);
		structureTypeEditor.Prime (workingList [index]);
		navagationBar.SetActive (true);
		structureTypeEditor.onUpdateType += onUpdateStructureType;
	}

	void onUpdateStructureType (StructureType _structure)
	{

	}

	public void CreateStructureType ()
	{
		newStructureType = new StructureType ();	
		workingList.Add (newStructureType);
		structureListDisplay.gameObject.SetActive (false);	
		structureTypeEditor.gameObject.SetActive (true);	

		structureTypeEditor.Prime (workingList [workingList.Count () - 1]);
		selectedStructureType = workingList [workingList.Count () - 1];
		navagationBar.SetActive (true);


	}

	public void Previous ()
	{
		var index = workingList.IndexOf (selectedStructureType);

		if (index > 0)
		{
			structureTypeEditor.Prime (workingList [index - 1]);
			selectedStructureType = workingList [index - 1];
		} else
		{
			structureTypeEditor.Prime (workingList [workingList.Count () - 1]);
			selectedStructureType = workingList [workingList.Count () - 1];
		}
	}

	public void Next ()
	{
		var index = workingList.IndexOf (selectedStructureType);

		if (index < workingList.Count () - 1)
		{
			structureTypeEditor.Prime (workingList [index + 1]);
			selectedStructureType = workingList [index + 1];

		} else
		{
			structureTypeEditor.Prime (workingList [0]);
			selectedStructureType = workingList [0];
		}

	}

	public void Cancel ()
	{
		structureTypeEditor.gameObject.SetActive (false);
		structureListDisplay.gameObject.SetActive (true);
		structureListDisplay.Prime (workingList);
		navagationBar.SetActive (false);
	}

	public void Complete ()
	{		
		structureTypeEditor.gameObject.SetActive (false);
		structureListDisplay.gameObject.SetActive (true);
		structureListDisplay.Prime (workingList);
		navagationBar.SetActive (false);

		foreach (var item in workingList)
		{
			if (!mainList.Contains (item))
				mainList.Add (item);
		}

	}

	public void Delete ()
	{		
		var index = workingList.IndexOf (selectedStructureType);
		workingList.Remove (selectedStructureType);

		if (mainList.Contains (selectedStructureType))
			mainList.Remove (selectedStructureType);

		if (index < workingList.Count () - 1)
		{
			structureTypeEditor.Prime (workingList [index + 1]);
			selectedStructureType = workingList [index + 1];

		} else
		{
			structureTypeEditor.Prime (workingList [0]);
			selectedStructureType = workingList [0];
		}


	}

	void clearList ()
	{
		structureListDisplay.onClick -= onClickStructure;
		structureListDisplay.onFilter -= onFilterStructureList;
	}

	void OnDestroy ()
	{
		clearList ();
	}



}
