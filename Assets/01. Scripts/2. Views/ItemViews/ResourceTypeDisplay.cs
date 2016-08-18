using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using JK.GameData;


namespace JK
{
	namespace View
	{


		public class ResourceTypeDisplay : MonoBehaviour
		{

			protected ResourceType resourceType;

			public Text displayname;
			public Text description;
			public Image icon;
			public Image background;
			public Text Category;
			public Text level;


			#region Delegates & Events

			public delegate void ResourceTypeDisplayDelegate (ResourceType _resource);

			public event ResourceTypeDisplayDelegate onClick;

			public event ResourceTypeDisplayDelegate onDelete;



			#endregion

			public void Prime (ResourceType _resourceType)
			{
				resourceType = _resourceType;

				if (displayname != null)
					displayname.text = resourceType.name;
				if (description != null)
					description.text = resourceType.descriptions; 
				if (icon != null)
					icon.sprite = resourceType.smallImage;
				if (Category != null)
					Category.text = resourceType.Category.ToString (); 		
				if (level != null)
					level.text = resourceType.level.ToString ();
				if (background != null)
					background.color = resourceType.backgroundColour;
			}




			public void Click ()
			{
				if (onClick != null)
					onClick.Invoke (resourceType);
			}


			public void DeleteResourceType ()
			{
				if (onDelete != null)
					onDelete.Invoke (resourceType);
			}



		}
	}
}
