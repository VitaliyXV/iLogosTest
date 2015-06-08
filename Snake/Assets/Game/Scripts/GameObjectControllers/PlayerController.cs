using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// reference to next segment of snake
	public GameObject NextSegment;
	// reference to script controller of next segment
	public NextSegmentController NextSegmentScript;

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

	// inputed direction
	private Direction direction;
	private Direction currentDirection;
	private GameObject currentTile;
	private GameObject nextTile;
	// script of next tile (shown type of tile and object on tile)
	private Cell nextCell;

	// remember old tile indexes
	private int oldTileY;
	private int oldTileX;
	// current tile indexes
	private int tileY;
	private int tileX;

	// old player position
	private Vector3 oldPos;
	// angle of direction
	private Quaternion rotation;

	// input controls from player
	private float horizontalAxis = 0;
	private float verticalAxis = 0;

	// flags
	private bool isAttack;		// attack on food
	private bool isIncrease;	// increase of snake
	private bool controlOn;		// enable controls from player
	
	// reference to field generator
	public IFieldGenerator<GameObject> field { get; set; }

	// signals (strange IoC framework)
	public LifesChangedSignal lifesChangedSignal { get; set; }
	public LengthChangedSignal lengthChangedSignal { get; set; }
	public PointsChangedSignal pointsChangedSignal { get; set; }
	public GameOverSignal gameOverSignal { get; set; }

	private AudioSource audioSource;
	private Animation snakeAnimation;

	void Start()
	{
		Debug.Log("Player start");

		// default values
		WalkSpeed = 0.7f;
		RotateSpeed = 5;
		AttackSpeed = 0.6f;
		SpeedUpCoef = 0.1f;

		// get componnents
		body = transform.GetComponent<Rigidbody>();
		snakeAnimation = GetComponentInChildren<Animation>();
		audioSource = GetComponent<AudioSource>();
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;	
	}

	/// <summary>
	/// Init player position, before game start
	/// </summary>
	public void StartRun()
	{
		oldTileY = tileY = field.PlayerStartPositionY;
		oldTileX = tileX = field.PlayerStartPositionX;
		currentTile = field.PlayerStartPosition;

		GetNextTile();

		oldPos = this.transform.position;

		ReadyToRun();
	}

	/// <summary>
	/// Set enabled input controls from player
	/// </summary>
	private void ReadyToRun()
	{
		controlOn = true;
		Walk();
	}
	
	void Update()
	{		

#if UNITY_ANDROID || UNITY_IOS
		// TODO: get input from touchpad
#else							
		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");

#endif
		if (horizontalAxis != 0 || verticalAxis != 0)
		{
			CheckDirection();
		}
		
		// rotate snake
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Optimizator.Instance.DeltaTime * RotateSpeed);

		if (!controlOn) return; // returns, if snake control disabled
		
		// move snake
		transform.position += ((nextTile.transform.position - oldPos) * Optimizator.Instance.DeltaTime * WalkSpeed);

		// move snake tail
		if (NextSegmentScript != null)
		{
			NextSegmentScript.Move();
		}

		// attack the food, if next tile has it, and distance is well
		if (!isAttack && nextCell.ObjectOnTileType == TileObject.Food && Vector3.Distance(transform.position, nextCell.ObjectOnTile.transform.position) < 1.5f)
		{
			Attack();
		}

		// switch on next tile, if distance is well
		if (Vector3.Distance(transform.position, nextTile.transform.position) < 0.01f)
		{
			currentTile = nextTile;
			
			if (direction != currentDirection) // switch direction
			{
				currentDirection = direction;
			}
			
			// remember old position
			oldPos = transform.position = nextTile.transform.position;

			if (isIncrease) // increase snake
			{
				isIncrease = false;
				IncreaseSnakeLength();
			}

			// set new direction for tail
			if (NextSegmentScript != null)
			{
				NextSegmentScript.SetDirection(currentTile, rotation);
			}

			GetNextTile();
		}
	}

	private void CheckDirection()
	{
		switch (GameData.CurrentCameraType)
		{
			// if camera type is Ordinary, just get inputed direction
			case CameraType.Ordinary:
				if (horizontalAxis > 0) { direction = Direction.Right; }
				if (horizontalAxis < 0) { direction = Direction.Left; }
				if (verticalAxis > 0) { direction = Direction.Up; }
				if (verticalAxis < 0) { direction = Direction.Down; }
				break;

			// if camera type is ThirdPerson, get inputed direction and switch to left/right direction
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

				if (verticalAxis < 0)
				{
					switch (currentDirection)
					{
						case Direction.Up: direction = Direction.Down; break;
						case Direction.Right: direction = Direction.Left; break;
						case Direction.Down: direction = Direction.Up; break;
						case Direction.Left: direction = Direction.Right; break;
					}
				}

				break;
		}
	}
	
	private void Attack()
	{
		isAttack = true;

		snakeAnimation.wrapMode = WrapMode.Default;
		snakeAnimation.CrossFade("Attack");

		audioSource.PlayOneShot(AttackSound);

		// eat the food
		Invoke("Eat", AttackSpeed);		
	}

	private void Eat()
	{
		audioSource.PlayOneShot(EatSound);

		// TODO: spawn particle blood

		// create new food (i don't create new object: move old food in new position)
		field.SetFood(nextCell.ObjectOnTile);
		
		// clear tile
		nextCell.ObjectOnTile = null;		
		nextCell.ObjectOnTileType = TileObject.Empty;

		// increase speed of snake
		WalkSpeed += SpeedUpCoef;
		RotateSpeed += SpeedUpCoef;
		AttackSpeed -= SpeedUpCoef / 2;

		GameData.IncreaseSnake();

		// raise signals to update gameplay view
		lengthChangedSignal.Dispatch(GameData.SnakeLength);
		pointsChangedSignal.Dispatch(GameData.CurrentPoints);

		// switch on the increased flag (to create new segment of tail on next tile)
		isIncrease = true;

		// set walk animation
		Walk();
				
		isAttack = false;
	}

	private void GetNextTile()
	{
		// remember current tile position
		oldTileY = tileY;
		oldTileX = tileX;

		// get next tile from field generator
		nextTile = field.NextTile(direction, ref tileY, ref tileX, ref rotation);
		nextCell = nextTile.GetComponent<Cell>();
	}

	private void Walk()
	{
		snakeAnimation.wrapMode = WrapMode.Loop;
		snakeAnimation.CrossFade("Walk");
	}

	void OnTriggerEnter(Collider target)
	{
		if(target.tag == "Wall" || target.tag == "Player")
		{
			Death();
		}
	}

	private void Death()
	{
		audioSource.PlayOneShot(DeathSound);

		// reset snake position to last enabled tile
		transform.position = currentTile.transform.position;
		nextTile = currentTile;
		tileY = oldTileY;
		tileX = oldTileX;
		controlOn = false;
		
		snakeAnimation.wrapMode = WrapMode.Default;
		snakeAnimation.CrossFade("Dead");

		GameData.DecreaseLifes();

		// notify the gameplay view about death
		lifesChangedSignal.Dispatch(GameData.LifeCount);

		if (GameData.LifeCount > 0) // next life...
		{
			Invoke("Prepare", 2);
		}
		else // gameover...
		{
			gameOverSignal.Dispatch();
		}
	}

	/// <summary>
	/// prepare to snake run
	/// </summary>
	private void Prepare()
	{
		snakeAnimation.wrapMode = WrapMode.Loop;
		snakeAnimation.CrossFade("Idle");
		Invoke("ReadyToRun", 3);
	}

	private void IncreaseSnakeLength()
	{
		// TODO: optimize - get next segment from object pool
		var next = Instantiate(NextSegment, transform.position, transform.rotation) as GameObject;

		var nextScript = next.GetComponent<NextSegmentController>();
		
		// set reference to head
		nextScript.Head = this;
		// share animation
		nextScript.SnakeAnimation = snakeAnimation;
		//set next tile for segment
		nextScript.NextTile = nextTile;

		nextScript.NextSegment = NextSegmentScript;

		// switch on box collider on tail, if this segment is not next of head
		if (NextSegmentScript != null) NextSegmentScript.SetActiveCollider();

		// set new segment as next, after head
		NextSegmentScript = nextScript;
	}
}
