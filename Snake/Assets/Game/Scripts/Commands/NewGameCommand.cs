using UnityEngine;
using strange.extensions.command.impl;

public class NewGameCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }
	
	public override void Execute()
	{
		manager.StartNewGame();
	}
}
