using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
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

    void Start()
    {
        ability._clone_is_alive = false;
        Load();
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.Ready:
                if (Input.GetButton("Dash"))
                {
                    ability_cooldown_logic.last_ability = Time.time;
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

    private void Load()
    {
        GameData game_data = SaveSystem.Load();
        string path = game_data.ability_scriptableobject_path;
        ability = Resources.Load(path) as Ability;

        if (ability == null)
        {
            Debug.LogError("Failed to load weapon prefab from path: " + path);
        }

        ability.COOLDOWN = ability.DEFAULT_COOLDOWN;
        Debug.Log(ability.name);
    }
}