using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Ability/Turret")]
public class Turret : Ability
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject turret;

    public override void Activate(GameObject parent)
    {
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

        SpawnParticles(particles, rigidbody.position);
        Instantiate(turret, rigidbody.position, Quaternion.identity);
    }
}