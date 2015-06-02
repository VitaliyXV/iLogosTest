using strange.extensions.command.impl;

public class StartCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }

	public override void Execute()
	{
		manager.Initialize();
	}
}
