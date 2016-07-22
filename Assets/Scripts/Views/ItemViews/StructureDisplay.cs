using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StructureDisplay : MonoBehaviour
{

	public Text displayname;
	public Text description;
	public Image icon;
	public Dropdown category;
	public Text cost;
	public Text influence;

	protected StructureType structureType;

	public delegate void StructureDisplayClick (StructureType _structureType);

	public event StructureDisplayClick onClick;

	public delegate void StructureDisplayDelete (StructureType _structureType);

	public event StructureDisplayDelete onDelete;



	public void Prime (StructureType _structureType)
	{
		structureType = _structureType; 

		if (displayname != null)
			displayname.text = structureType.name;
		if (description != null)
			description.text = structureType.descriptions;
		if (icon != null)
			icon.sprite = structureType.smallImage;
		if (cost != null)
			cost.text = structureType.cost.ToString ();
		if (influence != null)
			influence.text = structureType.influence.ToString ();
		

	}

	public void Click ()
	{
		if (onClick != null)
			onClick.Invoke (structureType);
	}

	public void Delete ()
	{
		if (onDelete != null)
			onDelete.Invoke (structureType);
	}




}
