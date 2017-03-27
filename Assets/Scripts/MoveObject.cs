using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
	public enum Direction
	{
		Up, Down, Left, Right
	};

	public Direction direction;
	public float oscillateDistance;
	public float speed;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 v = startPosition;

		switch (direction)
		{
			case (Direction.Down):
				v.y -= oscillateDistance * Mathf.Sin (Time.time * speed);
				break;
			case (Direction.Up):
				v.y += oscillateDistance * Mathf.Sin (Time.time * speed);
				break;
			case (Direction.Left):
				v.x -= oscillateDistance * Mathf.Sin (Time.time * speed);
				break;
			case (Direction.Right):
				v.x += oscillateDistance * Mathf.Sin (Time.time * speed);
				break;
		}
			
		transform.position = v;
	}
}

