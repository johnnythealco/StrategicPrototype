using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JK.GameData;


namespace JK
{
	namespace View
	{


		public class StructureDisplay : MonoBehaviour
		{

			public Text displayname;
			public Text description;
			public Image icon;
			public Image background;
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
				if (background != null)
					background.color = structureType.backgroundColour;
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
	}
}
