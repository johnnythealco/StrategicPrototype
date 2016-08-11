using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StructureRegister : ScriptableObject
{
	public Sprite defaultIcon;

	public List<StructureType> MasterList = new List<StructureType> ();

	public StructureType getStructureType (string _name)
	{
		foreach (var item in MasterList)
		{
			if (item.name == _name)
			{
				return item;
			}
		}

		return null;
	}

	public List<StructureType> getStructureTypes (List<string> _structureNames)
	{
		var result = new List<StructureType> ();
		foreach (var item in _structureNames)
		{
			var _structureType = getStructureType (item);
			if (_structureType != null)
				result.Add (_structureType);
		}
		return result;
	}

	public void SetDefaultIcon ()
	{
		if (defaultIcon == null)
			return;

		foreach (var item in MasterList)
		{
			if (item.smallImage == null)
			{
				item.smallImage = defaultIcon;
			}

			if (item.largeImage == null)
			{
				item.largeImage = defaultIcon;
			}
		}
	}

	void OnEnable ()
	{

		SetDefaultIcon ();
	}



}



