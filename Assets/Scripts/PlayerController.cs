using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float playerLevel = 1;
	public float moveSpeed = 0.1f;

	private float maxX = 2.85f;
	private float maxY = 4.85f; 

	private float bulletTime = 0;
	private float bulletTime2 = 0;
	public float bulletDelay = 0.075f;
	public float bulletDelay2 = 0.5f;

	public GameObject bullet1;
	public GameObject bullet2;

 	public GameObject bulletSpawn1;
	public GameObject bulletSpawn2;
	[HideInInspector]
	public GameObject bulletSpawn3;
	[HideInInspector]
	public GameObject bulletSpawn4;

	public GameObject Orbiter1;
	public GameObject Orbiter1WaypointA;
	public GameObject Orbiter1WaypointB;

	public GameObject Orbiter2;
	public GameObject Orbiter2WaypointA;
	public GameObject Orbiter2WaypointB;

	public GameObject Orbiter3;
	public GameObject Orbiter3WaypointA;
	public GameObject Orbiter3WaypointB;

	public GameObject Orbiter4;
	public GameObject Orbiter4WaypointA;
	public GameObject Orbiter4WaypointB;

	public GameObject Orbiter5;
	public GameObject Orbiter5WaypointA;
	public GameObject Orbiter5WaypointB;

	public bool shipFocus = false;

	void Start () {
		Orbiter1.SetActive(true);
		Orbiter2.SetActive(false);
		Orbiter3.SetActive(false);
		Orbiter4.SetActive(false);
		Orbiter5.SetActive(false);
	}


	// Update is called once per frame
	void Update () {

		//move the PlayerShip
		this.movePlayer();
		this.Shootbullet();


		this.OrbiterControl();
	}

	void movePlayer(){
	
		//player movement using translation
		float horizontal = Input.GetAxis("Horizontal");
		Vector2 horizontalTrans = new Vector2(horizontal * moveSpeed, 0);
		this.GetComponent<Rigidbody2D>().position += horizontalTrans;
		
		float vertical = Input.GetAxis("Vertical");
		Vector2 verticalTrans = new Vector2(0, vertical * moveSpeed);
		this.GetComponent<Rigidbody2D>().position += verticalTrans;
		
		//player boundary
		if(this.GetComponent<Rigidbody2D>().position.x >= maxX){
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

		float focus = Input.GetAxis("Ship Focus");
		if(focus == 1)
			shipFocus = true;
		else
			shipFocus = false;

		if(shipFocus)
			moveSpeed = 0.05f;
		else
			moveSpeed = 0.1f;

	}

	void Shootbullet(){
		if(Input.GetButton("Fire1") && Time.time > bulletTime){
			if(!shipFocus){
				//normal ship fire
				if(Time.time > bulletTime){
					if(playerLevel >= 1){
						Instantiate(bullet1, bulletSpawn1.transform.position, bulletSpawn1.transform.rotation);
						Instantiate(bullet1, bulletSpawn2.transform.position, bulletSpawn2.transform.rotation);
					}
					if(playerLevel >= 2){
						//Instantiate(bullet1, bulletSpawn3.transform.position, bulletSpawn3.transform.rotation);
						//8Instantiate(bullet1, bulletSpawn4.transform.position, bulletSpawn4.transform.rotation);
					}
					bulletTime = Time.time + bulletDelay;
				}
				//orbiter fire
				if(Time.time > bulletTime2){
					if(playerLevel == 1){
						Instantiate(bullet2, Orbiter1.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter1.transform.position.x, Orbiter1.transform.position.y - 0.2f, Orbiter1.transform.position.z), Quaternion.Euler(0, 0, -8f));
						Instantiate(bullet2, new Vector3(Orbiter1.transform.position.x, Orbiter1.transform.position.y - 0.2f, Orbiter1.transform.position.z), Quaternion.Euler(0, 0, 8f));

						/*
						Instantiate(bullet2, Orbiter1.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, Orbiter1.transform.position, Quaternion.Euler(0, 0, -10f));
						Instantiate(bullet2, Orbiter1.transform.position, Quaternion.Euler(0, 0, 10f));
						*/
					}
					if(playerLevel == 2){
						Instantiate(bullet2, Orbiter2.transform.position, Orbiter2.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter2.transform.position.x, Orbiter2.transform.position.y - 0.2f, Orbiter2.transform.position.z), Quaternion.Euler(0, 0, 8f));
						Instantiate(bullet2, new Vector3(Orbiter2.transform.position.x, Orbiter2.transform.position.y - 0.4f, Orbiter2.transform.position.z), Quaternion.Euler(0, 0, 16f));

						Instantiate(bullet2, Orbiter3.transform.position, Orbiter3.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter3.transform.position.x, Orbiter3.transform.position.y - 0.2f, Orbiter3.transform.position.z), Quaternion.Euler(0, 0, -8f));
						Instantiate(bullet2, new Vector3(Orbiter3.transform.position.x, Orbiter3.transform.position.y - 0.4f, Orbiter3.transform.position.z), Quaternion.Euler(0, 0, -16f));

						/*
						Instantiate(bullet2, Orbiter2.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, Orbiter2.transform.position, Quaternion.Euler(0, 0, 7.5f));
						Instantiate(bullet2, Orbiter2.transform.position, Quaternion.Euler(0, 0, 15f));

						Instantiate(bullet2, Orbiter3.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, Orbiter3.transform.position, Quaternion.Euler(0, 0, -7.5f));
						Instantiate(bullet2, Orbiter3.transform.position, Quaternion.Euler(0, 0, -15f));
						*/
					}
					if(playerLevel == 3){
						Instantiate(bullet2, Orbiter1.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter1.transform.position.x, Orbiter1.transform.position.y - 0.2f, Orbiter1.transform.position.z), Quaternion.Euler(0, 0, -8f));
						Instantiate(bullet2, new Vector3(Orbiter1.transform.position.x, Orbiter1.transform.position.y - 0.2f, Orbiter1.transform.position.z), Quaternion.Euler(0, 0, 8f));

						Instantiate(bullet2, Orbiter2.transform.position, Orbiter2.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter2.transform.position.x, Orbiter2.transform.position.y - 0.2f, Orbiter2.transform.position.z), Quaternion.Euler(0, 0, 8f));
						Instantiate(bullet2, new Vector3(Orbiter2.transform.position.x, Orbiter2.transform.position.y - 0.4f, Orbiter2.transform.position.z), Quaternion.Euler(0, 0, 16f));
						
						Instantiate(bullet2, Orbiter3.transform.position, Orbiter3.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter3.transform.position.x, Orbiter3.transform.position.y - 0.2f, Orbiter3.transform.position.z), Quaternion.Euler(0, 0, -8f));
						Instantiate(bullet2, new Vector3(Orbiter3.transform.position.x, Orbiter3.transform.position.y - 0.4f, Orbiter3.transform.position.z), Quaternion.Euler(0, 0, -16f));

						/*
						Instantiate(bullet2, Orbiter1.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, Orbiter1.transform.position, Quaternion.Euler(0, 0, -10f));
						Instantiate(bullet2, Orbiter1.transform.position, Quaternion.Euler(0, 0, 10f));

						Instantiate(bullet2, Orbiter2.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, Orbiter2.transform.position, Quaternion.Euler(0, 0, 7.5f));
						Instantiate(bullet2, Orbiter2.transform.position, Quaternion.Euler(0, 0, 15f));
						
						Instantiate(bullet2, Orbiter3.transform.position, Orbiter1.transform.rotation);
						Instantiate(bullet2, Orbiter3.transform.position, Quaternion.Euler(0, 0, -7.5f));
						Instantiate(bullet2, Orbiter3.transform.position, Quaternion.Euler(0, 0, -15f));
						*/
					}
					if(playerLevel == 4){

						Instantiate(bullet2, Orbiter2.transform.position, Orbiter2.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter2.transform.position.x, Orbiter2.transform.position.y - 0.2f, Orbiter2.transform.position.z), Quaternion.Euler(0, 0, 8f));
						Instantiate(bullet2, new Vector3(Orbiter2.transform.position.x, Orbiter2.transform.position.y - 0.4f, Orbiter2.transform.position.z), Quaternion.Euler(0, 0, 16f));
						
						Instantiate(bullet2, Orbiter3.transform.position, Orbiter3.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter3.transform.position.x, Orbiter3.transform.position.y - 0.2f, Orbiter3.transform.position.z), Quaternion.Euler(0, 0, -8f));
						Instantiate(bullet2, new Vector3(Orbiter3.transform.position.x, Orbiter3.transform.position.y - 0.4f, Orbiter3.transform.position.z), Quaternion.Euler(0, 0, -16f));

						Instantiate(bullet2, Orbiter4.transform.position, Orbiter4.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter4.transform.position.x, Orbiter4.transform.position.y - 0.2f, Orbiter4.transform.position.z), Quaternion.Euler(0, 0, 8f));
						Instantiate(bullet2, new Vector3(Orbiter4.transform.position.x, Orbiter4.transform.position.y - 0.4f, Orbiter4.transform.position.z), Quaternion.Euler(0, 0, 16f));
						
						Instantiate(bullet2, Orbiter5.transform.position, Orbiter5.transform.rotation);
						Instantiate(bullet2, new Vector3(Orbiter5.transform.position.x, Orbiter5.transform.position.y - 0.2f, Orbiter5.transform.position.z), Quaternion.Euler(0, 0, -8f));
						Instantiate(bullet2, new Vector3(Orbiter5.transform.position.x, Orbiter5.transform.position.y - 0.4f, Orbiter5.transform.position.z), Quaternion.Euler(0, 0, -16f));


						/*
						Instantiate(bullet2, Orbiter2.transform.position, Orbiter2.transform.rotation);
						Instantiate(bullet2, Orbiter2.transform.position, Quaternion.Euler(0, 0, -10f));
						Instantiate(bullet2, Orbiter2.transform.position, Quaternion.Euler(0, 0, 10f));
						
						Instantiate(bullet2, Orbiter3.transform.position, Orbiter3.transform.rotation);
						Instantiate(bullet2, Orbiter3.transform.position, Quaternion.Euler(0, 0, -10f));
						Instantiate(bullet2, Orbiter3.transform.position, Quaternion.Euler(0, 0, 10f));
						
						Instantiate(bullet2, Orbiter4.transform.position, Orbiter4.transform.rotation);
						Instantiate(bullet2, Orbiter4.transform.position, Quaternion.Euler(0, 0, -10f));
						Instantiate(bullet2, Orbiter4.transform.position, Quaternion.Euler(0, 0, 10f));

						Instantiate(bullet2, Orbiter5.transform.position, Orbiter5.transform.rotation);
						Instantiate(bullet2, Orbiter5.transform.position, Quaternion.Euler(0, 0, -10f));
						Instantiate(bullet2, Orbiter5.transform.position, Quaternion.Euler(0, 0, 10f));
						*/


					}
					bulletTime2 = Time.time + bulletDelay2;
				}
			}
			else{
				//focus fire
			}
		}
	}

	void OrbiterControl(){
		if(playerLevel == 1){
			Orbiter1.SetActive(true);
			Orbiter2.SetActive(false);
			Orbiter3.SetActive(false);
			Orbiter4.SetActive(false);
			Orbiter5.SetActive(false);
		}
		if(playerLevel == 2){
			Orbiter1.SetActive(false);
			Orbiter2.SetActive(true);
			Orbiter3.SetActive(true);
			Orbiter4.SetActive(false);
			Orbiter5.SetActive(false);
		}
		if(playerLevel == 3){
			Orbiter1.SetActive(true);
			Orbiter2.SetActive(true);
			Orbiter3.SetActive(true);
			Orbiter4.SetActive(false);
			Orbiter5.SetActive(false);
		}
		if(playerLevel == 4){
			Orbiter1.SetActive(false);
			Orbiter2.SetActive(true);
			Orbiter3.SetActive(true);
			Orbiter4.SetActive(true);
			Orbiter5.SetActive(true);
		}

		if(shipFocus){
			if(playerLevel == 1)
				//Orbiter1.transform.position = Vector3.Lerp(Orbiter1.transform.position,Orbiter1To.transform.position , .5f);
				Orbiter1.transform.position = Vector3.MoveTowards(Orbiter1.transform.position, Orbiter1WaypointB.transform.position, 0.25f);
		}
		else{	
			if(playerLevel == 1)
				//Orbiter1.transform.position = Vector3.Lerp(Orbiter1.transform.position,Orbiter1From.transform.position , .5f);
				Orbiter1.transform.position = Vector3.MoveTowards(Orbiter1.transform.position, Orbiter1WaypointA.transform.position, 0.25f);
		}

	}


}
