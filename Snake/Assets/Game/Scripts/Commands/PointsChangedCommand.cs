using strange.extensions.command.impl;
using UnityEngine;

public class PointsChangedCommand : Command
{
	[Inject]
	public int points { get; set; }

	public override void Execute()
	{
		Debug.Log("Points: " + points);
	}
}
