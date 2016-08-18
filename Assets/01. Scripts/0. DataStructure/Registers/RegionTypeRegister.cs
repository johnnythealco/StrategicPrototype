using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace JK
{
	namespace GameData
	{


		public class RegionTypeRegister : ScriptableObject
		{
			public Sprite defaultIcon;


			public List<RegionType> MasterList = new List<RegionType> ();

			public RegionType getRegionType (string _name)
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

			public List<RegionType> getRegionTypes (List<string> _regionNames)
			{
				var result = new List<RegionType> ();
				foreach (var item in _regionNames)
				{
					var _regionType = getRegionType (item);
					if (_regionType != null)
						result.Add (_regionType);
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

	}
}
