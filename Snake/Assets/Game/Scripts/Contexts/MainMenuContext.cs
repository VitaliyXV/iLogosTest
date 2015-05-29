using UnityEngine;

/// <summary>
/// Context for main menu scene
/// </summary>
public class MainMenuContext : SignalContext
{
	public MainMenuContext(MonoBehaviour contextView)
		: base(contextView)
	{

	}

	protected override void mapBindings()
	{
		base.mapBindings();
		
		BindSignalsToCommands();

		mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();

		var manager = GameObject.Find("Manager").GetComponent<MainMenuManager>();
		injectionBinder.Bind<IMainMenuManager>().ToValue(manager);
	}

	private void BindSignalsToCommands()
	{
		commandBinder.Bind<NewGameSignal>().To<NewGameCommand>().Pooled();
		commandBinder.Bind<CameraSignal>().To<CameraCommand>().Pooled();
		commandBinder.Bind<FieldTypeSignal>().To<FieldTypeCommand>().Pooled();
		commandBinder.Bind<ExitSignal>().To<ExitCommand>().Pooled();
	}
}
