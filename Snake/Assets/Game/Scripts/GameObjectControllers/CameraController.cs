using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public GameObject Player;
	public Vector3 TopCameraPosition;

	[SerializeField]
	private float rotationSpeed = 2;
	private Quaternion currentCameraRotation;
	private Vector3 currentCameraPosition;

	private GameObject playerCameraPoint;

	void Start () 
	{
		// get player to follow him
		Player = GameObject.FindGameObjectWithTag("Player");
		playerCameraPoint = GameObject.FindGameObjectWithTag("Camera");

		currentCameraRotation = Quaternion.identity;
		currentCameraPosition = Player.transform.position + TopCameraPosition;
	}
		
	void Update ()
	{
		// switch camera type, by 'V' key pressed
		if(Input.GetKeyUp(KeyCode.V))
		{
			SwitchCamera();
		}

		// move camera
		switch (GameData.CurrentCameraType)
		{
			case CameraType.Ordinary: MoveTopCamera(); break;
			case CameraType.ThirdPerson: MoveThirdPersonCamera(); break;
		}		
	}

	private void MoveTopCamera()
	{
		// TODO: Optimize. Don't slerp/lerp, when camera complete switch
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Optimizator.Instance.DeltaTime * rotationSpeed);
		transform.position = Vector3.Lerp(transform.position, Player.transform.position + TopCameraPosition, Optimizator.Instance.DeltaTime * rotationSpeed);
	}

	private void MoveThirdPersonCamera()
	{
		// TODO: Optimize. Don't slerp/lerp, when camera complete switch
		transform.rotation = Quaternion.Slerp(transform.rotation, playerCameraPoint.transform.rotation, Optimizator.Instance.DeltaTime * rotationSpeed);
		transform.position = Vector3.Lerp(transform.position, playerCameraPoint.transform.position, Optimizator.Instance.DeltaTime * rotationSpeed);
	}

	private void SwitchCamera()
	{
		switch (GameData.CurrentCameraType)
		{
			case CameraType.Ordinary: GameData.CurrentCameraType = CameraType.ThirdPerson; break;
			case CameraType.ThirdPerson: GameData.CurrentCameraType = CameraType.Ordinary; break;
		}	
	}
}
