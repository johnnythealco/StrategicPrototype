using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JK.GameData;


namespace JK
{
	namespace View
	{
		

		public class RegionDisplay : MonoBehaviour
		{
			public Text displayname;
			public Text description;
			public Image icon;
			public Image background;


			protected RegionType regionType;

			public delegate void RegionDisplayClick (RegionType _regionType);

			public event RegionDisplayClick onClick;

			public delegate void RegionTypeDelete (RegionType _regionType);

			public event RegionTypeDelete onDelete;



			public void Prime (RegionType _regionType)
			{
				regionType = _regionType; 

				if (displayname != null)
					displayname.text = regionType.name;
				if (description != null)
					description.text = regionType.descriptions;
				if (icon != null)
					icon.sprite = regionType.smallImage;
				if (background != null)
					background.color = regionType.backgroundColour;
			}

			public void Click ()
			{
				if (onClick != null)
					onClick.Invoke (regionType);
			}

			public void Delete ()
			{
				if (onDelete != null)
					onDelete.Invoke (regionType);
			}




		}
	}
}
