using UnityEngine;
using strange.extensions.command.impl;

public class LifesChangedCommand : Command
{
	//[Inject]
	//public MainMenuMediator mediator { get; set; }

	[Inject]
	public int lifes { get; set; }

	public override void Execute()
	{
		Debug.Log("Lifes: " + lifes);
		//manager.InputPlayerName(name);
	}
}
