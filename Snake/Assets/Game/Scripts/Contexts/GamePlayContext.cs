using UnityEngine;

public class GamePlayContext : GamePlaySignalContext
{
	public GamePlayContext(MonoBehaviour contextView)
		: base(contextView)
	{

	}

	protected override void mapBindings()
	{
		base.mapBindings();

		var manager = GameObject.Find("Manager").GetComponent<GamePlayController>();
		injectionBinder.Bind<IGamePlayController>().ToValue(manager);

		mediationBinder.Bind<GamePlayView>().To<GamePlayMediator>();

		switch (GameData.CurrentFieldType)
		{
			case FieldType.Square:
				injectionBinder.Bind<IFieldGenerator<GameObject>>().To<SquareFieldGenerator>().ToSingleton(); break;
			case FieldType.Hexahonal:
				injectionBinder.Bind<IFieldGenerator<GameObject>>().To<HexagonFieldGenerator>().ToSingleton(); break;
		}

		BindSignalsToCommands();
	}

	private void BindSignalsToCommands()
	{
		commandBinder.Bind<GamePlayStartSignal>().To<GamePlayStartCommand>().Once();
		commandBinder.Bind<LifesChangedSignal>().To<LifesChangedCommand>().Pooled();
		commandBinder.Bind<LengthChangedSignal>().To<LengthChangedCommand>().Pooled();
		commandBinder.Bind<PointsChangedSignal>().To<PointsChangedCommand>().Pooled();
		commandBinder.Bind<GameOverSignal>().To<GameOverCommand>().Pooled();
	}
}
