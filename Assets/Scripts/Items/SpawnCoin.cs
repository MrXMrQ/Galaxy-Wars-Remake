using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    [SerializeField] ParticleSystem COLLECT_PARTICLES;
    [SerializeField] int SCORE_VALUE;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(COLLECT_PARTICLES, transform.position, Quaternion.identity);
            PlayerMovement.Instance.score.UpdateScorePoints(SCORE_VALUE);
            Destroy(gameObject);
        }
    }
}
