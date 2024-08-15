using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] float KNOCKBACKTIME;
    [SerializeField] float HITDIRECTIONFORCE;
    [SerializeField] float CONSTFORCE;
    [SerializeField] float INPUTFORCE;
    Coroutine _coroutine;
    bool _is_being_knoc_backed { get; set; }

    [HideInInspector]
    public bool is_being_knock_backed
    {
        get
        {
            return _is_being_knoc_backed;
        }
    }

    void Start()
    {

    }

    private IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, Vector2 inputDirection)
    {
        _is_being_knoc_backed = true;

        Vector2 hitForce = hitDirection * HITDIRECTIONFORCE;
        Vector2 constantForce = constantForceDirection * CONSTFORCE;
        Vector2 combinedForce;

        float elapsedTime = 0f;
        while (elapsedTime < KNOCKBACKTIME)
        {
            elapsedTime += Time.fixedDeltaTime;

            Vector2 knockBackForce = hitForce + constantForce;

            if (inputDirection != Vector2.zero)
            {
                combinedForce = knockBackForce + inputDirection * INPUTFORCE;
            }
            else
            {
                combinedForce = knockBackForce;
            }
            player.velocity = combinedForce;

            yield return new WaitForFixedUpdate();
        }

        _is_being_knoc_backed = false;
    }

    public void CallKnockBack(Vector2 hitDirection, Vector2 constantForceDirection, Vector2 inputDirection)
    {
        _coroutine = StartCoroutine(KnockBackAction(hitDirection, constantForceDirection, inputDirection));
    }
}