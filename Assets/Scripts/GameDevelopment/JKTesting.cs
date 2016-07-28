using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JKTesting : MonoBehaviour
{
	public Register register;

	public StructureTypeListEditor ListEditor;


	void Start ()
	{
		var list = register.structureRegister.MasterList; 
		ListEditor.Prime (list);
	

	}





}
