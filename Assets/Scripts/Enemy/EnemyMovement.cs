using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour 
{
	/// <summary>
	/// Reference to player transform
	/// </summary>
	private Transform playerTransform;

	/// <summary>
	/// Reference to beast mode transform
	/// </summary>
	public Transform beastModeTransform;

	/// <summary>
	/// The nav mesh agent component
	/// </summary>
	private NavMeshAgent nav;

	/// <summary>
	/// The animatior of this boss.
	/// </summary>
	private Animation anim;

    /// <summary>
    /// The current running time.
    /// </summary>
	private float time;

    /// <summary>
    /// The time that is 3 seconds behind the current time.
    /// </summary>
	private float previousTime;

    /// <summary>
    /// Indicate when to attack.
    /// </summary>
	private float timer;

    /// <summary>
    /// Check whether the boss can attack or not.
    /// </summary>
	private bool isAttacking;

	/// <summary>
	/// Is this enemy the boss?
	/// </summary>
	public bool isBoss;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		time = 0.0f;
		previousTime = 0.0f;
		timer = 0.0f;
		isAttacking = false;

		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		nav = gameObject.GetComponent<NavMeshAgent> ();
		anim = gameObject.GetComponent<Animation> ();
	}
	
	/// <summary>
	/// Call FixedUpdate
	/// </summary>
	void Update () 
	{
		time += Time.deltaTime;
		timer += Time.deltaTime;

		// Set destination
		if (playerTransform.gameObject.activeSelf)
			nav.SetDestination (playerTransform.position);
		else
			nav.SetDestination (beastModeTransform.position);

		if (isBoss)
		{
			int timeDifference = (int)(time - previousTime);

			// Switch animation if it has been 5 seconds
			if (timeDifference == 5)
			{
				previousTime = time;
				isAttacking = !isAttacking;

				if (isAttacking)
					nav.Stop ();
				else
					nav.Resume();
			}

			// Attack or move
			if (isAttacking && timer >= 0.5f)
			{
				timer = 0.0f;
				GetComponent<EnemyAttacks> ().BossAttack ();
			}
			else if (!isAttacking)
			{
				BossMoveAnimation ();
			}
		}
	}

	/// <summary>
	/// Boss moves.
	/// </summary>
	private void BossMoveAnimation()
	{
		float distance = Vector3.Distance (playerTransform.position, transform.position);
		if (distance > 15.0f)
		{
			nav.speed = 10.0f;
			anim.Stop ("walk");
			anim.Play ("run");
		}
		else
		{
			nav.speed = 5.0f;
			anim.Stop ("run");
			anim.Play ("walk");
		}
	}
}
