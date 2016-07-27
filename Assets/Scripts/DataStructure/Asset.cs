using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Asset : System.Object
{
	public string name;
	public string descriptions;
	public Sprite smallImage;
	public Sprite largeImage;
}

[System.Serializable]
public class CoreResource
{
	public int credits;
	public int influence;
	public int population;
	public int health;
	public int prosperity;
}