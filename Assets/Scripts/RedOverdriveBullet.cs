using UnityEngine;
using System.Collections;

public class RedOverdriveBullet : BulletMove
{
    private float veerTime;
    public float veerDelay;
    public float rotation;

	// Use this for initialization
	void Start ()
    {
        veerTime = Time.time + veerDelay;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Time.time < veerTime)
        {
            transform.Rotate(Vector3.forward * rotation);
        }
        this.moveBullet();
	}
}
