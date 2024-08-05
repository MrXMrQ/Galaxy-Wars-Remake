using UnityEngine;

public class ProjectileBombLogic : MonoBehaviour
{
    [SerializeField] float life_time;
    [SerializeField] GameObject default_projectile;
    bool _is_detonated;
    float _instance_time;

    void Start()
    {
        _instance_time = Time.time;
    }


    void Update()
    {
        if (Time.time - _instance_time >= life_time && !_is_detonated)
        {
            _is_detonated = true;
            Detonate();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        Detonate();
    }

    private void Detonate()
    {
        Destroy(gameObject);
        ProjctileDefaultLogic projctile_default_logic_script = default_projectile.GetComponent<ProjctileDefaultLogic>();

        if (projctile_default_logic_script != null)
        {
            Vector2 positon = new Vector2(transform.position.x, transform.position.y);
            MakeInstance(projctile_default_logic_script, Vector2.up, positon);
            MakeInstance(projctile_default_logic_script, Vector2.down, positon);
            MakeInstance(projctile_default_logic_script, Vector2.right, positon);
            MakeInstance(projctile_default_logic_script, Vector2.left, positon);
        }
    }

    private void MakeInstance(ProjctileDefaultLogic projctile_default_logic_script, Vector2 move_direction, Vector2 position)
    {
        projctile_default_logic_script.SetMoveDirection(move_direction);
        Instantiate(default_projectile, position, Quaternion.identity);
    }
}