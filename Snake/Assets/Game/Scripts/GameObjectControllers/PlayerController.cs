using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public AudioClip EatSound;
	public AudioClip DeathSound;
	public AudioClip IncreaseSound;
	public AudioClip AttackSound;

	public float WalkSpeed { get; set; }
	public float RotateSpeed { get; set; }
	public float AttackSpeed { get; set; }
	public int Length { get; set; }
	public float SpeedUpCoef { get; set; }

	private Rigidbody body;
	private bool controlOn;
	private Direction direction;
	private Direction currentDirection;
	private GameObject currentTile;
	private GameObject nextTile;
	private Cell nextCell;


	private int oldTileY;
	private int oldTileX;
	private int tileY;
	private int tileX;

	private Vector3 oldPos;
	private Quaternion rotation;
	private float horizontalAxis = 0;
	private float verticalAxis = 0;
	
	//[Inject]
	public IFieldGenerator<GameObject> field { get; set; }

	private AudioSource audioSource;
	private Animation animation;

	void Start()
	{
		Debug.Log("Player start");

		WalkSpeed = 0.7f;
		RotateSpeed = 5;
		AttackSpeed = 0.6f;
		SpeedUpCoef = 0.1f;

		body = transform.GetComponent<Rigidbody>();
		animation = GetComponentInChildren<Animation>();
		audioSource = GetComponent<AudioSource>();
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;	
	}

	public void StartRun()
	{
		oldTileY = tileY = field.PlayerStartPositionY;
		oldTileX = tileX = field.PlayerStartPositionX;
		currentTile = field.PlayerStartPosition;

		GetNextTile();

		oldPos = this.transform.position; 

		controlOn = true;

		Walk();
	}

	private void ReadyToRun()
	{
		controlOn = true;
		Walk();
	}
	
	void Update()
	{		

#if UNITY_ANDROID || UNITY_IOS // get input from touchpad

#else							
		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");

#endif
		CheckDirection();		
		
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Optimizator.Instance.DeltaTime * RotateSpeed);

		if (!controlOn) return; // returns, if snake control disabled
		
		transform.position += ((nextTile.transform.position - oldPos) * Optimizator.Instance.DeltaTime * WalkSpeed);		

		if (!isAttack && nextCell.ObjectOnTileType == TileObject.Food && Vector3.Distance(transform.position, nextCell.ObjectOnTile.transform.position) < 1.5f)
		{
			Attack();
		}

		if (Vector3.Distance(transform.position, nextTile.transform.position) < 0.01f)
		{
			currentTile = nextTile;

			if (direction != currentDirection)
			{
				currentDirection = direction;
			}
			
			oldPos = transform.position = nextTile.transform.position;

			GetNextTile();
		}
	}

	private void CheckDirection()
	{
		switch (GameData.CurrentCameraType)
		{
			case CameraType.Ordinary:
				if (horizontalAxis > 0) { direction = Direction.Right; }
				if (horizontalAxis < 0) { direction = Direction.Left; }
				if (verticalAxis > 0) { direction = Direction.Up; }
				if (verticalAxis < 0) { direction = Direction.Down; }
				break;

			case CameraType.ThirdPerson:

				if (horizontalAxis > 0)
				{
					switch (currentDirection)
					{
						case Direction.Up: direction = Direction.Right; break;
						case Direction.Right: direction = Direction.Down; break;
						case Direction.Down: direction = Direction.Left; break;
						case Direction.Left: direction = Direction.Up; break;
					}
				}

				if (horizontalAxis < 0)
				{
					switch (currentDirection)
					{
						case Direction.Up: direction = Direction.Left; break;
						case Direction.Right: direction = Direction.Up; break;
						case Direction.Down: direction = Direction.Right; break;
						case Direction.Left: direction = Direction.Down; break;
					}
				}

				if (verticalAxis > 0)
				{
					direction = Direction.Up;
				}

				if (verticalAxis < 0)
				{
					direction = Direction.Down;
				}

				break;
		}
	}

	bool isAttack;

	private void Attack()
	{
		isAttack = true;

		animation.wrapMode = WrapMode.Default;
		animation.CrossFade("Attack");

		audioSource.PlayOneShot(AttackSound);

		Invoke("Eat", AttackSpeed);		
	}

	private void Eat()
	{
		audioSource.PlayOneShot(EatSound);

		// TODO: spawn particle blood

		// create new food
		field.SetFood(nextCell.ObjectOnTile);
		nextCell.ObjectOnTile = null;		
		nextCell.ObjectOnTileType = TileObject.Empty;

		// increase speed of snake
		WalkSpeed += SpeedUpCoef;
		RotateSpeed += SpeedUpCoef;
		AttackSpeed -= SpeedUpCoef / 2;

		Walk();

		isAttack = false;
	}

	private void GetNextTile()
	{
		oldTileY = tileY;
		oldTileX = tileX;
		nextTile = field.NextTile(direction, ref tileY, ref tileX, ref rotation);
		nextCell = nextTile.GetComponent<Cell>();
	}

	private void Walk()
	{
		animation.wrapMode = WrapMode.Loop;
		animation.CrossFade("Walk");
	}

	void OnTriggerEnter(Collider target)
	{
		if(target.tag == "Wall")
		{
			Death();
		}
	}

	private void Death()
	{
		audioSource.PlayOneShot(DeathSound);
		Debug.Log("Death");
		transform.position = currentTile.transform.position;
		nextTile = currentTile;
		tileY = oldTileY;
		tileX = oldTileX;
		controlOn = false;
		animation.wrapMode = WrapMode.Default;
		animation.CrossFade("Dead");
		
		Invoke("Prepare", 2);
	}

	private void Prepare()
	{
		animation.wrapMode = WrapMode.Loop;
		animation.CrossFade("Idle");
		Invoke("ReadyToRun", 3);
	}
}
