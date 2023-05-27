using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TruckControlMouse : MonoBehaviour {
	bool Challenge;
	public float TruckRunSpeed;
	public static float TruckTrueSpeed;
	float StartTime, NowTime;
	public static float TrueTime;
	public GameObject truck;

	public static float StartReciprocalTime;
	public GameObject StartReciprocalTimeText;
	public GameObject ReTimeUI;

	public static int PauseTimes;
	// Use this for initialization
	void Start () {
		Challenge = true;
		StartReciprocalTime = 3;
		StartTime = Time.time + 3;
		PauseTimes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Challenge == true&&TurnCollisionDetenction.TruckStop==false) {
			if (StartReciprocalTime > 0) {
				StartReciprocalTime -= Time.deltaTime;
				StartReciprocalTimeText.GetComponent<Text> ().text = "" + Mathf.CeilToInt (StartReciprocalTime);
				if (GameControl.PauseOK == true) {
					PauseTimes++;
					GameControl.PauseOK = false;
				}
			} else {
				ReTimeUI.SetActive (false);
				StartReciprocalTimeText.GetComponent<Text> ().text = "";
				ChallengeStart ();
			}
		}
	}

	void ChallengeStart(){

		NowTime = Time.time;
		TrueTime = NowTime - StartTime;

		TruckTrueSpeed = (-TruckRunSpeed - (TrueTime-PauseTimes*3) * 1.5f);
		transform.Translate (TruckTrueSpeed * Time.deltaTime, 0, 0);
		//print (Time.time);
	}

	void OnCollisionEnter(Collision CowboyCollision){
		if (CowboyCollision.collider.tag == "Wall") {
			Challenge = false;
			GameControl.GameOverBool = true;
			//TurnCollisionDetenction.TruckStop = true;
			TruckTrueSpeed=0;
		}
	}
}
