using UnityEngine;
using strange.extensions.command.impl;

public class FieldTypeCommand : Command
{
	[Inject]
	public IMainMenuManager manager { get; set; }
	
	public override void Execute()
	{
		manager.SwitchFieldType();
	}
}
