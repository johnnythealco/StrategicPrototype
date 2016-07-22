using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JKTesting : MonoBehaviour
{
	public Register register;

	public RegionTypeListEditor regionTypeListEditor;


	void Start ()
	{
		var regions = register.regionTypeRegister.MasterList; 
		regionTypeListEditor.Prime (regions);

	}




}
