using UnityEngine;
using System.Collections;

public class LaserMove : MonoBehaviour
{
    public float maxX = 8f;
    public float maxY = 10f;

    public float bulletSpeed;
    //public GameObject ReferencePoint;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.moveBullet();
	}

    public void moveBullet()
    {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, transform.position.y, transform.position.z);
        transform.Translate(Vector3.up * bulletSpeed);

        if (transform.position.y >= maxY || transform.position.y <= -maxY || transform.position.x >= maxX || transform.position.x <= -maxX)
            Destroy(this.gameObject);

    }
}
