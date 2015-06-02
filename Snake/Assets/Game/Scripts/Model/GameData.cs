using strange.extensions.signal.impl;
using UnityEngine;

public static class GameData
{
	public static Signal<string> playerNameChanged = new Signal<string>();
	public static CameraType CurrentCameraType { get; set; }
	public static FieldType CurrentFieldType { get; set; }
	public static Player Player { get; set; }

	static GameData()
	{
		Player = new Player();
	}
}
