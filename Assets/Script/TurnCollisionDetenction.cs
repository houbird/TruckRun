using UnityEngine;
using System.Collections;

public class TurnCollisionDetenction : MonoBehaviour {
	bool ReadytoTurn,ReadytoTurnRight,ReadytoTurnLeft,SuccessTurn;
	public static bool TruckStop;
	public GameObject player;
	public GameObject T_TurnRight_map;
	public GameObject T_TurnLeft_map;
	public GameObject Center;
	Vector2 MouseDownPos,MouseUpPos;
	float HorizontalDistance,VerticalDistance;
	// Use this for initialization
	void Start () {
		ReadytoTurn = false;
		ReadytoTurnRight = false;
		ReadytoTurnLeft = false;
		SuccessTurn = false;
		TruckStop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(ReadytoTurnLeft||ReadytoTurnRight||ReadytoTurn&&!GameControl.GameOverBool){
			ControlMouse ();
		}
	}

	void ControlMouse(){
		if (Input.GetMouseButtonDown (0)) {
			MouseDownPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			MouseUpPos = Input.mousePosition;
			DirectionChoose ();
		}
	}

	void DirectionChoose(){
		HorizontalDistance = MouseUpPos.y - MouseDownPos.y;
		VerticalDistance = MouseUpPos.x - MouseDownPos.x;
		if (Mathf.Abs (HorizontalDistance) < Mathf.Abs (VerticalDistance)) {
			SuccessTurn=true;
		}
	}

	void LeftRight(){
		GameControl.MapDistance = 0;
		GameObject FindTurnMap = GameObject.FindGameObjectWithTag ("TurnMap");
		Destroy (FindTurnMap);
		player.transform.position = new Vector3 (0, 2.8f, -0.2f);
		Center.transform.rotation = Quaternion.identity;
		if (VerticalDistance < 0&&!ReadytoTurnRight) {
			//Left
			Instantiate (T_TurnLeft_map, new Vector3 (0, 0, 0), Quaternion.identity);
		}
		if (VerticalDistance > 0&&!ReadytoTurnLeft) {
			//Right
			Instantiate (T_TurnRight_map, new Vector3 (0, 0, 0), Quaternion.identity);
		}
		ReadytoTurn = false;
		ReadytoTurnRight = false;
		ReadytoTurnLeft = false;
	}

	void OnTriggerEnter(Collider CollisionDetection){
		if (CollisionDetection.tag == "Turn") {
			ReadytoTurn = true;
		}
		if (CollisionDetection.tag == "TurnRight") {
			ReadytoTurnRight = true;
		}
		if (CollisionDetection.tag == "TurnLeft") {
			ReadytoTurnLeft = true;
		}
	}
	void OnTriggerExit(Collider CollisionDetection){
		if (SuccessTurn) {
			SuccessTurn = false;
			StopAllCoroutines();
			StartCoroutine(Rotate(0));
		}
	}

	IEnumerator Rotate(float rotationAmount)
	{
		TruckStop = true;
		if (VerticalDistance < 0&&!ReadytoTurnRight) {
			//Left
			for (int i = 0; i < 9; i++) {
				Center.transform.Rotate (new Vector3 (0, -10, 0), Space.World);
				yield return 0;
			}
			LeftRight ();
		}
		if (VerticalDistance > 0&&!ReadytoTurnLeft) {
			//Right
			for (int i = 0; i < 9; i++) {
				Center.transform.Rotate (new Vector3 (0, 10, 0), Space.World);
				yield return 0;
			}
			LeftRight ();
		}
		TruckStop = false;
	}
}

