using strange.extensions.context.impl;

public class GamePlayBootstrap : ContextView
{
	void Awake()
	{
		context = new GamePlayContext(this);
	}
}
