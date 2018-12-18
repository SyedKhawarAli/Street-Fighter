﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParticleAfterUse : MonoBehaviour {
	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (ps) {
			if (!ps.IsAlive ()) {
				Destroy (gameObject);
			}
		}
	}
}