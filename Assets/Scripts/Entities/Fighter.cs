﻿using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour {
	public enum PlayerType {
		HUMAN,
		AI,
		player1,
		player2
		};

		public static float MAX_HEALTH = 100f;

		public float healt = MAX_HEALTH;
		public string fighterName;
		public Fighter oponent;
		public bool enable;

		public PlayerType player;
		public FighterStates currentState = FighterStates.IDLE;

		protected Animator animator;
		private Rigidbody myBody;
		private AudioSource audioPlayer;
		private CapsuleCollider capculeCollider;
		private Quaternion fighterRotation;
		private bool brakeDefend;
		//for AI only
		private float random;
		private float randomSetTime;

		// Use this for initialization
		void Start () {
		myBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		audioPlayer = GetComponent<AudioSource> ();
		capculeCollider = GetComponent<CapsuleCollider> ();
	}

	public void UpdateHumanInput () {
		if (Input.GetAxis ("Horizontal") > 0.1) {
			animator.SetBool ("WALK", true);
		} else {
			animator.SetBool ("WALK", false);
		}

		if (Input.GetAxis ("Horizontal") < -0.1) {

			if (oponent.attacking) {
				animator.SetBool ("WALK_BACK", false);
				animator.SetBool ("DEFEND", true);
			} else {
				animator.SetBool ("WALK_BACK", true);
				animator.SetBool ("DEFEND", false);
			}

		} else {
			animator.SetBool ("WALK_BACK", false);
			animator.SetBool ("DEFEND", false);
		}

		if (Input.GetAxis ("Vertical") < -0.1) {
			animator.SetBool ("DUCK", true);
		} else {
			animator.SetBool ("DUCK", false);
		}

		if ((Input.GetAxis ("Vertical") > 0.1)) {
			animator.SetTrigger ("JUMP");
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("Fire1")) {
			animator.SetTrigger ("PUNCH");
		}

		if (Input.GetKeyDown (KeyCode.K) || Input.GetButtonDown ("Fire2")) {
			animator.SetTrigger ("KICK");
		}

		if (Input.GetKeyDown (KeyCode.H) || Input.GetButtonDown ("Fire3")) {
			animator.SetTrigger ("HADOKEN");
		}
	}
	public void UpdatePlayer1Input () {
		if (Input.GetAxis ("Horizontal") > 0.1) {
			animator.SetBool ("WALK", true);
		} else {
			animator.SetBool ("WALK", false);
		}

		if (Input.GetAxis ("Horizontal") < -0.1) {
			if (oponent.attacking) {
				animator.SetBool ("WALK_BACK", false);
				animator.SetBool ("DEFEND", true);
			} else {
				animator.SetBool ("WALK_BACK", true);
				animator.SetBool ("DEFEND", false);
			}

		} else {
			animator.SetBool ("WALK_BACK", false);
			animator.SetBool ("DEFEND", false);
		}

		if (Input.GetAxis ("Vertical") < -0.1) {
			OnDuckPressed ();
		} else {
			OnDuckLift ();
		}

		if ((Input.GetAxis ("Vertical") > 0.1)) {
			animator.SetTrigger ("JUMP");
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("Fire1")) {
			runPunch ();
			// animator.SetTrigger ("PUNCH");
		}

		if (Input.GetKeyDown (KeyCode.K) || Input.GetButtonDown ("Fire2")) {
			animator.SetTrigger ("KICK");
		}

		if (Input.GetKeyDown (KeyCode.H) || Input.GetButtonDown ("Fire3")) {
			runHadoken ();
		}
	}

	private void OnDuckPressed () {
		capculeCollider.height = 0.8f;
		capculeCollider.center = new Vector3 (0, 0.42f, 0);
		animator.SetBool ("DUCK", true);
	}
	private void OnDuckLift () {
		capculeCollider.height = 1.75f;
		capculeCollider.center = new Vector3 (0, 0.89f, 0);
		animator.SetBool ("DUCK", false);
	}
	public void UpdatePlayer2Input () {
		if (Input.GetAxis ("Horizontal1") < -0.1) {
			animator.SetBool ("WALK", true);
		} else {
			animator.SetBool ("WALK", false);
		}
		if (Input.GetAxis ("Horizontal1") > 0.1) {
			if (oponent.attacking) {
				animator.SetBool ("WALK_BACK", false);
				animator.SetBool ("DEFEND", true);
			} else {
				animator.SetBool ("WALK_BACK", true);
				animator.SetBool ("DEFEND", false);
			}
		} else {
			animator.SetBool ("WALK_BACK", false);
			animator.SetBool ("DEFEND", false);
		}

		if (Input.GetAxis ("Vertical1") < -0.1) {
			OnDuckPressed ();
		} else {
			OnDuckLift ();
		}

		if ((Input.GetAxis ("Vertical1") > 0.1)) {
			animator.SetTrigger ("JUMP");
		}

		if (Input.GetKeyDown (KeyCode.Delete) || Input.GetButtonDown ("Fire4")) {
			runPunch ();
			// animator.SetTrigger ("PUNCH");
		}

		if (Input.GetKeyDown (KeyCode.End) || Input.GetButtonDown ("Fire5")) {
			animator.SetTrigger ("KICK");
		}

		if (Input.GetKeyDown (KeyCode.PageDown) || Input.GetButtonDown ("Fire6")) {
			runHadoken ();
		}
	}

	private void runPunch () {
		animator.SetTrigger ("PUNCH");
	}
	private void runHadoken () {

		// print (Hadoken.movementForce);
		animator.SetTrigger ("HADOKEN");
	}
	public void UpdateAiInput () {
		animator.SetBool ("defending", defending);
		//animator.SetBool ("invulnerable", invulnerable);
		//animator.SetBool ("enable", enable);

		animator.SetBool ("oponent_attacking", oponent.attacking);
		animator.SetFloat ("distanceToOponent", getDistanceToOponennt ());

		if (Time.time - randomSetTime > 1) {
			random = Random.value;
			randomSetTime = Time.time;
		}
		animator.SetFloat ("random", random);
	}
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("health", healtPercent);

		if (oponent != null) {
			animator.SetFloat ("oponent_health", oponent.healtPercent);
		} else {
			animator.SetFloat ("oponent_health", 1);
		}

		if (enable) {

			if (player == PlayerType.player1) {
				UpdatePlayer1Input ();
				FighterStateBehavior.p1 = this;
				// Hadoken.movementForce = -400;
			} else if (player == PlayerType.player2) {
				UpdatePlayer2Input ();
				FighterStateBehavior.p2 = this;
				// Hadoken.movementForce = 200;
			} else if (player == PlayerType.HUMAN) {
				UpdateHumanInput ();
			} else {
				UpdateAiInput ();
			}

		}

		if (healt <= 0 && currentState != FighterStates.DEAD) {
			animator.SetTrigger ("DEAD");
		}
	}
	private float getDistanceToOponennt () {
		return Mathf.Abs (transform.position.x - oponent.transform.position.x);
	}
	public virtual void hurt (float damage) {
		if (!invulnerable) {
			if (defending) {
				damage = 0f;
				// animator.SetTrigger ("TAKE_HIT");
				print ("defending is true");
				animator.SetBool ("DEFEND", true);
				return;
			}
			if (animator.GetBool ("DEFEND") == true && FighterBrokeDeffendBehaviour.BrokeDeffend == false)
				damage = 0;
			if (animator.GetBool ("DEFEND") == true && FighterBrokeDeffendBehaviour.BrokeDeffend == true) {
				damage *= 0.20f;
				animator.SetTrigger ("TAKE_HIT");
				print ("defend is broken and damge: " + damage);
			}
			if (healt >= damage) {
				healt -= damage;
			} else {
				healt = 0;
				capculeCollider.height = 0.1f;
				capculeCollider.center = new Vector3 (0, 0.12f, 0);
			}

			if (healt > 0 && animator.GetBool ("DEFEND") == false) {
				print ("got hit");
				animator.SetTrigger ("TAKE_HIT");
			}
			// StartCoroutine (RestRotation ());
		}
	}
	public void playSound (AudioClip sound) {
		GameUtils.playSound (sound, audioPlayer);
	}
	public bool invulnerable {
		get {
			return currentState == FighterStates.TAKE_HIT ||
				currentState == FighterStates.TAKE_HIT_DEFEND ||
				currentState == FighterStates.DEAD;
		}
	}
	public bool defending {
		get {
			return currentState == FighterStates.DEFEND ||
				currentState == FighterStates.TAKE_HIT_DEFEND;
		}
	}
	public bool attacking {
		get {
			return currentState == FighterStates.ATTACK;
		}
	}
	public float healtPercent {
		get {
			return healt / MAX_HEALTH;
		}
	}
	public Rigidbody body {
		get {
			return this.myBody;
		}
	}

	public void flyingAnimation () {
		if (animator.GetBool ("DEFEND") == true)
			return;
		print ("flying kics hit");
		fighterRotation = this.GetComponent<Transform> ().rotation;
		animator.SetTrigger ("FLY");
		StartCoroutine (RestRotation ());
	}

	IEnumerator RestRotation () {
		yield return new WaitForSeconds (2);
		this.GetComponent<Transform> ().rotation = Quaternion.Euler (0, fighterRotation.eulerAngles.y, 0);
	}
}