using UnityEngine;
using System.Collections;

public class LaserMove1 : BulletMove
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
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("PF1").transform.position.x, transform.position.y, transform.position.z);
    }
}
