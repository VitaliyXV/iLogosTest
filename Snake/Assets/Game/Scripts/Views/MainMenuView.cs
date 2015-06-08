using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class MainMenuView : View
{
	public Button CameraButton;
	public Button FieldButton;
	public Button FacebookButton;
	public InputField PlayerNameInput;
	public InputField FieldHeightInput;
	public InputField FieldWidthInput;
	
	public Signal<Vector2> buttonNewGameClicked = new Signal<Vector2>();
	public Signal buttonCameraClicked = new Signal();
	public Signal buttonFieldTypeClicked = new Signal();
	public Signal buttonExitClicked = new Signal();
	public Signal buttonFacebookClicked = new Signal();
	public Signal facebookLoggedSignal = new Signal();
	public Signal<string> inputPlayerNameChanged = new Signal<string>();
	
	private Text cameraButtonText;
	private Text fieldButtonText;
	private List<Text> playerInputText;
	private List<Text> fieldHeightText;
	private List<Text> fieldWidthText;

	protected override void Start()
	{
		base.Start();

		cameraButtonText = CameraButton.GetComponentInChildren<Text>();
		fieldButtonText = FieldButton.GetComponentInChildren<Text>();
		playerInputText = PlayerNameInput.GetComponentsInChildren<Text>().ToList();
		fieldHeightText = FieldHeightInput.GetComponentsInChildren<Text>().ToList();
		fieldWidthText = FieldWidthInput.GetComponentsInChildren<Text>().ToList();

		UpdatePlayerName();
	}

	public void Initialize()
	{
		Debug.Log("View initialized");
	}

	/// <summary>
	/// Start new game
	/// </summary>
	public void ButtonNewGameClicked()
	{
		int fieldWidth = 10;
		int fieldHeight = 10;

		var h = fieldHeightText.FirstOrDefault().text;
		var w = fieldWidthText.FirstOrDefault().text;

		if (!int.TryParse(fieldHeightText.LastOrDefault().text, out fieldHeight) || fieldHeight < 3) fieldHeight = 10;
		if (!int.TryParse(fieldWidthText.LastOrDefault().text, out fieldWidth) || fieldWidth < 3) fieldWidth = 10;

		if (fieldHeight > 100) fieldHeight = 100;
		if (fieldWidth > 100) fieldWidth = 100;

		var size = new Vector2(fieldWidth, fieldHeight);

		Debug.Log("Set size: " + size);

		buttonNewGameClicked.Dispatch(size);
	}

	/// <summary>
	/// Switch camera type
	/// </summary>
	public void ButtonCameraClicked()
	{		
		buttonCameraClicked.Dispatch();
		cameraButtonText.text = GameData.CurrentCameraType.ToString();
	}

	/// <summary>
	/// switch field type
	/// </summary>
	public void ButtonFieldTypeClicked()
	{		
		buttonFieldTypeClicked.Dispatch();
		fieldButtonText.text = GameData.CurrentFieldType.ToString();
	}

	/// <summary>
	/// Exit from game
	/// </summary>
	public void ButtonExitClicked()
	{
		buttonExitClicked.Dispatch();
	}

	/// <summary>
	/// Now, it just get first name from facebook
	/// </summary>
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