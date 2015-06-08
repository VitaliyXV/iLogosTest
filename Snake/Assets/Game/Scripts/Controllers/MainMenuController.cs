﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IMainMenuController
{
	[SerializeField]
	private int snakeSpriteCount = 5;

	[Inject]
	public ISocialProvider SocialProvider { get; set; }

	public GameObject SnakeSprite;
	
	public void Initialize()
	{
		Debug.Log("Initialized");

		GameData.ResetGameData();

		// load Player data from preferences
		GameData.Player = LocalDataProvider.Instance.GetPlayer();	

		RunSnakes();
	}

	public void StartNewGame(Vector2 fieldSize)
	{
		Debug.Log("New game: " + fieldSize.y + ", " + fieldSize.x);

		GameData.FieldHeight = (int)fieldSize.y;
		GameData.FieldWeight = (int)fieldSize.x;

		Application.LoadLevel("Game");
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

	public void JoinWithFacebook()
	{
		Debug.Log("FACEBOOK");
		SocialProvider.ConnectToSocial();
	}
	
	private void RunSnakes()
	{
		for (int i = 0; i < snakeSpriteCount; i++)
		{
			var snake = Instantiate(SnakeSprite, new Vector3(-9, -4, 0), Quaternion.identity) as GameObject;
			snake.transform.SetParent(transform);
		}
	}
}
