using UnityEngine;
using strange.extensions.command.impl;

public class ExitCommand : Command
{
	public override void Execute()
	{
		Debug.Log("Exit");
	}
}
