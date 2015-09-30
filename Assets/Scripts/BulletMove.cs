using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour
{
	
	public float maxX = 8f;
	public float maxY = 10f;

	public float bulletSpeed;
	
	// Update is called once per frame
	void Update ()
    {
		this.moveBullet();
	}

	public void moveBullet()
    {
	
		transform.Translate(Vector3.up * bulletSpeed);

		if(transform.position.y >= maxY || transform.position.y <= -maxY || transform.position.x >= maxX || transform.position.x <= -maxX)
			Destroy(this.gameObject);

	}
}
