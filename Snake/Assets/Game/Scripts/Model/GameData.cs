using UnityEngine;

public static class GameData
{
	public static CameraType CurrentCameraType { get; set; }
	public static FieldType CurrentFieldType { get; set; }
	public static Player Player { get; set; }

	static GameData()
	{
		
	}
}
