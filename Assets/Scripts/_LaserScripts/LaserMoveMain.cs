using UnityEngine;
using System.Collections;

public class LaserMoveMain : BulletMove
{

    //public GameObject ReferencePoint;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.handleLaser();
        this.moveBullet();
	}

    void handleLaser()
    {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, transform.position.y, transform.position.z);
    }
}
