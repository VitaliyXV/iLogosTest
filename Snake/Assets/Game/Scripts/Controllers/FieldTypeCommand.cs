using UnityEngine;
using strange.extensions.command.impl;

public class FieldTypeCommand : Command
{
	public override void Execute()
	{
		Debug.Log("Field type change");
	}
}
