using UnityEngine;
using strange.extensions.command.impl;

public class LengthChangedCommand : Command
{
	//[Inject]
	//public MainMenuMediator mediator { get; set; }

	[Inject]
	public int length { get; set; }

	public override void Execute()
	{
		Debug.Log("Length: " + length);
		//manager.InputPlayerName(name);
	}
}
