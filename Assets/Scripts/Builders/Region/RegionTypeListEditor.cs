using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RegionTypeListEditor : MonoBehaviour
{


	protected List<RegionType> regionTypeList;

	public RegionTypeEditor regionTypeEditor;
	public RegionTypeListDisplay regionListDisplay;
	public GameObject navagationBar;

	RegionType newRegionType;
	RegionType selectedRegionType;


	public void Prime (List<RegionType> _regionTypeList)
	{
		regionTypeList = _regionTypeList;
		regionTypeEditor.gameObject.SetActive (false);
		regionListDisplay.gameObject.SetActive (true);
		regionListDisplay.Prime (regionTypeList);
		regionListDisplay.onClick += onClickRegion;
		navagationBar.SetActive (false);


	}

	void onClickRegion (RegionType _regionType)
	{
		selectedRegionType = _regionType;
		var index = regionTypeList.IndexOf (_regionType);
		regionListDisplay.gameObject.SetActive (false);
		regionTypeEditor.gameObject.SetActive (true);
		regionTypeEditor.Prime (regionTypeList [index]);
		navagationBar.SetActive (true);
	}

	public void CreateRegionType ()
	{
		newRegionType = new RegionType ();	
		regionTypeList.Add (newRegionType);
		regionListDisplay.gameObject.SetActive (false);	
		regionTypeEditor.gameObject.SetActive (true);
		navagationBar.gameObject.SetActive (true);
		regionTypeEditor.Prime (regionTypeList [regionTypeList.Count () - 1]);
	

	}

	public void Previous ()
	{
		var index = regionTypeList.IndexOf (selectedRegionType);

		if (index > 0)
		{
			regionTypeEditor.Prime (regionTypeList [index - 1]);
			selectedRegionType = regionTypeList [index - 1];
		} else
		{
			regionTypeEditor.Prime (regionTypeList [regionTypeList.Count () - 1]);
			selectedRegionType = regionTypeList [regionTypeList.Count () - 1];
		}
	}

	public void Next ()
	{
		var index = regionTypeList.IndexOf (selectedRegionType);

		if (index < regionTypeList.Count () - 1)
		{
			regionTypeEditor.Prime (regionTypeList [index + 1]);
			selectedRegionType = regionTypeList [index + 1];

		} else
		{
			regionTypeEditor.Prime (regionTypeList [0]);
			selectedRegionType = regionTypeList [0];
		}

	}

	public void Cancel ()
	{

		regionTypeEditor.gameObject.SetActive (false);
		navagationBar.SetActive (false);
		regionListDisplay.Prime (regionTypeList);
		regionListDisplay.gameObject.SetActive (true);

	}

	public void Complete ()
	{		
		regionTypeEditor.gameObject.SetActive (false);
		navagationBar.SetActive (false);
		regionListDisplay.Prime (regionTypeList);
		regionListDisplay.gameObject.SetActive (true);

	}

	public void Delete ()
	{		
		var index = regionTypeList.IndexOf (selectedRegionType);
		regionTypeList.Remove (selectedRegionType);

		if (index < regionTypeList.Count () - 1)
		{
			regionTypeEditor.Prime (regionTypeList [index + 1]);
			selectedRegionType = regionTypeList [index + 1];

		} else
		{
			regionTypeEditor.Prime (regionTypeList [0]);
			selectedRegionType = regionTypeList [0];
		}

		
	}




}
