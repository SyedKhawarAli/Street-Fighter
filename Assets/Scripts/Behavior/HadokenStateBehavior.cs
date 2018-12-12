using System.Collections;
using UnityEngine;

public class HadokenStateBehavior : FighterStateBehavior {

	string hadokenPath = "";
	override public void OnStateEnter (Animator animator,
		AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);

		float fighterX = fighter.transform.position.x;

		if (fighter.player == Fighter.PlayerType.player1) {
			Debug.Log ("hello");
			hadokenPath = "Sfx/Hadoken";
		} else {
			Debug.Log ("hello 2");
			hadokenPath = "Sfx/Hadoken_left";
		}

		GameObject instance = Object.Instantiate (
			// Resources.Load("Sfx/Hadoken"),
			Resources.Load (hadokenPath),
			new Vector3 (fighterX, 1, 0),
			Quaternion.Euler (0, 0, 0)
		) as GameObject;

		Hadoken hadoken = instance.GetComponent<Hadoken> ();
		hadoken.caster = fighter;
	}
}