using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] public Ability ability;
    [SerializeField] AbilityCooldownLogic ability_cooldown_logic;
    float cooldown;
    float duration;

    enum AbilityState
    {
        Ready,
        Duration,
        Cooldown
    }

    AbilityState state = AbilityState.Ready;

    void Update()
    {
        switch (state)
        {
            case AbilityState.Ready:
                if (Input.GetButton("Dash"))
                {
                    ability_cooldown_logic.last_dash = Time.time;
                    ability.Activate(gameObject);
                    state = AbilityState.Duration;
                    duration = ability.DURATION;
                }
                break;
            case AbilityState.Duration:
                if (duration > 0)
                {
                    duration -= Time.deltaTime;
                }
                else
                {
                    //* start cooldown
                    state = AbilityState.Cooldown;
                    cooldown = ability.COOLDOWN;
                }
                break;
            case AbilityState.Cooldown:
                //* check cooldwon
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.Ready;
                }
                break;
        }
    }
}