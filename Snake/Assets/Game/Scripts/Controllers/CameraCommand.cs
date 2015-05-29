using UnityEngine;
using strange.extensions.command.impl;

public class CameraCommand : Command
{
	public override void Execute()
	{
		Debug.Log("Camera type change");
	}
}
