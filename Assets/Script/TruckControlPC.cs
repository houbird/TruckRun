using UnityEngine;
using System.Collections;
public class TruckControlPC : MonoBehaviour {
	bool Challenge;
	public float TruckRunSpeed;


	// Use this for initialization
	void Start () {
		Challenge = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (Challenge == true) {
				ChallengeStart ();
		}
	}

	void ChallengeStart(){
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (0, 0, TruckRunSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision CowboyCollision){
		if (CowboyCollision.collider.tag == "Wall") {
			Challenge = false;
			GameControl.GameOverBool = true;
		}
	}
}
