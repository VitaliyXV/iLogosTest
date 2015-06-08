using UnityEngine;

public class NextSegmentController : MonoBehaviour
{
	/// <summary>
	/// Referense to head
	/// </summary>
	public PlayerController Head;

	/// <summary>
	/// Reference to next segment object
	/// </summary>
	public GameObject NextTile;

	/// <summary>
	/// Reference to script of next segment object
	/// </summary>
	public NextSegmentController NextSegment;

	public Animation SnakeAnimation;

	// old posion of segment
	private Vector3 oldPos;

	// rotation for current segment
	private Quaternion rotation;

	private BoxCollider boxCollider;
	

	void Start()
	{
		oldPos = transform.position;

		boxCollider = GetComponent<BoxCollider>();

		// switch off collider (while this segment not moved from head)
		boxCollider.enabled = false;

		SnakeAnimation = GetComponentInChildren<Animation>();
		SnakeAnimation.wrapMode = WrapMode.Loop;
		SnakeAnimation.CrossFade("Walk");
	}

	/// <summary>
	/// Set direction for current segment
	/// </summary>
	/// <param name="nextTile">Next tile object</param>
	/// <param name="rotation">Rotation for segment</param>
	public void SetDirection(GameObject nextTile, Quaternion rotation)
	{	
		// set next tile for all others segments of tail
		if (NextSegment != null) NextSegment.SetDirection(NextTile, rotation);

		NextTile = nextTile;
		this.rotation = rotation;
	}

	/// <summary>
	/// Move segment
	/// </summary>
	public void Move()
	{
		transform.position += ((NextTile.transform.position - oldPos) * Optimizator.Instance.DeltaTime * Head.WalkSpeed);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Optimizator.Instance.DeltaTime * Head.RotateSpeed);

		oldPos = transform.position;

		if (NextSegment != null) NextSegment.Move();
	}

	/// <summary>
	/// Switch on collider, to triggered with head
	/// </summary>
	public void SetActiveCollider()
	{
		boxCollider.enabled = true;
	}
}
