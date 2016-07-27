using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StructureTypeListDisplay : MonoBehaviour
{
	public Transform target;
	public StructureDisplay structureDisplayPrefab;

	protected List<StructureType> structureTypes;
	protected List<StructureDisplay> structureDisplays;

	#region Delegates & Events

	public delegate void StructureTypeDisplayClick (StructureType _structureType);

	public event StructureTypeDisplayClick onClick;

	public delegate void StructureTypeListDisplayClose ();

	public event StructureTypeListDisplayClose onClose;

	public void Close ()
	{
		if (onClose != null)
			onClose.Invoke ();
	}



	#endregion

	public void Prime (List<StructureType> _structureTypes)
	{
		structureTypes = _structureTypes;

		clearList ();


		foreach (var structure in structureTypes)
		{
			StructureDisplay listItem = (StructureDisplay)Instantiate (structureDisplayPrefab); 
			listItem.transform.SetParent (target, false);
			listItem.Prime (structure);
			listItem.gameObject.tag = "StructureDisplay";
			listItem.onClick += onClickListItem;


			structureDisplays.Add (listItem);		
		}
		
	}

	void onClickListItem (StructureType _structureType)
	{
		if (onClick != null)
			onClick.Invoke (_structureType);
			
	}

	void clearList ()
	{
		if (structureDisplays == null)
			structureDisplays = new List<StructureDisplay> ();
		
		foreach (var item in structureDisplays)
		{
			item.onClick -= onClickListItem;
	
		}

		for (int i = 0; i < target.childCount; i++)
		{
			if (target.GetChild (i).gameObject.tag == "StructureDisplay")
			{
				Destroy (target.GetChild (i).gameObject);
			}
		}
	}

	public void destroy ()
	{
		clearList ();
		Destroy (gameObject);
		
	}





}
