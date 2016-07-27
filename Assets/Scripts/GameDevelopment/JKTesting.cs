using UnityEngine;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System;
=======
>>>>>>> origin/master

public class JKTesting : MonoBehaviour
{
	public Register register;

	public ResourceTypeListEditor resourceTypeListEditor;


	void Start ()
	{
		var resources = register.resourceTypeRegister.MasterList; 
		resourceTypeListEditor.Prime (resources);
<<<<<<< HEAD
	
=======
>>>>>>> origin/master

	}




<<<<<<< HEAD

=======
>>>>>>> origin/master
}
