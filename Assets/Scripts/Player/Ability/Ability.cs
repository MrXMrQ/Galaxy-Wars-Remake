using UnityEngine;

public class Ability : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public float DEFAULT_COOLDOWN;
    [HideInInspector] public float COOLDOWN;
    [SerializeField] public float DURATION;

    public virtual void Activate(GameObject parent)
    {

    }

    protected void SpawnParticles(ParticleSystem particles, Vector2 pos)
    {
        Instantiate(particles, pos, Quaternion.identity);
    }
}
