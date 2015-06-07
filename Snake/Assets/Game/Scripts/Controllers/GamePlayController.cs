using System.Collections;
using UnityEngine;

public class GamePlayController : MonoBehaviour, IGamePlayController
{
	public GameObject PlayerObject;

	private PlayerController playerController;

	[Inject]
	public IFieldGenerator<GameObject> fieldGenerator { get; set; }

	[Inject]
	public LifesChangedSignal lifesChangedSignal { get; set; }

	[Inject]
	public LengthChangedSignal lengthChangedSignal { get; set; }

	[Inject]
	public PointsChangedSignal pointsChangedSignal { get; set; }

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
		playerController.lifesChangedSignal = lifesChangedSignal;
		playerController.lengthChangedSignal = lengthChangedSignal;
		playerController.pointsChangedSignal = pointsChangedSignal;

		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown()
	{
		for (int i = 3; i > 0; i--)
		{
			yield return new WaitForSeconds(1f);
		}
		
		playerController.StartRun();
	}
}
