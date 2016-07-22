using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StructureTypeListEditor : MonoBehaviour
{
	
	protected List<StructureType> structureTypeList;

	public StructureTypeEditor structureTypeEditor;
	public StructureTypeListDisplay structureListDisplay;

	StructureType newStructureType;

	public void Prime (List<StructureType> _structureTypeList)
	{
		structureTypeList = _structureTypeList;
		structureListDisplay.gameObject.SetActive (true);
		structureListDisplay.Prime (structureTypeList);
		structureListDisplay.onClick += onClickStructure;

		
	}

	void onClickStructure (StructureType _structureType)
	{
		var index = structureTypeList.IndexOf (_structureType);

		structureListDisplay.gameObject.SetActive (false);
		structureTypeEditor.gameObject.SetActive (true);

		structureTypeEditor.Prime (structureTypeList [index]);

		structureTypeEditor.onComplete += StructureTypeEditor_onComplete;
		structureTypeEditor.onCancel += StructureTypeEditor_onCancel;
		structureTypeEditor.onNext += StructureTypeEditor_onNext;
		structureTypeEditor.onPrevious += StructureTypeEditor_onPrevious;
	}

	//	public void CreateStructureType ()
	//	{
	//		newStructureType = new StructureType ();
	//
	//		structureTypeList.Add (newStructureType);
	//		structureListDisplayPrefab.gameObject.SetActive (false);
	//
	//		structureTypeEditor = (StructureTypeEditor)Instantiate (structureTypeEditorPrefab);
	//
	//		structureTypeEditor.Prime (structureTypeList [structureTypeList.Count () - 1]);
	//
	//		structureTypeEditor.onComplete += StructureTypeEditor_onComplete;
	//		structureTypeEditor.onCancel += StructureTypeEditor_onCancel;
	//		structureTypeEditor.onNext += StructureTypeEditor_onNext;
	//		structureTypeEditor.onPrevious += StructureTypeEditor_onPrevious;
	//	}

	void StructureTypeEditor_onPrevious (StructureType _structureType)
	{
		var index = structureTypeList.IndexOf (_structureType);

		if (index > 0)
			structureTypeEditor.Prime (structureTypeList [index - 1]);
		else
			structureTypeEditor.Prime (structureTypeList [structureTypeList.Count () - 1]);
	}

	void StructureTypeEditor_onNext (StructureType _structureType)
	{
		var index = structureTypeList.IndexOf (_structureType);

		if (index < structureTypeList.Count () - 1)
			structureTypeEditor.Prime (structureTypeList [index + 1]);
		else
			structureTypeEditor.Prime (structureTypeList [0]);

	}

	void StructureTypeEditor_onCancel (StructureType _structureType)
	{

		structureListDisplay.gameObject.SetActive (false);
		structureTypeEditor.onComplete -= StructureTypeEditor_onComplete;
		structureTypeEditor.onCancel -= StructureTypeEditor_onCancel;
		structureTypeEditor.onNext -= StructureTypeEditor_onNext;
		structureTypeEditor.onPrevious -= StructureTypeEditor_onPrevious;
		structureListDisplay.Prime (structureTypeList);
		structureListDisplay.gameObject.SetActive (true);

	}

	void StructureTypeEditor_onComplete (StructureType _structureType)
	{		
		structureTypeEditor.gameObject.SetActive (false);
		structureTypeEditor.onComplete -= StructureTypeEditor_onComplete;
		structureTypeEditor.onCancel -= StructureTypeEditor_onCancel;
		structureTypeEditor.onNext -= StructureTypeEditor_onNext;
		structureTypeEditor.onPrevious -= StructureTypeEditor_onPrevious;
		structureListDisplay.Prime (structureTypeList);
		structureListDisplay.gameObject.SetActive (true);

	}





}
