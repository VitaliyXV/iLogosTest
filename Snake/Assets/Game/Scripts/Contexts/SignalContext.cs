using UnityEngine;
using strange.extensions.signal.impl;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

/// <summary>
/// Class for enabling StrangeIoc signals
/// </summary>
public class SignalContext : MVCSContext
{
	public SignalContext(MonoBehaviour contextView)
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
		Signal startSignal = injectionBinder.GetInstance<StartSignal>();
		startSignal.Dispatch();
	}
}
