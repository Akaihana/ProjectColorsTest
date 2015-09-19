using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //variable declaration
	public float playerLevel = 1;                   //ship level
	public float moveSpeed = 0.1f;                  //ship move speed
    private bool special = false;
    private float specialTime = 2f;
    private float specialTimeFinish = 0f;

	private float maxX = 3.35f;                     //player boundary maximum x
	private float maxY = 4.85f;                     //player boundary maximum y

	private float bulletTime1 = 0;                  //bullet timer for bullet type 1
	private float bulletTime2 = 0;                  //bullet timer for bullet type 2
	private float bulletTime3 = 0;                  //bullet timer for bullet type 3
    public float bulletDelay1 = 0.075f;             //bullet delay timer for bullet type 1
	public float bulletDelay2 = 0.5f;               //bullet delay timer for bullet type 2
    public float bulletDelay3 = 0.1f;               //bullet delay timer for bullet type 3

    private float bulletOffset1 = 0.4f;             //bullet offset in the y direction
    private float bulletOffset2 = 0.8f;             //bullet offset 2 in the y direction
    private float bulletRotationOffset1 = 8f;       //bullet rotation offset
    private float bulletRotationOffset2 = 16f;      //bullet rotation offset 2

    private float specialOffset = 0f;
    //bullets
	public GameObject bullet1;
	public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;

    //normal bullet spawn points
 	public GameObject bulletSpawn1;
	public GameObject bulletSpawn2;

    //orbiters
    private bool destination = false;               //orbiter rotation management
    private float rotationSpeed = 300f;             //orbiter rotation speed
    private float destinationSpeed = 0.1f;          //orbiter speed management

    public GameObject orbiter1;
	public GameObject orbiter1WaypointA;
	public GameObject orbiter1WaypointB;
   

	public GameObject orbiter2;
	public GameObject orbiter2WaypointA;
	public GameObject orbiter2WaypointB;
    public GameObject orbiter2WaypointC;

    public GameObject orbiter3;
	public GameObject orbiter3WaypointA;
	public GameObject orbiter3WaypointB;
    public GameObject orbiter3WaypointC;

    public GameObject orbiter4;
	public GameObject orbiter4WaypointA;
	public GameObject orbiter4WaypointB;

	public bool shipFocus = false;              //ship focus status boolean

	void Start ()
    {
        //at start, only set lvl 1 orbiter to be active
		orbiter1.SetActive(true);
		orbiter2.SetActive(false);
		orbiter3.SetActive(false);
		orbiter4.SetActive(false);
	}


	// Update is called once per frame
	void Update ()
    {
		//move the Player
		this.movePlayer();
        //handles bullet shots
        this.shootBullet();
        //handles special shots
        this.shootSpecial();
        //handles the control for the ship orbiters
		this.controlOrbiter();
	}

	void movePlayer()
    {
        //player movement using translation
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 horizontalTrans = new Vector3(horizontal * moveSpeed, 0, 0);
        this.transform.position += horizontalTrans;

        float vertical = Input.GetAxis("Vertical");
		Vector3 verticalTrans = new Vector3(0, vertical * moveSpeed, 0);
		this.transform.position += verticalTrans;

        //player boundary
        if (this.transform.position.x >= maxX)
        {
			this.transform.position = new Vector2(maxX, transform.position.y);
		}
		else if (this.transform.position.x <= -maxX)
        {
			this.transform.position = new Vector2(-maxX, transform.position.y);
		}
		
		if (this.transform.position.y >= maxY)
        {
			this.transform.position = new Vector2(transform.position.x, maxY);
		}
		else if (transform.position.y <= -maxY)
        {
			this.transform.position = new Vector2(transform.position.x, -maxY);
		}

        //listen for player input for ship Focus
		float focus = Input.GetAxis("Ship Focus");
		if (focus == 1)
			shipFocus = true;
		else
			shipFocus = false;

		if (shipFocus)
			moveSpeed = 0.05f;
		else
			moveSpeed = 0.1f;

        if (Input.GetKeyDown("1"))
        {
            playerLevel = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            playerLevel = 2;
        }
        else if (Input.GetKeyDown("3"))
        {
            playerLevel = 3;
        }
        else if (Input.GetKeyDown("4"))
        {
            playerLevel = 4;
        }
    }

	void shootBullet()
    {
		if (Input.GetButton("Fire1") && Time.time > bulletTime1)
        {
			if (!shipFocus)
            {
				//normal ship fire
				if (Time.time > bulletTime1)
                {
					if (playerLevel >= 1)
                    {
						Instantiate(bullet1, bulletSpawn1.transform.position, bulletSpawn1.transform.rotation);
						Instantiate(bullet1, bulletSpawn2.transform.position, bulletSpawn2.transform.rotation);
					}
					bulletTime1 = Time.time + bulletDelay1;
				}
				//orbiter fire
				if (Time.time > bulletTime2)
                {
					if (playerLevel == 1)
                    {
                        Instantiate(bullet2, orbiter1.transform.position, Quaternion.Euler(0, 0, 0)) ;
						Instantiate(bullet2, new Vector3(orbiter1.transform.position.x, orbiter1.transform.position.y - bulletOffset1, orbiter1.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter1.transform.position.x, orbiter1.transform.position.y - bulletOffset1, orbiter1.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));
					}
					else if (playerLevel == 2)
                    {
                        Instantiate(bullet2, orbiter2.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet2, new Vector3(orbiter2.transform.position.x, orbiter2.transform.position.y - bulletOffset1, orbiter2.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
                        Instantiate(bullet2, new Vector3(orbiter2.transform.position.x, orbiter2.transform.position.y - bulletOffset1, orbiter2.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));

                        Instantiate(bullet2, orbiter3.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet2, new Vector3(orbiter3.transform.position.x, orbiter3.transform.position.y - bulletOffset1, orbiter3.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
                        Instantiate(bullet2, new Vector3(orbiter3.transform.position.x, orbiter3.transform.position.y - bulletOffset1, orbiter3.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));
                    }
					else if (playerLevel == 3)
                    {
						Instantiate(bullet2, orbiter1.transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullet2, new Vector3(orbiter1.transform.position.x, orbiter1.transform.position.y - bulletOffset1, orbiter1.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter1.transform.position.x, orbiter1.transform.position.y - bulletOffset1, orbiter1.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));

						Instantiate(bullet2, orbiter2.transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullet2, new Vector3(orbiter2.transform.position.x, orbiter2.transform.position.y - bulletOffset1, orbiter2.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter2.transform.position.x, orbiter2.transform.position.y - bulletOffset2, orbiter2.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset2));
						
						Instantiate(bullet2, orbiter3.transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullet2, new Vector3(orbiter3.transform.position.x, orbiter3.transform.position.y - bulletOffset1, orbiter3.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter3.transform.position.x, orbiter3.transform.position.y - bulletOffset2, orbiter3.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset2));
					}
					else if (playerLevel == 4)
                    {
                        Instantiate(bullet2, orbiter1.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet2, new Vector3(orbiter1.transform.position.x, orbiter1.transform.position.y - bulletOffset1, orbiter1.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
                        Instantiate(bullet2, new Vector3(orbiter1.transform.position.x, orbiter1.transform.position.y - bulletOffset1, orbiter1.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));

                        Instantiate(bullet2, orbiter2.transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullet2, new Vector3(orbiter2.transform.position.x, orbiter2.transform.position.y - bulletOffset1, orbiter2.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter2.transform.position.x, orbiter2.transform.position.y - bulletOffset2, orbiter2.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset2));
						
						Instantiate(bullet2, orbiter3.transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullet2, new Vector3(orbiter3.transform.position.x, orbiter3.transform.position.y - bulletOffset1, orbiter3.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter3.transform.position.x, orbiter3.transform.position.y - bulletOffset2, orbiter3.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset2));

						Instantiate(bullet2, orbiter4.transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullet2, new Vector3(orbiter4.transform.position.x, orbiter4.transform.position.y - bulletOffset1, orbiter4.transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset1));
						Instantiate(bullet2, new Vector3(orbiter4.transform.position.x, orbiter4.transform.position.y - bulletOffset1, orbiter4.transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset1));
					}
					bulletTime2 = Time.time + bulletDelay2;
				}
			}
			else
            {
				//focus fire
                if(Time.time > bulletTime3)
                {
                    if (playerLevel == 1)
                    {
                        Instantiate(bullet3, this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter1.transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (playerLevel == 2)
                    {
                        Instantiate(bullet3, this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter2.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter3.transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (playerLevel == 3)
                    {
                        Instantiate(bullet3, this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter1.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter2.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter3.transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (playerLevel == 4)
                    {
                        Instantiate(bullet3, this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter1.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter2.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter3.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullet3, orbiter4.transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    bulletTime3 = Time.time + bulletDelay3;
                }
			}
		}
	}

    void shootSpecial()
    {
        if (Input.GetButtonDown("SpecialFire1") && !special)
        {
            Debug.Log("Special!");
            special = true;
            specialTimeFinish = specialTime + Time.time;
        }
        if (special)
        {
            Instantiate(bullet4, this.transform.position, Quaternion.Euler(0, 0, -specialOffset));
            Instantiate(bullet4, this.transform.position, Quaternion.Euler(0, 0, specialOffset));
            specialOffset += 2f;
            if (Time.time > specialTimeFinish) 
            {
                special = false;
                specialOffset = 0f;
                Debug.Log("Special Finish!");
            }
        }
    }

	void controlOrbiter()
    {
		if (playerLevel == 1)
        {
			orbiter1.SetActive(true);
			orbiter2.SetActive(false);
			orbiter3.SetActive(false);
			orbiter4.SetActive(false);
		}
		else if (playerLevel == 2)
        {
			orbiter1.SetActive(false);
			orbiter2.SetActive(true);
			orbiter3.SetActive(true);
			orbiter4.SetActive(false);
		}
		else if (playerLevel == 3)
        {
			orbiter1.SetActive(true);
			orbiter2.SetActive(true);
			orbiter3.SetActive(true);
			orbiter4.SetActive(false);
		}
		else if (playerLevel == 4)
        {
			orbiter1.SetActive(true);
			orbiter2.SetActive(true);
			orbiter3.SetActive(true);
			orbiter4.SetActive(true);
		}

        if (!destination)
        {
            if (playerLevel == 1)
            {
                if (orbiter1.transform.position == orbiter1WaypointB.transform.position)
                    destination = true;
            }
            else if (playerLevel == 2)
            {
                if (orbiter2.transform.position == orbiter2WaypointB.transform.position && orbiter3.transform.position == orbiter3WaypointB.transform.position)
                    destination = true;
            }
            else if (playerLevel == 3)
            {
                if (orbiter1.transform.position == orbiter1WaypointB.transform.position && orbiter2.transform.position == orbiter2WaypointC.transform.position &&
                    orbiter3.transform.position == orbiter3WaypointC.transform.position)
                    destination = true;
            }
            else if (playerLevel == 4)
            {
                if (orbiter1.transform.position == orbiter1WaypointB.transform.position && orbiter2.transform.position == orbiter2WaypointB.transform.position &&
                    orbiter3.transform.position == orbiter3WaypointB.transform.position && orbiter4.transform.position == orbiter4WaypointB.transform.position)
                    destination = true;
            }
            else
                destination = false;
        }
        if (shipFocus)
        {
			if (playerLevel == 1)
            {
                if (destination)
                    orbiter1.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                else
                    orbiter1.transform.position = Vector3.MoveTowards(orbiter1.transform.position, orbiter1WaypointB.transform.position, destinationSpeed);
            }
            else if (playerLevel == 2)
            {
                if (destination)
                {
                    orbiter2.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiter3.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    orbiter2.transform.position = Vector3.MoveTowards(orbiter2.transform.position, orbiter2WaypointB.transform.position, destinationSpeed);
                    orbiter3.transform.position = Vector3.MoveTowards(orbiter3.transform.position, orbiter3WaypointB.transform.position, destinationSpeed);
                }
            }
            else if (playerLevel == 3)
            {
                if (destination)
                {
                    orbiter1.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiter2.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiter3.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    orbiter1.transform.position = Vector3.MoveTowards(orbiter1.transform.position, orbiter1WaypointB.transform.position, destinationSpeed);
                    orbiter2.transform.position = Vector3.MoveTowards(orbiter2.transform.position, orbiter2WaypointC.transform.position, destinationSpeed);
                    orbiter3.transform.position = Vector3.MoveTowards(orbiter3.transform.position, orbiter3WaypointC.transform.position, destinationSpeed);
                }
            }
            else if (playerLevel == 4)
            {
                if (destination)
                {
                    orbiter1.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiter2.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiter3.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiter4.transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    orbiter1.transform.position = Vector3.MoveTowards(orbiter1.transform.position, orbiter1WaypointB.transform.position, destinationSpeed);
                    orbiter2.transform.position = Vector3.MoveTowards(orbiter2.transform.position, orbiter2WaypointB.transform.position, destinationSpeed);
                    orbiter3.transform.position = Vector3.MoveTowards(orbiter3.transform.position, orbiter3WaypointB.transform.position, destinationSpeed);
                    orbiter4.transform.position = Vector3.MoveTowards(orbiter4.transform.position, orbiter4WaypointB.transform.position, destinationSpeed);
                }
            }

		}
		else
        {
            if (playerLevel == 1)
            {
                orbiter1.transform.position = Vector3.MoveTowards(orbiter1.transform.position, orbiter1WaypointA.transform.position, destinationSpeed);
            }
            else if (playerLevel == 2)
            {
                orbiter2.transform.position = Vector3.MoveTowards(orbiter2.transform.position, orbiter2WaypointA.transform.position, destinationSpeed);
                orbiter3.transform.position = Vector3.MoveTowards(orbiter3.transform.position, orbiter3WaypointA.transform.position, destinationSpeed);
            }
            else if (playerLevel == 3)
            {
                orbiter1.transform.position = Vector3.MoveTowards(orbiter1.transform.position, orbiter1WaypointA.transform.position, destinationSpeed);
                orbiter2.transform.position = Vector3.MoveTowards(orbiter2.transform.position, orbiter2WaypointA.transform.position, destinationSpeed);
                orbiter3.transform.position = Vector3.MoveTowards(orbiter3.transform.position, orbiter3WaypointA.transform.position, destinationSpeed);
            }
            else if (playerLevel == 4)
            {
                orbiter1.transform.position = Vector3.MoveTowards(orbiter1.transform.position, orbiter1WaypointA.transform.position, destinationSpeed);
                orbiter2.transform.position = Vector3.MoveTowards(orbiter2.transform.position, orbiter2WaypointA.transform.position, destinationSpeed);
                orbiter3.transform.position = Vector3.MoveTowards(orbiter3.transform.position, orbiter3WaypointA.transform.position, destinationSpeed);
                orbiter4.transform.position = Vector3.MoveTowards(orbiter4.transform.position, orbiter4WaypointA.transform.position, destinationSpeed);
            }
            destination = false;
        }

	}
}
