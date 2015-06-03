using strange.extensions.command.impl;

public class GamePlayStartCommand : Command
{
	[Inject]
	public IGamePlayController manager { get; set; }

	public override void Execute()
	{
		manager.Initialize();
	}
}
