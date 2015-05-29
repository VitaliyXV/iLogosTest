using UnityEngine;
using strange.extensions.command.impl;

public class FieldTypeCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }
	
	public override void Execute()
	{
		manager.SwitchFieldType();
	}
}
