using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoreResourceDisplay : MonoBehaviour
{
	protected CoreResource coreResource;

	public Text credits;
	public Text influence;
	public Text population;
	public Text health;
	public Text prosperity;

	public InputField creditsInput;
	public InputField influenceInput;
	public InputField populationInput;
	public InputField healthInput;
	public InputField prosperityInput;

	#region Delegates & Events

	public delegate void CoreResourceUpdateDelegate (CoreResource _coreResource);

	public event CoreResourceUpdateDelegate onUpdateResource;

	#endregion

	public void Prime (CoreResource _coreResource)
	{

		coreResource = _coreResource;
		//Text Displays
		if (credits != null)
			credits.text = coreResource.credits.ToString ();		
		if (influence != null)
			influence.text = coreResource.influence.ToString (); 		
		if (health != null)
			health.text = coreResource.health.ToString (); 
		if (population != null)
			population.text = coreResource.population.ToString (); 
		if (prosperity != null)
			prosperity.text = coreResource.prosperity.ToString ();

		//Input Field Displays
		if (creditsInput != null)
			creditsInput.text = coreResource.credits.ToString ();	
		if (influenceInput != null)
			influenceInput.text = coreResource.influence.ToString (); 
		if (populationInput != null)
			populationInput.text = coreResource.population.ToString (); 
		if (healthInput != null)
			healthInput.text = coreResource.health.ToString ();
		if (prosperityInput != null)
			prosperityInput.text = coreResource.prosperity.ToString ();



	}

	public void getInput ()
	{

		if (creditsInput != null)
			coreResource.credits = int.Parse (creditsInput.text);	
		if (influenceInput != null)
			coreResource.influence = int.Parse (influenceInput.text); 
		if (prosperityInput != null)
			coreResource.prosperity = int.Parse (prosperityInput.text); 
		if (healthInput != null)
			coreResource.health = int.Parse (healthInput.text);
		if (populationInput != null)
			coreResource.population = int.Parse (populationInput.text);

		if (onUpdateResource != null)
			onUpdateResource.Invoke (coreResource);
	}


}
