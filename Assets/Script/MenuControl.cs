using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

	public GameObject MenuScoreText;
	public GameObject RankScore;
	bool RankAvailable;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("Score")==false){
			PlayerPrefs.SetInt("Score", 0);
		}

		RankAvailable = false;
	}
	
	// Update is called once per frame
	void Update () {
		MenuScoreText.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Score");
	}

	public void MenuStartButton(){
		Application.LoadLevel (001);
	}

	public void MenuExitButton(){
		Application.Quit();
	}

	public void ShowRankButtonClick(){
		if (RankAvailable==false) {
			RankScore.GetComponent<Animation> ().Play ("FadIn");
			RankAvailable = true;
		} else {
			RankScore.GetComponent<Animation> ().Play ("FadOut");
			RankAvailable = false;
		}
	}
}
