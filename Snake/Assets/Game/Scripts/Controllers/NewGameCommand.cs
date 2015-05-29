using UnityEngine;
using strange.extensions.command.impl;

public class NewGameCommand : Command
{
	public override void Execute()
	{
		Debug.Log("New game");
	}
}
