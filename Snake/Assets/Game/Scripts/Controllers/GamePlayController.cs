using UnityEngine;

public class GamePlayController : MonoBehaviour, IGamePlayController
{
	public GameObject PlayerObject;

	[Inject]
	public IFieldGenerator<GameObject> fieldGenerator { get; set; }

	public void Initialize()
	{
		Debug.Log("Init Game play");
	}

	void Start()
	{
		fieldGenerator.Generate();

		var start = fieldGenerator.PlayerStartPosition;
		PlayerObject.transform.position = start.transform.position;
	}
}
