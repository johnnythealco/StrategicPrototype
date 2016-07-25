using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JKTesting : MonoBehaviour
{
	public Register register;

	public ResourceTypeListEditor resourceTypeListEditor;


	void Start ()
	{
		var resources = register.resourceTypeRegister.MasterList; 
		resourceTypeListEditor.Prime (resources);

	}




}
