using UnityEngine;
using System.Collections;

public class FlameCollider : MonoBehaviour
{
	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerStay(Collider other)
	{
		EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth> ();
		if (enemyHealth != null)
		{
			enemyHealth.ReceiveDamage (Constants.FireDamage);
		}
	}
}

