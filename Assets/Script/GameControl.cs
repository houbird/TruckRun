using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	public GameObject[] Map;
	public GameObject[] TurnMap;
	public static int MapDistance;
	public static bool GameOverBool;

	public GameObject Truck;
	public GameObject RunSpeedText;
	public GameObject PlayStopButton;
	int Score;
	public static bool CheckPause;
	public GameObject PauseMaskUI, OverMaskUI, ReTimeUI;
	public GameObject NowTripText, PauseTripText, OverTripText;
	public GameObject PauseBestText, OverBestText;

	public static bool PauseOK;
	// Use this for initialization
	void Start () {
		GameOverBool = false;
		CheckPause = false;
		MapDistance = 0;
		PauseMaskUI.SetActive (false);
		OverMaskUI.SetActive (false);
		ReTimeUI.SetActive (true);
		Time.timeScale = 1;
		Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UguiShow();
		ProduceMap();
		if (GameOverBool == true) {
			print ("GameOver");
			OverMaskUI.SetActive (true);
			if (Score > PlayerPrefs.GetInt ("Score")) {
				PlayerPrefs.SetInt ("Score", Score);
			}
		}

	}
		
	void UguiShow(){
		//Score
		Score += (CheckPause==false)&&(TruckControlMouse.StartReciprocalTime)<=0?(Mathf.FloorToInt((-TruckControlMouse.TruckTrueSpeed)))/60:0;
		NowTripText.GetComponent<Text> ().text = "" + Score;
		RunSpeedText.GetComponent<Text> ().text = "" + Mathf.FloorToInt((-TruckControlMouse.TruckTrueSpeed));
		OverTripText.GetComponent<Text> ().text = "" + Score;
		PauseBestText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Score");
		OverBestText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Score");
	}
	void ProduceMap(){
		GameObject FindMap = GameObject.FindGameObjectWithTag ("Map");
		GameObject FindTurnMap = GameObject.FindGameObjectWithTag ("TurnMap");
		if (FindMap == null&&FindTurnMap==null) {
			MapDistance -= 48;
			int RandomMap = Random.Range (0, Map.Length+TurnMap.Length);
			if (RandomMap < Map.Length) {
				Instantiate (Map [RandomMap], new Vector3 (MapDistance, 0, 0), Quaternion.identity);
			} else {
				Instantiate (TurnMap [RandomMap-Map.Length], new Vector3 (MapDistance, 0, 0), Quaternion.identity);
			}
		}
	}

	public void PlayStop (){
		if (OverMaskUI.active == false) {
			if (CheckPause == false) {
				CheckPause = true;
				PauseMaskUI.SetActive (true);
				Time.timeScale = 0;
				PauseTripText.GetComponent<Text> ().text = "" + Score;
				PauseOK = true;
				//TruckControlMouse.PauseTimes ++;
			} else {
				ReTimeUI.SetActive (true);
				TruckControlMouse.StartReciprocalTime = 3;
				CheckPause = false;
				PauseMaskUI.SetActive (false);
				Time.timeScale = 1;
			}
		}
	}

	public void MenuButton(){
		Application.LoadLevel ("Menu");
	}

	public void PauseUI_PlayButton(){
		PauseMaskUI.SetActive (false);
		CheckPause = true;
		Time.timeScale = 1;
	}

	public void EndAgainButton(){
		Application.LoadLevel (001);
	}
}
