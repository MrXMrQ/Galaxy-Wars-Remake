using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Ability/Clone")]
public class Clone : Ability
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject clone;
    [SerializeField] float clone_offset;
    [SerializeField] float duration;
    [HideInInspector] public CloneController clone_controller;

    public override void Activate(GameObject parent)
    {
        PlayerMovement player = parent.GetComponent<PlayerMovement>();
        if (!_clone_is_alive)
        {

            _clone_is_alive = true;

            Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();

            SpawnParticles(particles, rigidbody.position);

            GameObject cloneInstance = Instantiate(clone, rigidbody.position, Quaternion.identity);
            clone_controller = cloneInstance.GetComponent<CloneController>();
            player.border.clone = cloneInstance.GetComponent<Rigidbody2D>();
            player.StartCoroutine(SmoothMove(rigidbody.transform, cloneInstance.transform));
        }
        else
        {
            player.ability_cooldown_logic.last_ability = -player.ability_holder.ability.DEFAULT_COOLDOWN;
        }
    }

    private IEnumerator SmoothMove(Transform playerTransform, Transform cloneTransform)
    {
        Vector2 originalPlayerPosition = playerTransform.position;
        Vector2 targetPlayerPosition = originalPlayerPosition + Vector2.left * clone_offset;

        Vector2 originalClonePosition = cloneTransform.position;
        Vector2 targetClonePosition = originalClonePosition + Vector2.right * clone_offset;

        float transitionProgress = 0f;

        while (transitionProgress < 1f)
        {
            //* Increment progress over time
            transitionProgress += Time.deltaTime / duration;

            //* Smoothly move the player and clone to their target positions
            playerTransform.position = Vector2.Lerp(originalPlayerPosition, targetPlayerPosition, transitionProgress);

            if (cloneTransform != null)
            {
                cloneTransform.position = Vector2.Lerp(originalClonePosition, targetClonePosition, transitionProgress);
            }

            yield return null;
        }

        //* Ensure final positions are set after the transition
        playerTransform.position = targetPlayerPosition;
        if (cloneTransform != null)
        {
            cloneTransform.position = targetClonePosition;
            clone_controller._max_distance = (playerTransform.position - cloneTransform.position).magnitude;
        }
    }
}