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
		
	}

	void Update ()
	{
	
	}
}
