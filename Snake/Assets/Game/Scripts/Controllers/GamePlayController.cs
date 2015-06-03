using UnityEngine;

public class GamePlayController : MonoBehaviour, IGamePlayController
{
	[Inject]
	public IFieldGenerator<GameObject> fieldGenerator { get; set; }

	public void Initialize()
	{
		Debug.Log("Init Game play");
	}

	void Start()
	{
		fieldGenerator.Generate();
	}
}
