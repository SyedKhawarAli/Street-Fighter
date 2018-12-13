using System.Collections;
using UnityEngine;

public class HitColider : MonoBehaviour {
	public string punchName;
	public float damage;

	public Fighter owner;

	void OnTriggerEnter (Collider other) {
		Fighter somebody = other.gameObject.GetComponent<Fighter> ();
		if (owner.attacking) {
			if (somebody != null && somebody != owner) {
				somebody.hurt (damage);
				// somebody.body.AddRelativeForce (new Vector3 (0, 0, -800));
				print (punchName);

			}
		}
	}
}