using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public GameObject EnemyBulletGO;

	// Use this for initialization
	void Start () {
        Invoke("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireEnemyBullet()
    {
        // Play shoot sound
        gameObject.GetComponent<AudioSource>().Play();

        GameObject playerShip = GameObject.Find("PlayerGO");

        // If player still alive
        if(playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // Set the bullet direction to shoot on player
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
