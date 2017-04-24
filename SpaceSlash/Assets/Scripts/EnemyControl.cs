using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    GameObject scoreUITextGO;

    private float speed;
    public GameObject ExplosionGO;

	// Use this for initialization
	void Start ()
    {
        speed = 2.5f;

        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Destroy if colide against player or player bullets
        if ((collider.tag == "PlayerShipTag") || (collider.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            // Add 50points to score
            scoreUITextGO.GetComponent<GameScore>().Score += 50;

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
