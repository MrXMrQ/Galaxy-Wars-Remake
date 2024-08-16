using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Ability/Clone")]
public class Clone : Ability
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject clone;

    public override void Activate(GameObject parent)
    {
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

        SpawnParticles(particles, rigidbody.position);
        Instantiate(clone, rigidbody.position - new Vector2(-2, 0), Quaternion.identity);
    }
}