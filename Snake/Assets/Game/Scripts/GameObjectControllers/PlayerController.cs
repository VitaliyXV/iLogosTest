using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject PlayerObject;

	public float Speed;
	public int Length;

	[Inject]
	public IFieldGenerator<GameObject> field { get; set; }

	void Start ()
	{
//		var start = field[field.PlayerStartPositionY, field.PlayerStartPositionX];
//		transform.position = start.transform.position;
	}

	void Update ()
	{
	
	}
}
