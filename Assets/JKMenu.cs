using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class JKMenu : MonoBehaviour
{
	public Transform fileMenu;
	public Transform resourceMenu;

	Transform activeMenu;


	// Use this for initialization
	void Start ()
	{
		fileMenu.gameObject.SetActive (false);
		resourceMenu.gameObject.SetActive (false);
	}


	public void toggleFileMenu ()
	{
		if (fileMenu.gameObject.activeInHierarchy)
		{
			fileMenu.gameObject.SetActive (false);
			activeMenu = null;
		} else
		{
			hideActiveMenu ();
			fileMenu.gameObject.SetActive (true);
			activeMenu = fileMenu;
		}
	}

	public void toggleResourceMenu ()
	{
		if (resourceMenu.gameObject.activeInHierarchy)
		{
			resourceMenu.gameObject.SetActive (false);
			activeMenu = null;
		} else
		{
			hideActiveMenu ();
			resourceMenu.gameObject.SetActive (true);
			activeMenu = resourceMenu;
		}
	}

	public void hideActiveMenu ()
	{
		if (activeMenu != null)
		{
			activeMenu.gameObject.SetActive (false);
			activeMenu = null;
		}
	}


}
