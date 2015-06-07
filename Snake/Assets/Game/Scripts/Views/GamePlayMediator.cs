using UnityEngine;
using strange.extensions.mediation.impl;

public class GamePlayMediator : Mediator
{
	[Inject]
	public GamePlayView gamePlayView { get; set; }

	[Inject]
	public LifesChangedSignal lifesChangedSignal { get; set; }

	[Inject]
	public LengthChangedSignal lengthChangedSignal { get; set; }

	[Inject]
	public PointsChangedSignal pointsChangedSignal { get; set; }

	public override void OnRegister()
	{
		Debug.Log("Registered");
		lifesChangedSignal.AddListener(gamePlayView.UpdateLifes);
		lengthChangedSignal.AddListener(gamePlayView.UpdateLength);
		pointsChangedSignal.AddListener(gamePlayView.UpdatePoints);
	}

	public override void OnRemove()
	{
		lifesChangedSignal.RemoveListener(gamePlayView.UpdateLifes);
		lengthChangedSignal.RemoveListener(gamePlayView.UpdateLength);
		pointsChangedSignal.RemoveListener(gamePlayView.UpdatePoints);
	}
}
