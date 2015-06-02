using strange.extensions.command.impl;

public class FacebookCommand : Command
{
	[Inject]
	public IMainMenuController manager { get; set; }

	public override void Execute()
	{
		manager.JoinWithFacebook();
	}
}
