using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public static Game Manager;

	public Register register;


	public List<ResourceType> AllResources{ get { return register.AllResources; } }

	public List<ResourceType> StrategicResources{ get { return register.StrategicResources; } }

	void Awake ()
	{
		if (Manager == null)
		{
			Manager = this;

		} else if (Manager != this)
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

	}


}
