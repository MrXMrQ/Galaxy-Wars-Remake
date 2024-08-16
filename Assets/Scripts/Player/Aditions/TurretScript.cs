using TMPro;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] GameObject player_default_projectile;
    [SerializeField] float max_life_time;
    [SerializeField] float min_life_time;
    [SerializeField] string tag_to_detect;
    [SerializeField] float detect_radius;
    float life_time;
    float _instance_time;


    void Awake()
    {
        life_time = Random.Range(max_life_time, min_life_time);
    }

    void Start()
    {
        _instance_time = Time.time;
    }

    void Update()
    {
        if (Time.time - _instance_time >= life_time)
        {
            Destroy(gameObject);
        }

        DetectAsteroids();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            Debug.Log(other.tag);

            AsteroidLogic asteroid_script = other.gameObject.GetComponent<AsteroidLogic>();

            if (asteroid_script != null)
            {
                PlayerMovement.Instance.score.score += asteroid_script.SCORE_VALUE;
            }
            else
            {
                Debug.LogWarning("NULL");
            }
        }
    }

    private void DetectAsteroids()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detect_radius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Asteroid"))
            {
                Debug.Log("Erkanntes Objekt: " + hitCollider.gameObject.name + " mit Tag: " + hitCollider.tag);
            }
        }
    }
}