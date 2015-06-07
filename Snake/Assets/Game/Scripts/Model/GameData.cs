using strange.extensions.signal.impl;
using UnityEngine;

public static class GameData
{
	public static Signal<string> playerNameChanged = new Signal<string>();
	public static CameraType CurrentCameraType { get; set; }
	public static FieldType CurrentFieldType { get; set; }
	public static int FieldHeight { get; set; }
	public static int FieldWeight { get; set; }
	public static Player Player { get; set; }
	public static int PointsByFood { get; set; }
	public static int LifeCount { get; set; }
	public static int SnakeLength { get; set; }
	public static int CurrentPoints { get; set; }

	static GameData()
	{
		Player = new Player();
		PointsByFood = 100;
		LifeCount = 3;
	}	
}
