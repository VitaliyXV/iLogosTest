using strange.extensions.signal.impl;
using UnityEngine;

public static class GameData
{
	public static CameraType CurrentCameraType { get; set; }
	public static FieldType CurrentFieldType { get; set; }

	public static int FieldHeight { get; set; }
	public static int FieldWeight { get; set; }

	public static Player Player { get; set; }

	public static int PointsByFood { get; private set; }
	public static int LifeCount { get; private set; }
	public static int SnakeLength { get; private set; }
	public static int CurrentPoints { get; private set; }

	static GameData()
	{
		Player = new Player();
		PointsByFood = 100;
		ResetGameData();
	}

	public static void ResetGameData()
	{
		LifeCount = 3;
		SnakeLength = 1;
		CurrentPoints = 0;
	}

	public static void DecreaseLifes()
	{
		LifeCount--;
	}

	public static void IncreaseSnake()
	{
		SnakeLength++;
		CurrentPoints += PointsByFood;
	}
}
