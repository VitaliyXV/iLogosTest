using UnityEngine;
using System.Collections;

/// <summary>
/// General game class
/// </summary>
/// <remarks>Singleton pattern</remarks>
public class Game 
{
	public static Game Instance { get { return instance; } }

	public GameState State { get; set; }

	private static Game instance = new Game();
	private Game() { }
}
