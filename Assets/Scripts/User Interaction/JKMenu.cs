using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JKMenu : MonoBehaviour
{
	public Transform fileMenu;
	public Transform resourceMenu;
	public Transform structureMenu;
	public Transform regionMenu;
	public CanvasGroup canvasGroup;

	Transform activeMenu;


	// Use this for initialization
	void Start ()
	{
		fileMenu.gameObject.SetActive (false);
		resourceMenu.gameObject.SetActive (false);
		structureMenu.gameObject.SetActive (false);
		regionMenu.gameObject.SetActive (false);
		canvasGroup.blocksRaycasts = false;
	}


	public void toggleFileMenu ()
	{
		if (fileMenu.gameObject.activeInHierarchy)
		{
			fileMenu.gameObject.SetActive (false);
			activeMenu = null;
			canvasGroup.blocksRaycasts = false;
		} else
		{
			hideActiveMenu ();
			fileMenu.gameObject.SetActive (true);
			activeMenu = fileMenu;
			canvasGroup.blocksRaycasts = true;
		}
	}

	public void toggleResourceMenu ()
	{
		if (resourceMenu.gameObject.activeInHierarchy)
		{
			resourceMenu.gameObject.SetActive (false);
			activeMenu = null;
			canvasGroup.blocksRaycasts = false;
		} else
		{
			hideActiveMenu ();
			resourceMenu.gameObject.SetActive (true);
			activeMenu = resourceMenu;
			canvasGroup.blocksRaycasts = true;
		}
	}

	public void hideActiveMenu ()
	{
		if (activeMenu != null)
		{
			activeMenu.gameObject.SetActive (false);
			activeMenu = null;
			canvasGroup.blocksRaycasts = false;
		}
	}

	public void toggleStructureMenu ()
	{
		if (structureMenu.gameObject.activeInHierarchy)
		{
			structureMenu.gameObject.SetActive (false);
			activeMenu = null;
			canvasGroup.blocksRaycasts = false;
		} else
		{
			hideActiveMenu ();
			structureMenu.gameObject.SetActive (true);
			activeMenu = structureMenu;
			canvasGroup.blocksRaycasts = true;
		}
	}

	public void toggleRegionMenu ()
	{
		if (regionMenu.gameObject.activeInHierarchy)
		{
			regionMenu.gameObject.SetActive (false);
			activeMenu = null;
			canvasGroup.blocksRaycasts = false;
		} else
		{
			hideActiveMenu ();
			regionMenu.gameObject.SetActive (true);
			activeMenu = regionMenu;
			canvasGroup.blocksRaycasts = true;
		}
	}



}
