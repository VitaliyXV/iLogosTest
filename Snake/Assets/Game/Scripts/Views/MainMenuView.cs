using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class MainMenuView : View
{
	public Button cameraButton;
	public Button fieldButton;
	public Button facebookButton;
	public InputField playerInput;

	private Text cameraButtonText;
	private Text fieldButtonText;
	private List<Text> playerInputText;

	public Signal buttonNewGameClicked = new Signal();
	public Signal buttonCameraClicked = new Signal();
	public Signal buttonFieldTypeClicked = new Signal();
	public Signal buttonExitClicked = new Signal();
	public Signal buttonFacebookClicked = new Signal();
	public Signal facebookLoggedSignal = new Signal();
	public Signal<string> inputPlayerNameChanged = new Signal<string>();
	

	protected override void Start()
	{
		base.Start();

		cameraButtonText = cameraButton.GetComponentInChildren<Text>();
		fieldButtonText = fieldButton.GetComponentInChildren<Text>();
		playerInputText = playerInput.GetComponentsInChildren<Text>().ToList();

		UpdatePlayerName();
	}

	public void Initialize()
	{
		Debug.Log("View initialized");
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

	public void JoinWithFacebbok()
	{
		buttonFacebookClicked.Dispatch();

		// TODO: set facebook button disabled
	}

	public void PlayerNameEditEnded()
	{
		inputPlayerNameChanged.Dispatch(playerInputText.LastOrDefault().text);
	}

	public void UpdatePlayerName()
	{
		playerInputText.ForEach(t => t.text = GameData.Player.Name);
	}
}