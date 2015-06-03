using System.Collections;
using UnityEngine;

public class GamePlayController : MonoBehaviour, IGamePlayController
{
	public GameObject PlayerObject;

	private PlayerController playerController;

	[Inject]
	public IFieldGenerator<GameObject> fieldGenerator { get; set; }

	public void Initialize()
	{
		Debug.Log("Init Game play");
	}

	void Start()
	{
		fieldGenerator.Generate();
		PlayerObject = Instantiate(PlayerObject, fieldGenerator.PlayerStartPosition.transform.position, GameData.CurrentFieldType == FieldType.Hexahonal ? Quaternion.Euler(0, 0, 33) :  Quaternion.identity) as GameObject;
		playerController = PlayerObject.GetComponent<PlayerController>();
		playerController.field = fieldGenerator;

		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown()
	{
		for (int i = 3; i > 0; i--)
		{
			Debug.Log("Ready: " + i);
			yield return new WaitForSeconds(1f);
		}
		
		Debug.Log("GO!");

		playerController.StartRun();
	}
}
