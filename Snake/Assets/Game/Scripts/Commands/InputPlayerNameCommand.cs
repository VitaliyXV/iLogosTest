﻿using strange.extensions.command.impl;
using UnityEngine;

public class InputPlayerNameCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }

	[Inject]
	public string name { get; set; }

	public override void Execute()
	{
		Debug.Log("Commnad name: " + name);
		manager.InputPlayerName(name);
	}
}
