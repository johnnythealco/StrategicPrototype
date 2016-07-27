using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegionTypeListDisplay : MonoBehaviour
{
	public Transform target;
	public RegionDisplay regionDisplayPrefab;

	protected List<RegionType> regionTypes;
	protected List<RegionDisplay> regionDisplays;

	#region Delegates & Events

	public delegate void RegionTypeDisplayClick (RegionType _regionType);

	public event RegionTypeDisplayClick onClick;

	public delegate void RegionTypeListDisplayClose ();

	public event RegionTypeListDisplayClose onClose;

	public void Close ()
	{
		if (onClose != null)
			onClose.Invoke ();
	}


	#endregion

	public void Prime (List<RegionType> _regionTypes)
	{
		regionTypes = _regionTypes;

		clearList ();


		foreach (var structure in regionTypes)
		{
			RegionDisplay listItem = (RegionDisplay)Instantiate (regionDisplayPrefab); 
			listItem.transform.SetParent (target, false);
			listItem.Prime (structure);
			listItem.gameObject.tag = "RegionDisplay";
			listItem.onClick += onClickListItem;


			regionDisplays.Add (listItem);		
		}

	}

	void onClickListItem (RegionType _regionType)
	{
		if (onClick != null)
			onClick.Invoke (_regionType);

	}

	void clearList ()
	{
		if (regionDisplays == null)
			regionDisplays = new List<RegionDisplay> ();

		foreach (var item in regionDisplays)
		{
			item.onClick -= onClickListItem;

		}

		for (int i = 0; i < target.childCount; i++)
		{
			if (target.GetChild (i).gameObject.tag == "RegionDisplay")
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
