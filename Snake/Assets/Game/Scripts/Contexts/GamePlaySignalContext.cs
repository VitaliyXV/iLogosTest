using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using UnityEngine;

public class GamePlaySignalContext : MVCSContext
{
	public GamePlaySignalContext(MonoBehaviour contextView)
		: base(contextView)
	{
		
	}

	protected override void addCoreComponents()
	{
		base.addCoreComponents();

		// rebind events (default) to signals
		injectionBinder.Unbind<ICommandBinder>();
		injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
	}

	public override void Launch()
	{
		base.Launch();
		Signal startSignal = injectionBinder.GetInstance<GamePlayStartSignal>();
		startSignal.Dispatch();
	}
}
