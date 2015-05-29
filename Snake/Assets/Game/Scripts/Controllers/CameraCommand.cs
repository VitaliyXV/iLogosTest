﻿using UnityEngine;
using strange.extensions.command.impl;

public class CameraCommand : Command
{
	[Inject]
	public IMainMenuManager manager { get; set; }	

	public override void Execute()
	{
		manager.SwitchCamera();
	}
}
