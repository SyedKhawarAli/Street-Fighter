using System.Collections;
using UnityEngine;

public class Hadoken : MonoBehaviour {
	public Fighter caster;
	public Fighter fighter;
	public static float movementForce = 200;
	public float damage;

	private Rigidbody body;
	private float creationTime;

	// Use this for initialization
	void Start () {
		Debug.Log ("Hadok " + this.gameObject.name);
		creationTime = Time.time;
		body = GetComponent<Rigidbody> ();
		// if (Fighter.player == Fighter.PlayerType.player1) {
		// 	body.AddRelativeForce (new Vector3 (movementForce, 0, 0));
		// } else {
		// 	body.AddRelativeForce (new Vector3 (-movementForce, 0, 0));
		// }
		body.AddRelativeForce (new Vector3 (movementForce, 0, 0));
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - creationTime > 3) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision col) {
		Fighter fighter = col.gameObject.GetComponent<Fighter> ();
		if (fighter != null && fighter != caster) {
			fighter.hurt (damage);
			Destroy (gameObject);
		}
	}
}