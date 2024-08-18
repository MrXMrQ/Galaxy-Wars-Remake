using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] ParticleSystem COLLECT_PARTICLES;
    [SerializeField] public float ITEM_DURATION;
    [SerializeField] int index;
    Sprite _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            Instantiate(COLLECT_PARTICLES, transform.position, Quaternion.identity);
            ItemLogic.Instance.UpdateSprite(_sprite, index);
            ItemLogic.Instance.collected_items[index] = true;
            Destroy(gameObject);
        }
    }
}
