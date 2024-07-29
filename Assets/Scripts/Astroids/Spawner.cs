using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }
    public GameObject[] asteroids;
    public GameObject[] bosses;
    public ParticleSystem[] particleSystems;
    public float spawnrate;
    public int scoreToSpawnBoss;
    public bool isBossAlive;
    public float xOffSet;
    public HealthbarManager healthbarManager;
    private float timer = 0;
    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreToSpawnBoss <= PlayerController.currentScore)
        {
            scoreToSpawnBoss += 50;
            isBossAlive = true;
            StartCoroutine(SpawnBoss());
        }

        if (isBossAlive)
        {
            return;
        }

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

        int index = rnd.Next(0, asteroids.Length);
        Vector3 position = new Vector3(rnd.Next((int)leftPoint, (int)rightPoint), transform.position.y, 0);

        Instantiate(asteroids[index], position, transform.rotation);
    }

    private IEnumerator SpawnBoss()
    {
        int index = rnd.Next(0, bosses.Length);
        GameObject boss = bosses[index];
        Logic logic = boss.GetComponent<Logic>();

        Instantiate(particleSystems[index], logic.spawnPoint, Quaternion.identity);

        yield return new WaitForSeconds(particleSystems[index].main.duration);

        healthbarManager.Init(boss);
        Instantiate(boss, logic.spawnPoint, transform.rotation);
    }
}
