using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject PlayerObject;

	public float Speed;
	public int Length;

	// TODO: [Inject]
	public IFieldGenerator<GameObject> field;

	void Start ()
	{
		//var start = field[field.PlayerStartPositionY, field.PlayerStartPositionX];

		//transform.position = start.transform.position;
	}

	void Update ()
	{
	
	}
}
