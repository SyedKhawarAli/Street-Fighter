using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (loadScene());
	}
	IEnumerator loadScene () {
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("BattleGround");
	}
	// Update is called once per frame
	void Update () {

	}
}