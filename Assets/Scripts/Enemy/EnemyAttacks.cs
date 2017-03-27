using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour 
{
	private GameObject player;
	public GameObject beastMode;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
	private bool touchPlayer;
	private float timeBetweenAttacks;
	private float timer;
	private Animation anim;

	// Use this for initialization
	void Start () 
	{
		timer = 0.0f;
		timeBetweenAttacks = 0.5f;

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		anim = GetComponent<Animation> ();

		touchPlayer = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if (touchPlayer && enemyHealth.CurrentHealth > 0 && timer >= timeBetweenAttacks)
		{
			timer = 0.0f;
			playerHealth.ReceiveDamage (Constants.TouchDamaged);
		}
	}

	/// <summary>
	/// Boss Attack.
	/// </summary>
	public void BossAttack()
	{
		if (player.activeSelf)
			transform.rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		else
			transform.rotation = Quaternion.LookRotation (beastMode.transform.position - transform.position);
		
		GetComponentInChildren<ShootIce> ().FireIce (player.transform.position);
		anim.Play ("punch");
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag ("Shield"))
		{
			touchPlayer = false;
		}
		else if (other.gameObject.CompareTag ("Player"))
		{
			touchPlayer = true;
		}
	}

	/// <summary>
	/// Raises the collision exit event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerExit(Collider other)
	{		
		if (other.gameObject.CompareTag ("Player"))
		{
			touchPlayer = false;
		}
	}
}
