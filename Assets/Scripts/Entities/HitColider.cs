using System.Collections;
using UnityEngine;

public class HitColider : MonoBehaviour {
	public float damage;
	public ParticleSystem particleEffect;
	public Fighter owner;
	private Fighter somebody;
	void OnTriggerEnter (Collider other) {
		somebody = other.gameObject.GetComponent<Fighter> ();
		if (owner.attacking) {
			if (somebody != null && somebody != owner) {
				somebody.hurt (damage);
				if (FighterFlyingBehaviour.flyinKick) {
					print ("flyinKick is true");
					FighterFlyingBehaviour.flyinKick = false;
					somebody.body.AddForce (somebody.transform.up * 200);
					somebody.flyingAnimation ();	
				}
				somebody.body.AddForce (somebody.transform.forward * -300);
				Quaternion rot = this.GetComponent<Transform> ().rotation;
				Vector3 pos = this.transform.position;
				var effect = Instantiate (particleEffect, pos, Quaternion.identity);				
			}
		}
	}
}