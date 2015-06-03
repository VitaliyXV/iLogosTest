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
	}
}
