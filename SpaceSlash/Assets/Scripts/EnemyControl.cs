using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    GameObject scoreUITextGO;

    private float speed;
    public GameObject ExplosionGO;

    Vector2 minPos;
    Vector2 maxPos;

    // Use this for initialization
    void Awake()
    {
        minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        maxPos.y = (maxPos.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y);

        minPos.y = (minPos.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y);
    }

    // Use this for initialization
    void Start ()
    {
        ResetPosition();

        speed = 2.5f;

        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        if(transform.position.y < minPos.y)
        {
            ResetPosition();
        }
	}

    public void ResetPosition()
    {
        // Reset the enemy position to random
        transform.position = new Vector2(Random.Range(minPos.x, maxPos.x), maxPos.y);
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
