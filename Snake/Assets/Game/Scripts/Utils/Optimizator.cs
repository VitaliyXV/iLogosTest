using UnityEngine;

public class Optimizator : MonoBehaviour
{
	public float DeltaTime { get; private set; }

	public static Optimizator Instance { get { return instance; } }

	private static Optimizator instance;	

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		DeltaTime = Time.deltaTime;
	}
}
