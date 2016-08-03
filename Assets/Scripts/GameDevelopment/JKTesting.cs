using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JKTesting : MonoBehaviour
{
	public Register register;

	public StructureTypeListEditor structureList;
	public RegionTypeListEditor regionList;


	void Start ()
	{
		var structures = register.structureRegister.MasterList;
		var regions = register.regionTypeRegister.MasterList; 
		structureList.Prime (structures);
		regionList.Prime (regions);
	}





}
