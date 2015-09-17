using UnityEngine;
using System.Collections;

public class Notes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*
		Vector2 startingPosition = new Vector2(0, -4);
		this.GetComponent<Rigidbody2D>().position = startingPosition;
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
		/*
		//player movement using velocity
		float horizontal = Input.GetAxis("Horizontal");
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * shipSpeed, GetComponent<Rigidbody2D>().velocity.y);

		float vertical = Input.GetAxis("Vertical");
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, vertical * shipSpeed);

		//player boundary
		if(this.GetComponent<Rigidbody2D>().position.x >= maxX){
			//print ("im out of the boundary on the right side");
			this.GetComponent<Rigidbody2D>().position = new Vector2(maxX, GetComponent<Rigidbody2D>().position.y);
		}
		else if(this.GetComponent<Rigidbody2D>().position.x <= -maxX){
			this.GetComponent<Rigidbody2D>().position = new Vector2(-maxX, GetComponent<Rigidbody2D>().position.y);
		}
		if(this.GetComponent<Rigidbody2D>().position.y >= maxY){
			this.GetComponent<Rigidbody2D>().position = new Vector2(GetComponent<Rigidbody2D>().position.x, maxY);
		}
		else if(this.GetComponent<Rigidbody2D>().position.y <= -maxY){
			this.GetComponent<Rigidbody2D>().position = new Vector2(GetComponent<Rigidbody2D>().position.x, -maxY);
		}
		*/
	}
}
