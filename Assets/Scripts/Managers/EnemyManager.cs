using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour 
{
	/// <summary>
	/// The key prefab.
	/// </summary>
	private GameObject keyPrefab;

	/// <summary>
	/// The enemy prefab.
	/// </summary>
	private GameObject enemyPrefab;

	/// <summary>
	/// List of enemies on platform 1
	/// </summary>
	private List<GameObject> enemyList1;

	/// <summary>
	/// List of enemies on platform 2
	/// </summary>
	private List<GameObject> enemyList2;

	/// <summary>
	/// Number of dead enemies
	/// </summary>
	private int enemyDead;

	/// <summary>
	/// The enemy count text.
	/// </summary>
	public Text enemyCountText;

	/// <summary>
	/// Maximum number of enemies
	/// </summary>
	public int maxEnemies1 = 6;
	public int maxEnemies2 = 4;

	/// <summary>
	/// Index for enemylist1 and enemyList2
	/// </summary>
	private int index1;
	private int index2;

	/// <summary>
	/// Teleportation pads
	/// </summary>
	public GameObject teleportPad1;
	public GameObject teleportPad2;
	public GameObject teleportPad3;
	public GameObject teleportPad4;
	public GameObject teleportPad5;

	/// <summary>
	/// /// Spawning points
	/// </summary>
	public GameObject spawningPoint1;
	public GameObject spawningPoint2;

	private KeyHolder keyHolder;

	// Use this for initialization
	void Start () 
	{
		// Initialize members
		keyPrefab = Resources.Load("Key") as GameObject;
		enemyPrefab = Resources.Load ("Enemy") as GameObject;
		enemyList1 = new List<GameObject> ();
		enemyList2 = new List<GameObject> ();
		enemyDead = 0;
		index1 = 0;
		index2 = 0;
		keyHolder = GameObject.FindGameObjectWithTag ("Player").GetComponent<KeyHolder> ();

		keyHolder.AcquireShieldKey += new EventHandler (AcquireShieldKeyHandler);

		// Set the count text
		SetEnemyCountText ();

		// Instantiate enemies and set them to be inactive
		InstantiateEnemies();

		InvokeRepeating ("EnableEnemy1", 3.0f, 5.0f);
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update()
	{
		if (!teleportPad5.activeSelf && enemyDead == maxEnemies1 + maxEnemies2 && keyHolder.AcquiredAllKeys)
		{
			teleportPad5.SetActive (true);
		}
	}

	/// <summary>
	/// Instantiates the enemies.
	/// </summary>
	private void InstantiateEnemies()
	{
		// Generate random indexes
		int randomIndex1 = UnityEngine.Random.Range (0, maxEnemies1);
		//int randomIndex2 = UnityEngine.Random.Range (maxEnemies1, maxEnemies1 + maxEnemies2);

		for (int i = 0; i < maxEnemies1 + maxEnemies2; i++)
		{
			GameObject enemy = Instantiate (enemyPrefab);
			enemy.SetActive (false);
			enemy.GetComponent<EnemyHealth> ().EnemyDeath += new EventHandler (EnemyDeathHandler);

			// Assign the key to the selected enemy
			if (randomIndex1 == i)
			{
				GameObject key = Instantiate (keyPrefab);
				//key.name = (randomIndex1 == i) ? Constants.JumpKeyName : Constants.SwordKeyName;
				key.name = Constants.JumpKeyName;
				key.SetActive (false);
				key.transform.parent = enemy.transform;
			}

			if (i < maxEnemies1)
			{
				enemy.transform.position = spawningPoint1.transform.position;	
				enemyList1.Add (enemy);
			}
			else
			{
				enemy.transform.position = spawningPoint2.transform.position;
				enemyList2.Add (enemy);
			}
		}
	}
		
	/// <summary>
	/// Enables the enemies on platform 1
	/// </summary>
	private void EnableEnemy1()
	{
		if (index1 >= maxEnemies1)
		{
			CancelInvoke ();
			return;
		}
		enemyList1 [index1].SetActive (true);
		index1++;
	}

	/// <summary>
	/// Enables the enemies on platform 2
	/// </summary>
	private void EnableEnemy2()
	{
		if (index2 >= maxEnemies2)
		{
			CancelInvoke ();
			return;
		}
		enemyList2 [index2].SetActive (true);
		index2++;
	}

	/// <summary>
	/// Handle what happens when an enemy dies
	/// </summary>
	/// <param name="sender">Object that sends this event.</param>
	/// <param name="e">Event arguments.</param>
	private void EnemyDeathHandler (object sender, EventArgs e)
	{
		EnemyHealth enemyHealth = sender as EnemyHealth;

		// Make the senemy drop the key
		if (enemyHealth.gameObject.transform.childCount == 9)
		{
			GameObject key = enemyHealth.gameObject.transform.GetChild (8).gameObject;
			key.transform.parent = null;
			key.transform.position = enemyHealth.gameObject.transform.position;
			key.transform.rotation = Quaternion.identity;
			key.SetActive (true);
		}

		// Destroy the enemy object
		Destroy (enemyHealth.gameObject);
		enemyDead++;
		SetEnemyCountText ();

		// Enable the teleportation pads
		if (enemyDead == maxEnemies1)
		{
			teleportPad1.SetActive (true);
			teleportPad2.SetActive (true);
		}
	}

	/// <summary>
	/// Sets the enemy count text.
	/// </summary>
	private void SetEnemyCountText()
	{
		enemyCountText.text = "Enemy: " + enemyDead.ToString () + "/" + (maxEnemies1 + maxEnemies2).ToString();
	}

	/// <summary>
	/// Handler for when acquiring the shield key
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void AcquireShieldKeyHandler(object sender, EventArgs e)
	{
		teleportPad3.SetActive (true);
		teleportPad4.SetActive (true);
		InvokeRepeating ("EnableEnemy2", 5.0f, 5.0f);
	}
}
