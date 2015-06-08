using System;
using UnityEngine;

/// <summary>
/// Class for save game data (locally)
/// Singleton (tecnical requirement)
/// </summary>
public class LocalDataProvider
{
	public static LocalDataProvider Instance { get { return instance; } }

	private static LocalDataProvider instance = new LocalDataProvider();

	public ISerializer Serializer { get; set; }

	private LocalDataProvider()
	{
		Serializer = new XmlSerializator();
	}

	public void SavePlayer(Player player)
	{
		var data = Serializer.Serialize<Player>(player);
		SetValue("Player", data);
	}

	public Player GetPlayer()
	{
		var data = GetValue("Player");
		return string.IsNullOrEmpty(data) ? new Player() : Serializer.Deserialize<Player>(data);
	}

	public string GetValue(string key)
	{
		return PlayerPrefs.GetString(key);
	}

	public void SetValue(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
	}

	public void SavePlayersPreferences()
	{
		PlayerPrefs.Save();
	}

	public void GetPrefab(string name, Action<GameObject> callback)
	{
		callback((GameObject)Resources.Load("Prefabs/" + name));
	}
}
