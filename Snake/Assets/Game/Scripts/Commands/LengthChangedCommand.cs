using UnityEngine;
using strange.extensions.command.impl;

public class LengthChangedCommand : Command
{
	[Inject]
	public int length { get; set; }

	public override void Execute()
	{
		Debug.Log("Length: " + length);
	}
}
