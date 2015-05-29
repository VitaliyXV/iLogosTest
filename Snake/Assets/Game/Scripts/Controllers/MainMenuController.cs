using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IMainMenuController
{
	void Awake()
	{
		// load Player data from preferences
		GameData.Player = LocalDataProvider.Instance.GetPlayer();
	}
	
#region IMainMenuManager	

	public void StartNewGame()
	{
		Debug.Log("New game");
	}

	public void SwitchCamera()
	{
		var type = (int)GameData.CurrentCameraType + 1;
		if (type >= Enum.GetNames(typeof(CameraType)).Length) type = 0;
		GameData.CurrentCameraType = (CameraType)type;
	}

	public void SwitchFieldType()
	{
		var type = (int)GameData.CurrentFieldType + 1;
		if (type >= Enum.GetNames(typeof(FieldType)).Length) type = 0;
		GameData.CurrentFieldType = (FieldType)type;
	}
	
	public void InputPlayerName(string name)
	{
		GameData.Player.Name = name;
		LocalDataProvider.Instance.SavePlayer(GameData.Player);
	}

	public void Exit()
	{
		Debug.Log("Exit");
		Application.Quit();
	}

#endregion
	
}
