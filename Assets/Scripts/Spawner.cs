using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] asteroids;
    public float spawnrate;
    public float xOffSet;
    private float timer = 0;
    private System.Random rnd = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        float leftPoint = transform.position.x - xOffSet;
        float rightPoint = transform.position.x + xOffSet;

        Instantiate(asteroids[rnd.Next(0, asteroids.Length)], new Vector3(rnd.Next((int)leftPoint, (int)rightPoint), transform.position.y, 0), transform.rotation);
    }
}
