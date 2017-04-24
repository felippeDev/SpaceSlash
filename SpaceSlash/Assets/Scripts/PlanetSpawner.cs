using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour {

    public GameObject[] Planets;
    Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
        // Add planets to queue
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MovePlanetDown()
    {
        EnqueuePlanets();

        if(availablePlanets.Count == 0)
        {
            return;
        }

        GameObject planet = availablePlanets.Dequeue();

        planet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach (GameObject planet in Planets)
        {
            if ((planet.transform.position.y < 0) && (!planet.GetComponent<Planet>().isMoving))
            {
                // If planet is down and is not moving
                planet.GetComponent<Planet>().ResetPosition();

                availablePlanets.Enqueue(planet);
            }
        }
    }
}
