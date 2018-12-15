using System.Collections;
using UnityEngine;

public class HitColider : MonoBehaviour {
	public string punchName;
	public float damage;

	public Fighter owner;
	private Fighter somebody;
	Vector3 startMarker, endMarker;
	bool isCollided;
	public float speed = 1f;
	private float startTime;
	private float journeyLength;

	private float fraction = 0;
	void OnTriggerEnter (Collider other) {
		somebody = other.gameObject.GetComponent<Fighter> ();
		print (this.GetComponent<Transform> ().name);
		print (other.GetComponent<Transform> ().name);
		if (owner.attacking) {
			if (somebody != null && somebody != owner) {
				somebody.hurt (damage);
				somebody.body.AddForce (somebody.transform.forward * -600);
			}
		}
	}
}