﻿using UnityEngine;

/// <summary>
/// Used for snakes on MainMenu screen
/// </summary>
public class SnakeSpriteController : MonoBehaviour
{
	public float MinXPosition;
	public float MinYPosition;
	public float MaxXPosition;
	public float MaxYPosition;
	public float MaxSpeed;

	private Vector3 startPosition;
	private Vector3 endPosition;
	public float moveSpeed;
	
	void Start () 
	{
		SetDirection();
	}
	
	void SetDirection()
	{
		// set random speed
		moveSpeed = Random.Range(1.0f, MaxSpeed);
		
		// set random position
		startPosition = transform.position;
		startPosition.y = Random.Range(MinYPosition, (MaxYPosition - MinYPosition) + MinYPosition);
		startPosition.z = 0;

		transform.position = startPosition;

		endPosition.x = startPosition.x < 0 ? MaxXPosition : MinXPosition;
		endPosition.y = startPosition.y;
		endPosition.z = 0;
	}

	void Update()
	{
		// move snake
		Vector3 currentPosition = transform.position;

		Vector3 moveDirection = endPosition - currentPosition;
		moveDirection.z = 0;
		moveDirection.Normalize();

		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp(currentPosition, target, Optimizator.Instance.DeltaTime);
		
		// reset snake on new position
		if (Mathf.Round(currentPosition.x - endPosition.x) == 0)
		{
			transform.Rotate(0, 180, 0);
			SetDirection();
		}
	}
}
