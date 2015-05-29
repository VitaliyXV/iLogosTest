using strange.extensions.context.impl;

/// <summary>
/// Class for initializing context of main scene
/// </summary>
public class MainMenuBootstrap : ContextView
{
	void Awake()
	{
		context = new MainMenuContext(this);
	}
}
