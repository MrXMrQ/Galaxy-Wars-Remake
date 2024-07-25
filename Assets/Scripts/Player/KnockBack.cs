using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockBackTime;
    public float hitDirectionForce;
    public float constForce;
    public float inputForce;
    public bool isBeingKnockBack { get; private set; }
    public Rigidbody2D player;
    private Coroutine coroutine;

    private IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, Vector2 inputDirection)
    {
        isBeingKnockBack = true;

        Vector2 hitForce = hitDirection * hitDirectionForce;
        Vector2 constantForce = constantForceDirection * constForce;
        Vector2 combinedForce;

        float elapsedTime = 0f;
        while (elapsedTime < knockBackTime)
        {
            elapsedTime += Time.fixedDeltaTime;

            Vector2 knockBackForce = hitForce + constantForce;

            if (inputDirection != Vector2.zero)
            {
                combinedForce = knockBackForce + inputDirection * inputForce;
            }
            else
            {
                combinedForce = knockBackForce;
            }
            player.velocity = combinedForce;

            yield return new WaitForFixedUpdate();
        }

        isBeingKnockBack = false;
    }

    public void CallKnockBack(Vector2 hitDirection, Vector2 constantForceDirection, Vector2 inputDirection)
    {
        coroutine = StartCoroutine(KnockBackAction(hitDirection, constantForceDirection, inputDirection));
    }
}