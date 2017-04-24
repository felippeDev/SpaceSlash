using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnerGO : MonoBehaviour {

    public GameObject StarGO;
    public int MaxStars;

    Color[] starColors =
    {
        new Color(250, 250, 255, 0.8f),
        new Color(223, 196, 234, 0.5f),
        new Color(255, 255, 255, 0.2f),
        new Color(255, 255, 255, 0.8f)
    };

    // Use this for initialization
    void Start () {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < MaxStars; i++)
        {
            GameObject star = (GameObject)Instantiate(StarGO);

            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
