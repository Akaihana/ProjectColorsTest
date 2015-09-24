using UnityEngine;
using System.Collections;

public class SpecialShot : BulletMove
{
    public float specialDelay = 3f;
    public float specialTime = 0f;
    public float rotationSpeed = 300f;

	// Use this for initialization
	void Start ()
    {
        specialTime = Time.time + specialDelay;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.shootSpecial();
	}

    void shootSpecial()
    {

        //Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.position);
        //Vector3 reference = GameObject.FindGameObjectWithTag("Player").transform.position;


        if (Time.time >= specialTime)
        {
            this.moveBullet();
        }
        else
        {
            //this.transform.RotateAround(GameObject.FindGameObjectWithTag("Player").transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
           
            //this.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            //this.transform.Translate(Vector3.up * 0.01f);
        }
    }
}
