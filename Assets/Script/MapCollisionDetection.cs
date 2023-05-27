using UnityEngine;
using System.Collections;

public class MapCollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider CollisionDetection)
	{
		if (CollisionDetection.tag == "Map") {
			CollisionDetection.tag = "RunMap";
		}
	}
	void OnTriggerExit (Collider CollisionDetection)
	{
		if (CollisionDetection.tag == "RunMap") {
			Destroy (CollisionDetection.gameObject);
		}
	}
}
