using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class MainMenuView : View
{
	public Button cameraButton;
	public Button fieldButton;

	private Text cameraButtonText;
	private Text fieldButtonText;

	public Signal buttonNewGameClicked = new Signal();
	public Signal buttonCameraClicked = new Signal();
	public Signal buttonFieldTypeClicked = new Signal();
	public Signal buttonExitClicked = new Signal();
	

	protected override void Start()
	{
		base.Start();

		cameraButtonText = cameraButton.GetComponentInChildren<Text>();
		fieldButtonText = fieldButton.GetComponentInChildren<Text>();
	}

	public void ButtonNewGameClicked()
	{
		buttonNewGameClicked.Dispatch();
	}

	public void ButtonCameraClicked()
	{		
		buttonCameraClicked.Dispatch();
		cameraButtonText.text = GameData.CurrentCameraType.ToString();
	}

	public void ButtonFieldTypeClicked()
	{		
		buttonFieldTypeClicked.Dispatch();
		fieldButtonText.text = GameData.CurrentFieldType.ToString();
	}

	public void ButtonExitClicked()
	{
		buttonExitClicked.Dispatch();
	}
}