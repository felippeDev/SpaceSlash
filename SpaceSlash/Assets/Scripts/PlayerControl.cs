using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerBulletGO;
    public GameObject PlayerBulletPosition01;
    public GameObject PlayerBulletPosition02;
    public GameObject ExplosionGO;
    public VirtualJoystick moveJoystick;
    public bool isVulnerable;

    public Text LivesUIText;

    const int MaxLives = 3;
    int lives; // Current player lives

    public float speed;

    public void Init()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, -4f);

        // Working with SetActive instead destroy
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        isVulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Player shoot
        if (Input.GetKeyDown("space"))
        {
            Fire();
        }

        // Player movement by keyboard
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        // Player movement by mouse (finger gestures)
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Debug.Log("Clicked: X = " + Input.mousePosition.x.ToString() + " Y = " + Input.mousePosition.y.ToString());

        //    Vector3 InputDirection = (Input.mousePosition.magnitude > 1) ? Input.mousePosition.normalized : Input.mousePosition;

        //    direction = InputDirection;

        //    Debug.Log("Moving Direction: " + direction);
        //}

        // Player movement by joystick
        if (moveJoystick.InputDirection != Vector3.zero)
        {
            direction = moveJoystick.InputDirection;
        }

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.x = max.x - 0.285f;
        min.x = min.x + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    public void Fire()
    {
        // Play shoot sound
        gameObject.GetComponent<AudioSource>().Play();

        GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
        bullet01.transform.position = PlayerBulletPosition01.transform.position;

        GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
        bullet02.transform.position = PlayerBulletPosition02.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Destroy if colide against enemy or enemy bullets
        if (((collider.tag == "EnemyShipTag") || (collider.tag == "EnemyBulletTag")) && isVulnerable)
        {
            PlayExplosion();

            // Decrease lives
            lives--;

            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                gameObject.SetActive(false);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
