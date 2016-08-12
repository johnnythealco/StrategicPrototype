using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceTypeRegister : ScriptableObject
{
	public Sprite defaultIcon;

	public Sprite coretIcon;
	public Sprite baseIcon;
	public Sprite StrategicIcon;
	public Sprite CommunityIcon;
	public Sprite HealthIcon;
	public Sprite ShelterIcon;
	public Sprite LuxuryIcon;
		
	public List<ResourceType> MasterList = new List<ResourceType> ();

	public List<ResourceType> StrategicResources { get { return getResourceListByCategory (ResourceCategory.Strategic); } }

	List<ResourceType> getResourceListByCategory (ResourceCategory category)
	{
		List<ResourceType> result = new List<ResourceType> ();

		foreach (var item in MasterList)
		{
			if (item.Category == category)
			{
				result.Add (item);
			}
		}

		return result;
	}

	public ResourceType getResourceType (string _name)
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

	public List<ResourceType> getResourceTypes (List<string> _resourceNames)
	{
		var result = new List<ResourceType> ();
		foreach (var item in _resourceNames)
		{
			var _resourceType = getResourceType (item);
			if (_resourceType != null)
				result.Add (_resourceType);
		}
		return result;
	}

	public void SetDefaultIcon ()
	{
		if (defaultIcon == null)
			return;

		foreach (var item in MasterList)
		{
			Sprite icon;
			
			int i = (int)item.Category;

			switch (i)
			{
			case 0:
				{
					icon = coretIcon;
				}
				break;
			case 1:
				{
					icon = baseIcon;
				}
				break;
			case 2:
				{
					icon = StrategicIcon;
				}
				break;
			case 3:
				{
					icon = CommunityIcon;
				}
				break;
			case 4:
				{
					icon = HealthIcon;
				}
				break;
			case 5:
				{
					icon = ShelterIcon;
				}
				break;
			case 6:
				{
					icon = LuxuryIcon;
				}
				break;
			default:
				{
					icon = defaultIcon;
				}
				break;

			}

			if (item.smallImage == null)
			{
				item.smallImage = icon;
			}

			if (item.largeImage == null)
			{
				item.largeImage = icon;
			}
		}
	}

	void OnEnable ()
	{
		
		SetDefaultIcon ();
	}

}