using System.Collections;
using UnityEngine;

public class FabricatorLogic : MonoBehaviour
{
    public static FabricatorLogic Instance { get; private set; }

    [Header("PREFABS")]
    [SerializeField] GameObject[] asteroid_prefabs;
    [SerializeField] GameObject[] boss_prefabs;

    [Header("VALUES")]
    [SerializeField] public float spawn_rate;
    [SerializeField] int score_to_spawn_boss;
    [SerializeField] int increse_score_to_spawn_boss;
    [SerializeField] BossHealthbarManager boss_healthbar;
    [HideInInspector] public bool is_boss_alive;
    bool _can_spawn_asteroid;
    int _last_boss_index;

    [Header("CAMERA BOUNDS")]
    Camera main_camera;
    Vector3 _LEFT_POINT;
    Vector3 _RIGHT_POINT;

    void Start()
    {
        main_camera = Camera.main;
        CalculateCameraBounds();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        SpawnAsteroid();
    }
    void Update()
    {
        if (is_boss_alive)
        {
            return;
        }

        if (_can_spawn_asteroid)
        {
            StartCoroutine(Timer());
        }

        if (score_to_spawn_boss <= PlayerMovement.Instance.score.score)
        {
            StartCoroutine(SpawnBoss());
        }
    }

    private void CalculateCameraBounds()
    {
        float distance = main_camera.nearClipPlane;

        _LEFT_POINT = main_camera.ViewportToWorldPoint(new Vector3(0, 1, distance));
        _RIGHT_POINT = main_camera.ViewportToWorldPoint(new Vector3(1, 0, distance));
    }

    private IEnumerator Timer()
    {
        _can_spawn_asteroid = false;
        yield return new WaitForSeconds(spawn_rate);
        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        float leftPoint = _LEFT_POINT.x;
        float rightPoint = _RIGHT_POINT.x;

        int index = Random.Range(0, asteroid_prefabs.Length);
        float randomX = Random.Range(leftPoint, rightPoint);

        Vector2 position = new Vector2(randomX, transform.position.y);
        Instantiate(asteroid_prefabs[index], position, transform.rotation);
        _can_spawn_asteroid = true;
    }

    private IEnumerator SpawnBoss()
    {
        is_boss_alive = true;
        score_to_spawn_boss += increse_score_to_spawn_boss;

        int index;

        do
        {
            index = Random.Range(0, boss_prefabs.Length);
        } while (index == _last_boss_index);

        _last_boss_index = index;

        GameObject boss = boss_prefabs[index];
        BossLogic logic = boss.GetComponent<BossLogic>();

        if (logic == null)
        {
            Debug.LogError("BossLogic component missing on the boss prefab.");
            yield break;
        }

        logic.SpawnParticles();

        ParticleSystem.MainModule mainModule = logic.spawn_particles.main;

        yield return new WaitForSeconds(mainModule.duration);

        boss_healthbar.Init(boss);
        Instantiate(boss, logic.spawn_point, transform.rotation);
    }
}
