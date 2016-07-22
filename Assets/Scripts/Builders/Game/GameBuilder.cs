using UnityEngine;
using System.Collections;
using System.Linq;

public class GameBuilder : MonoBehaviour
{
	public Register register;
	public GameBuilder gameBuilder;
	public ResourceBuilder resourceBuilderPrefab;
	public StructureTypeEditor structureEditorPrefab;
	public RegionTypeEditor regionTypeEditorPrefab;
	ResourceBuilder ResourceTypeBuilder;
	StructureTypeEditor structureTypeEditor;
	RegionTypeEditor regionTypeEditor;
	StructureType newStructureType;
	RegionType newRegionType;

	#region Resource Type Creation

	public void CreateResourceType ()
	{
		gameBuilder.gameObject.SetActive (false);
		ResourceTypeBuilder = (ResourceBuilder)Instantiate (resourceBuilderPrefab);

		ResourceTypeBuilder.onComplete += ResourceBuilder_onComplete;
	}


	void ResourceBuilder_onComplete ()
	{
		ResourceTypeBuilder.destroy ();
		ResourceTypeBuilder.onComplete -= ResourceBuilder_onComplete;
		gameBuilder.gameObject.SetActive (true);
	}

	#endregion

	#region Structure Type Creation

	public void CreateStructureType ()
	{
		newStructureType = new StructureType ();
		var StructureTypes = register.structureRegister.MasterList;
		StructureTypes.Add (newStructureType);
		gameBuilder.gameObject.SetActive (false);

		structureTypeEditor = (StructureTypeEditor)Instantiate (structureEditorPrefab);

		structureTypeEditor.Prime (StructureTypes [StructureTypes.Count () - 1]);

		structureTypeEditor.onComplete += StructureTypeEditor_onComplete;
		structureTypeEditor.onCancel += StructureTypeEditor_onCancel;
		structureTypeEditor.onNext += StructureTypeEditor_onNext;
		structureTypeEditor.onPrevious += StructureTypeEditor_onPrevious;
	}

	void StructureTypeEditor_onPrevious (StructureType _structureType)
	{
		var StructureTypes = register.structureRegister.MasterList;
		var index = StructureTypes.IndexOf (_structureType);

		if (index > 0)
			structureTypeEditor.Prime (StructureTypes [index - 1]);
		else
			structureTypeEditor.Prime (StructureTypes [StructureTypes.Count () - 1]);
	}

	void StructureTypeEditor_onNext (StructureType _structureType)
	{
		var StructureTypes = register.structureRegister.MasterList;
		var index = StructureTypes.IndexOf (_structureType);

		if (index < StructureTypes.Count () - 1)
			structureTypeEditor.Prime (StructureTypes [index + 1]);
		else
			structureTypeEditor.Prime (StructureTypes [0]);

	}

	void StructureTypeEditor_onCancel (StructureType _structureType)
	{
		var StructureTypes = register.structureRegister.MasterList;
		StructureTypes.Remove (_structureType);
		structureTypeEditor.destroy ();
		structureTypeEditor.onComplete -= StructureTypeEditor_onComplete;
		structureTypeEditor.onCancel -= StructureTypeEditor_onCancel;
		structureTypeEditor.onNext -= StructureTypeEditor_onNext;
		structureTypeEditor.onPrevious -= StructureTypeEditor_onPrevious;
		gameBuilder.gameObject.SetActive (true);
	}

	void StructureTypeEditor_onComplete (StructureType _structureType)
	{		
		structureTypeEditor.destroy ();
		structureTypeEditor.onComplete -= StructureTypeEditor_onComplete;
		structureTypeEditor.onCancel -= StructureTypeEditor_onCancel;
		structureTypeEditor.onNext -= StructureTypeEditor_onNext;
		structureTypeEditor.onPrevious -= StructureTypeEditor_onPrevious;
		gameBuilder.gameObject.SetActive (true);
	}

	void AddStructureType (StructureType _structureType)
	{
		var structureRegister = register.structureRegister;

		if (StructureType.IsValid (_structureType) && structureRegister.getStructureType (_structureType.name) == null)
		{
			structureRegister.MasterList.Add (_structureType);
		}




	}

	#endregion

	//	public void CreateRegionType ()
	//	{
	//		newRegionType = new RegionType ();
	//		var RegionTypes = register.regionTypeRegister.MasterList;
	//		RegionTypes.Add (newRegionType);
	//		gameBuilder.gameObject.SetActive (false);
	//
	//		regionTypeEditor = (RegionTypeEditor)Instantiate (regionTypeEditorPrefab);
	//		regionTypeEditor.Prime (RegionTypes [RegionTypes.Count () - 1]);
	//
	//		regionTypeEditor.onClickOK += RegionTypeEditor_onClickOK;
	//		regionTypeEditor.onClickCancel += RegionTypeEditor_onClickCancel;
	//		regionTypeEditor.onClickNext += RegionTypeEditor_onClickNext;
	//		regionTypeEditor.onClickPrevious += RegionTypeEditor_onClickPrevious;
	//	}
	//
	//	void RegionTypeEditor_onClickPrevious (RegionType _regiontype)
	//	{
	//		var RegionTypes = register.regionTypeRegister.MasterList;
	//		var index = RegionTypes.IndexOf (_regiontype);
	//
	//		if (index > 0)
	//			regionTypeEditor.Prime (RegionTypes [index - 1]);
	//		else
	//			regionTypeEditor.Prime (RegionTypes [RegionTypes.Count () - 1]);
	//	}
	//
	//	void RegionTypeEditor_onClickNext (RegionType _regiontype)
	//	{
	//		var RegionTypes = register.regionTypeRegister.MasterList;
	//		var index = RegionTypes.IndexOf (_regiontype);
	//
	//		if (index < RegionTypes.Count () - 1)
	//			regionTypeEditor.Prime (RegionTypes [index + 1]);
	//		else
	//			regionTypeEditor.Prime (RegionTypes [0]);
	//	}
	//
	//	void RegionTypeEditor_onClickCancel ()
	//	{
	//		regionTypeEditor.destroy ();
	//		unsubscribe ();
	//		gameBuilder.gameObject.SetActive (true);
	//	}
	//
	//	void RegionTypeEditor_onClickOK ()
	//	{
	//		regionTypeEditor.destroy ();
	//		unsubscribe ();
	//		gameBuilder.gameObject.SetActive (true);
	//	}
	//
	//
	//	void unsubscribe ()
	//	{
	//		regionTypeEditor.onClickOK -= RegionTypeEditor_onClickOK;
	//		regionTypeEditor.onClickCancel -= RegionTypeEditor_onClickCancel;
	//		regionTypeEditor.onClickNext -= RegionTypeEditor_onClickNext;
	//		regionTypeEditor.onClickPrevious -= RegionTypeEditor_onClickPrevious;
	//	}
}
