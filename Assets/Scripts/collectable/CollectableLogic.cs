using UnityEngine;

public class CollectableLogic : MonoBehaviour
{
    [SerializeField] float MOVEMENT_SPEED = 2f;
    float _dead_zone;
    Camera _main_camera;

    void Start()
    {
        _main_camera = Camera.main;
        CalculateCameraBounds();

    }

    void Update()
    {
        transform.Translate(Vector2.down * MOVEMENT_SPEED * Time.deltaTime);

        if (transform.position.y < _dead_zone)
        {
            Destroy(gameObject);
        }
    }

    private void CalculateCameraBounds()
    {
        Vector3 bottom_right = _main_camera.ViewportToWorldPoint(new Vector3(-1, -1, _main_camera.nearClipPlane));
        _dead_zone = bottom_right.y;
    }
}