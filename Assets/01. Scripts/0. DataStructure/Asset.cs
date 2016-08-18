using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace JK
{
	namespace GameData
	{


		[System.Serializable]
		public class Asset : System.Object
		{
			public string name;
			public string descriptions;
			public Sprite smallImage;
			public Sprite largeImage;
			public Color32 backgroundColour = new Color (221, 221, 221, 255);



		}

	}
}

