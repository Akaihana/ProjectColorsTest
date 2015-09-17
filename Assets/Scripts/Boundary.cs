using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerExit2D(Collider2D other){
		Destroy(other.gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
