using UnityEngine;
using strange.extensions.command.impl;

public class NewGameCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }

	[Inject]
	public Vector2 FieldSize { get; set; }
	
	public override void Execute()
	{
		manager.StartNewGame(FieldSize);
	}
}
