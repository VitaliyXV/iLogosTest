using strange.extensions.command.impl;

public class MainMenuStartCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }

	public override void Execute()
	{
		manager.Initialize();
	}
}
