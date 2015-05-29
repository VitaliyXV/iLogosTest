using UnityEngine;
using strange.extensions.command.impl;

public class ExitCommand : Command
{
	[Inject]
	public IMainMenuManager manager { get; set; }

	public override void Execute()
	{
		manager.Exit();
	}
}
