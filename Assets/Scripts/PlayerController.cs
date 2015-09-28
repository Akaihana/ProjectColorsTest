using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //variable declaration
    public float playerLevel = 1;                   //ship level
    public float moveSpeed = 0.1f;                  //ship move speed

    public float specialMeter = 0;
    private float specialMeterMax = 100;
    private float specialCost1 = 25;
    private bool special = false;
    private float specialTime = 4f;
    private float specialTimeFinish = 0f;
    private bool[] specialInterval = { false, false };

    private float maxX = 3.35f;                     //player boundary maximum x
	private float maxY = 4.85f;                     //player boundary maximum y

    private float[] bulletTime = { 0, 0, 0 };
    public float[] bulletDelay = { 0.075f, 0.25f, 0f };
    public float[] bulletOffset = { 0.4f, 0.8f };
    private float[] bulletRotationOffset = { 8f, 16f };

    private float specialOffset = 0f;

    //bullets
    public GameObject[] bullets;

    //normal bullet spawn points
    public GameObject[] bulletSpawn = new GameObject[2];

    //orbiters
    private bool destination = false;               //orbiter rotation management
    private float rotationSpeed = 300f;             //orbiter rotation speed
    private float destinationSpeed = 0.1f;          //orbiter speed management

    public GameObject[] orbiters = new GameObject[4];
    public GameObject[] orbiterWaypoints1 = new GameObject[2];
    public GameObject[] orbiterWaypoints2 = new GameObject[3];
    public GameObject[] orbiterWaypoints3 = new GameObject[3];
    public GameObject[] orbiterWaypoints4 = new GameObject[2];

	public bool shipFocus = false;              //ship focus status boolean

	void Start ()
    {
        //at start, only set lvl 1 orbiter to be active
		orbiters[0].SetActive(true);
		orbiters[1].SetActive(false);
		orbiters[2].SetActive(false);
		orbiters[3].SetActive(false);
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

    //function to move the player
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

        //the ship focus, when in focus lower the ships speed
		if (shipFocus)
			moveSpeed = 0.05f;
		else
			moveSpeed = 0.1f;


        //debugging buttons for testing ship levels
        if (Input.GetKeyDown("1"))
        {
            playerLevel = 1;
            destination = false;
        }
        else if (Input.GetKeyDown("2"))
        {
            playerLevel = 2;
            destination = false;
        }
        else if (Input.GetKeyDown("3"))
        {
            playerLevel = 3;
            destination = false;
        }
        else if (Input.GetKeyDown("4"))
        {
            playerLevel = 4;
            destination = false;
        }
    }

    //function to handle normal and unique player shots
	void shootBullet()
    {
		if (Input.GetButton("Fire1") && Time.time > bulletTime[0])
        {
			if (!shipFocus)
            {
				//normal ship fire
				if (Time.time > bulletTime[0])
                {
					if (playerLevel >= 1)
                    {
						Instantiate(bullets[0], bulletSpawn[0].transform.position, bulletSpawn[0].transform.rotation);
						Instantiate(bullets[0], bulletSpawn[1].transform.position, bulletSpawn[1].transform.rotation);
					}
					bulletTime[0] = Time.time + bulletDelay[0];
				}
				//orbiter fire
				if (Time.time > bulletTime[1])
                {
					if (playerLevel == 1)
                    {
                        Instantiate(bullets[1], orbiters[0].transform.position, Quaternion.Euler(0, 0, 0)) ;
						Instantiate(bullets[1], new Vector3(orbiters[0].transform.position.x, orbiters[0].transform.position.y - bulletOffset[0], orbiters[0].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[0].transform.position.x, orbiters[0].transform.position.y - bulletOffset[0], orbiters[0].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));
					}
					else if (playerLevel == 2)
                    {
                        Instantiate(bullets[1], orbiters[1].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[1], new Vector3(orbiters[1].transform.position.x, orbiters[1].transform.position.y - bulletOffset[0], orbiters[1].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
                        Instantiate(bullets[1], new Vector3(orbiters[1].transform.position.x, orbiters[1].transform.position.y - bulletOffset[0], orbiters[1].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));

                        Instantiate(bullets[1], orbiters[2].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[1], new Vector3(orbiters[2].transform.position.x, orbiters[2].transform.position.y - bulletOffset[0], orbiters[2].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
                        Instantiate(bullets[1], new Vector3(orbiters[2].transform.position.x, orbiters[2].transform.position.y - bulletOffset[0], orbiters[2].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));
                    }
					else if (playerLevel == 3)
                    {
						Instantiate(bullets[1], orbiters[0].transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullets[1], new Vector3(orbiters[0].transform.position.x, orbiters[0].transform.position.y - bulletOffset[0], orbiters[0].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[0].transform.position.x, orbiters[0].transform.position.y - bulletOffset[0], orbiters[0].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));

						Instantiate(bullets[1], orbiters[1].transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullets[1], new Vector3(orbiters[1].transform.position.x, orbiters[1].transform.position.y - bulletOffset[0], orbiters[1].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[1].transform.position.x, orbiters[1].transform.position.y - bulletOffset[1], orbiters[1].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[1]));
						
						Instantiate(bullets[1], orbiters[2].transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullets[1], new Vector3(orbiters[2].transform.position.x, orbiters[2].transform.position.y - bulletOffset[0], orbiters[2].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[2].transform.position.x, orbiters[2].transform.position.y - bulletOffset[1], orbiters[2].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[1]));
					}
					else if (playerLevel == 4)
                    {
                        Instantiate(bullets[1], orbiters[0].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[1], new Vector3(orbiters[0].transform.position.x, orbiters[0].transform.position.y - bulletOffset[0], orbiters[0].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
                        Instantiate(bullets[1], new Vector3(orbiters[0].transform.position.x, orbiters[0].transform.position.y - bulletOffset[0], orbiters[0].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));

                        Instantiate(bullets[1], orbiters[1].transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullets[1], new Vector3(orbiters[1].transform.position.x, orbiters[1].transform.position.y - bulletOffset[0], orbiters[1].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[1].transform.position.x, orbiters[1].transform.position.y - bulletOffset[1], orbiters[1].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[1]));
						
						Instantiate(bullets[1], orbiters[2].transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullets[1], new Vector3(orbiters[2].transform.position.x, orbiters[2].transform.position.y - bulletOffset[0], orbiters[2].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[2].transform.position.x, orbiters[2].transform.position.y - bulletOffset[1], orbiters[2].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[1]));

						Instantiate(bullets[1], orbiters[3].transform.position, Quaternion.Euler(0, 0, 0));
						Instantiate(bullets[1], new Vector3(orbiters[3].transform.position.x, orbiters[3].transform.position.y - bulletOffset[0], orbiters[3].transform.position.z), Quaternion.Euler(0, 0, -bulletRotationOffset[0]));
						Instantiate(bullets[1], new Vector3(orbiters[3].transform.position.x, orbiters[3].transform.position.y - bulletOffset[0], orbiters[3].transform.position.z), Quaternion.Euler(0, 0, bulletRotationOffset[0]));
					}
					bulletTime[1] = Time.time + bulletDelay[1];
				}
			}
			else
            {
				//focus fire
                if(Time.time > bulletTime[2])
                {
                    if (playerLevel == 1)
                    {
                        Instantiate(bullets[2], this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[0].transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (playerLevel == 2)
                    {
                        Instantiate(bullets[2], this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[1].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[2].transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (playerLevel == 3)
                    {
                        Instantiate(bullets[2], this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[0].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[1].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[2].transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    else if (playerLevel == 4)
                    {
                        Instantiate(bullets[2], this.transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[0].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[1].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[2].transform.position, Quaternion.Euler(0, 0, 0));
                        Instantiate(bullets[2], orbiters[3].transform.position, Quaternion.Euler(0, 0, 0));
                    }
                    bulletTime[2] = Time.time + bulletDelay[2];
                }
			}
		}
	}

    //function to handle Special Activation
    void shootSpecial()
    {
        //special meter cannot exceed the maximum special meter amount
        if(specialMeter >= specialMeterMax)
        {
            specialMeter = specialMeterMax;
        }

        //if special is pressed
        if (Input.GetButtonDown("SpecialFire1") && !special && specialMeter >= specialCost1)
        {
            Debug.Log("Special!");
            special = true;
            specialInterval[0] = false;
            specialInterval[1] = false;
            //specialMeter -= 25;
            specialTimeFinish = specialTime + Time.time;
            
        }
        //if special is pressed but not enough meter
        else if(Input.GetButtonDown("SpecialFire1") && !special && specialMeter <= specialCost1)
            Debug.Log("Not enough meter!");

        //activate special pattern
        if (special)
        {
            moveSpeed = 0.005f;
            if (!specialInterval[0])
            {
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(bullets[3], this.transform.position, Quaternion.Euler(0, 0, specialOffset));
                    specialOffset += 45;
                }
                specialInterval[0] = true;
            }
            else if (!specialInterval[1] && Time.time > specialTimeFinish - 3f)
            {
                specialOffset = 22.5f;
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(bullets[3], this.transform.position, Quaternion.Euler(0, 0, specialOffset));
                    specialOffset += 45;
                }
                specialInterval[1] = true;
            }

            if (Time.time > specialTimeFinish) 
            {
                special = false;
                specialOffset = 0f;
                Debug.Log("Special Finish!");
            }
        }
    }

    //function to handle the controls of the orbiters
	void controlOrbiter()
    {
        //depending on orbiter level, certain orbiters are active
		if (playerLevel == 1)
        {
			orbiters[0].SetActive(true);
			orbiters[1].SetActive(false);
			orbiters[2].SetActive(false);
			orbiters[3].SetActive(false);
		}
		else if (playerLevel == 2)
        {
			orbiters[0].SetActive(false);
			orbiters[1].SetActive(true);
			orbiters[2].SetActive(true);
			orbiters[3].SetActive(false);
		}
		else if (playerLevel == 3)
        {
			orbiters[0].SetActive(true);
			orbiters[1].SetActive(true);
			orbiters[2].SetActive(true);
			orbiters[3].SetActive(false);
		}
		else if (playerLevel == 4)
        {
			orbiters[0].SetActive(true);
			orbiters[1].SetActive(true);
			orbiters[2].SetActive(true);
			orbiters[3].SetActive(true);
		}

        //when holding Focus, if the orbiters are at the intended locations for rotation
        if (!destination)
        {
            if (playerLevel == 1)
            {
                if (orbiters[0].transform.position == orbiterWaypoints1[1].transform.position)
                    destination = true;
            }
            else if (playerLevel == 2)
            {
                if (orbiters[1].transform.position == orbiterWaypoints2[1].transform.position && orbiters[2].transform.position == orbiterWaypoints3[1].transform.position)
                    destination = true;
            }
            else if (playerLevel == 3)
            {
                if (orbiters[0].transform.position == orbiterWaypoints1[1].transform.position && orbiters[1].transform.position == orbiterWaypoints2[2].transform.position &&
                    orbiters[2].transform.position == orbiterWaypoints3[2].transform.position)
                    destination = true;
            }
            else if (playerLevel == 4)
            {
                if (orbiters[0].transform.position == orbiterWaypoints1[1].transform.position && orbiters[1].transform.position == orbiterWaypoints2[1].transform.position &&
                    orbiters[2].transform.position == orbiterWaypoints3[1].transform.position && orbiters[3].transform.position == orbiterWaypoints4[1].transform.position)
                    destination = true;
            }
            else
                destination = false;
        }

        //if holding focus, rotate the Orbiters
        if (shipFocus)
        {
			if (playerLevel == 1)
            {
                if (destination)
                    orbiters[0].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                else
                    orbiters[0].transform.position = Vector3.MoveTowards(orbiters[0].transform.position, orbiterWaypoints1[1].transform.position, destinationSpeed);
            }
            else if (playerLevel == 2)
            {
                if (destination)
                {
                    orbiters[1].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiters[2].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    orbiters[1].transform.position = Vector3.MoveTowards(orbiters[1].transform.position, orbiterWaypoints2[1].transform.position, destinationSpeed);
                    orbiters[2].transform.position = Vector3.MoveTowards(orbiters[2].transform.position, orbiterWaypoints3[1].transform.position, destinationSpeed);
                }
            }
            else if (playerLevel == 3)
            {
                if (destination)
                {
                    orbiters[0].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiters[1].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiters[2].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    orbiters[0].transform.position = Vector3.MoveTowards(orbiters[0].transform.position, orbiterWaypoints1[1].transform.position, destinationSpeed);
                    orbiters[1].transform.position = Vector3.MoveTowards(orbiters[1].transform.position, orbiterWaypoints2[2].transform.position, destinationSpeed);
                    orbiters[2].transform.position = Vector3.MoveTowards(orbiters[2].transform.position, orbiterWaypoints3[2].transform.position, destinationSpeed);
                }
            }
            else if (playerLevel == 4)
            {
                if (destination)
                {
                    orbiters[0].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiters[1].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiters[2].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                    orbiters[3].transform.RotateAround(this.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    orbiters[0].transform.position = Vector3.MoveTowards(orbiters[0].transform.position, orbiterWaypoints1[1].transform.position, destinationSpeed);
                    orbiters[1].transform.position = Vector3.MoveTowards(orbiters[1].transform.position, orbiterWaypoints2[1].transform.position, destinationSpeed);
                    orbiters[2].transform.position = Vector3.MoveTowards(orbiters[2].transform.position, orbiterWaypoints3[1].transform.position, destinationSpeed);
                    orbiters[3].transform.position = Vector3.MoveTowards(orbiters[3].transform.position, orbiterWaypoints4[1].transform.position, destinationSpeed);
                }
            }
		}
		else
        {
            if (playerLevel == 1)
            {
                orbiters[0].transform.position = Vector3.MoveTowards(orbiters[0].transform.position, orbiterWaypoints1[0].transform.position, destinationSpeed);
            }
            else if (playerLevel == 2)
            {
                orbiters[1].transform.position = Vector3.MoveTowards(orbiters[1].transform.position, orbiterWaypoints2[0].transform.position, destinationSpeed);
                orbiters[2].transform.position = Vector3.MoveTowards(orbiters[2].transform.position, orbiterWaypoints3[0].transform.position, destinationSpeed);
            }
            else if (playerLevel == 3)
            {
                orbiters[0].transform.position = Vector3.MoveTowards(orbiters[0].transform.position, orbiterWaypoints1[0].transform.position, destinationSpeed);
                orbiters[1].transform.position = Vector3.MoveTowards(orbiters[1].transform.position, orbiterWaypoints2[0].transform.position, destinationSpeed);
                orbiters[2].transform.position = Vector3.MoveTowards(orbiters[2].transform.position, orbiterWaypoints3[0].transform.position, destinationSpeed);
            }
            else if (playerLevel == 4)
            {
                orbiters[0].transform.position = Vector3.MoveTowards(orbiters[0].transform.position, orbiterWaypoints1[0].transform.position, destinationSpeed);
                orbiters[1].transform.position = Vector3.MoveTowards(orbiters[1].transform.position, orbiterWaypoints2[0].transform.position, destinationSpeed);
                orbiters[2].transform.position = Vector3.MoveTowards(orbiters[2].transform.position, orbiterWaypoints3[0].transform.position, destinationSpeed);
                orbiters[3].transform.position = Vector3.MoveTowards(orbiters[3].transform.position, orbiterWaypoints4[0].transform.position, destinationSpeed);
            }
            destination = false;
        }
	}
}
