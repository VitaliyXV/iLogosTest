using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float WalkSpeed { get; set; }
	public float RotateSpeed { get; set; }
	public int Length { get; set; }	

	private Rigidbody body;
	private bool controlOn;
	private Direction direction;
	private Direction currentDirection;
	private GameObject currentTile;
	private GameObject nextTile;

	private int tileY;
	private int tileX;

	private Vector3 oldPos;
	private Quaternion rotation;
	private float horizontalAxis = 0;
	private float verticalAxis = 0;

	[Inject]
	public IFieldGenerator<GameObject> field { get; set; }

	private Animation animation;

	void Start()
	{
		Debug.Log("Player start");

		WalkSpeed = 0.5f;
		RotateSpeed = 5;

		body = transform.GetComponent<Rigidbody>();
		animation = GetComponentInChildren<Animation>();
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;	
	}

	public void StartRun()
	{
		Debug.Log("RUN with speed: " + WalkSpeed);

		tileY = field.PlayerStartPositionY;
		tileX = field.PlayerStartPositionX;
		currentTile = field.PlayerStartPosition;
		nextTile = field.NextTile(direction, ref tileY, ref tileX);

		SetRotation();

		oldPos = this.transform.position; 

		controlOn = true;
		
		animation.wrapMode = WrapMode.Loop;
		animation.CrossFade("Walk");
	}
	
	void Update()
	{		

#if UNITY_ANDROID || UNITY_IOS // get input from touchpad

#else							
		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");
#endif

		if (horizontalAxis > 0) { direction = Direction.RIGHT; }
		if (horizontalAxis < 0) { direction = Direction.LEFT; }
		if (verticalAxis > 0) { direction = Direction.UP; }
		if (verticalAxis < 0) { direction = Direction.DOWN; }

		if (!controlOn) return; // returns, if snake control disabled
		
		transform.position += ((nextTile.transform.position - oldPos) * Optimizator.Instance.DeltaTime * WalkSpeed);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Optimizator.Instance.DeltaTime * RotateSpeed);

		if (Vector3.Distance(transform.position, nextTile.transform.position) < 0.01f)
		{
			currentTile = nextTile;

			if (direction != currentDirection)
			{
				SetRotation();
				currentDirection = direction;
			}
			
			oldPos = transform.position = nextTile.transform.position;

			nextTile = field.NextTile(direction, ref tileY, ref tileX);
		}
	}

	private void SetRotation()
	{
		switch (direction)
		{
			case Direction.UP: rotation = Quaternion.Euler(0, 0, 0); break;
			case Direction.DOWN: rotation = Quaternion.Euler(0, 0, 180); break;
			case Direction.RIGHT: rotation = Quaternion.Euler(0, 0, -90); break;
			case Direction.LEFT: rotation = Quaternion.Euler(0, 0, 90); break;
		}
	}
}
