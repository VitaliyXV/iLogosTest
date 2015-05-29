using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;


public class MainMenuView : View
{
	public Signal buttonNewGameClicked = new Signal();
	public Signal buttonCameraClicked = new Signal();
	public Signal buttonFieldTypeClicked = new Signal();
	public Signal buttonExitClicked = new Signal();

	public void ButtonNewGameClicked()
	{
		buttonNewGameClicked.Dispatch();
	}

	public void ButtonCameraClicked()
	{
		buttonCameraClicked.Dispatch();
	}

	public void ButtonFieldTypeClicked()
	{
		buttonFieldTypeClicked.Dispatch();
	}

	public void ButtonExitClicked()
	{
		buttonExitClicked.Dispatch();
	}
}