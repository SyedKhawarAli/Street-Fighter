using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BannerController : MonoBehaviour {
	public GameObject restartBtn;
	public GameObject winText;
	private Animator animator;
	private AudioSource audioPlayer;
	private bool animating;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		audioPlayer = GetComponent<AudioSource> ();
	}

	public void showRoundFight () {
		animating = true;
		animator.SetTrigger ("SHOW_ROUND_FIGHT");
	}

	public void showYouWin (string playerName) {
		animating = true;
		animator.SetTrigger ("SHOW_YOU_WIN");
		winText.GetComponent<Text> ().text = playerName + "  WON";
		winText.SetActive (true);
		restartBtn.SetActive (true);
	}

	public void showYouLose () {
		animating = true;
		animator.SetTrigger ("SHOW_YOU_LOSE");
	}

	public void playVoice (AudioClip voice) {
		GameUtils.playSound (voice, audioPlayer);
	}

	public void animationEnded () {
		animating = false;
	}

	public bool isAnimating {
		get {
			return animating;
		}
	}

	public void startTempScene () {
		SceneManager.LoadScene ("TempScene");
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update () {
		if (restartBtn.activeSelf) {
			if (Input.GetKeyDown (KeyCode.Joystick1Button5) || Input.GetKeyDown (KeyCode.Joystick2Button5)) {
				startTempScene ();
			}
		}
	}
}