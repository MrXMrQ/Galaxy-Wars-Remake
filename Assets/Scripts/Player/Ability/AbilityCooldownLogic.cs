using UnityEngine;

public class AbilityCooldownLogic : MonoBehaviour
{
    [SerializeField] PlayerMovement player_movement;
    [SerializeField] Borders border;
    [SerializeField] CooldownSlotLogic[] cooldown_slots;
    [HideInInspector] public float last_ability, last_shot, last_teleport;

    void Start()
    {
        last_ability = -player_movement.ability_holder.ability.DEFAULT_COOLDOWN;
        last_shot = -player_movement.SHOT_COOLDOWN_DEFAULT_VALUE;
        last_teleport = -border.TELEPORT_COOLDOWN;
    }

    void Update()
    {
        Slot01();
        Slot02();
        Slot03();
    }

    private void Slot01()
    {
        if (Time.time - last_ability <= player_movement.ability_holder.ability.COOLDOWN)
        {
            cooldown_slots[0].SetSliderMaxValue(player_movement.ability_holder.ability.COOLDOWN);
            cooldown_slots[0].SetSliderValue(player_movement.ability_holder.ability.COOLDOWN - (Time.time - last_ability));
        }
        else
        {
            cooldown_slots[0].SetSliderValue(0);
        }
    }

    private void Slot02()
    {
        if (Time.time - last_shot <= player_movement.shot_cooldown)
        {
            cooldown_slots[1].SetSliderMaxValue(player_movement.shot_cooldown);
            cooldown_slots[1].SetSliderValue(player_movement.shot_cooldown - (Time.time - last_shot));
        }
        else
        {
            cooldown_slots[1].SetSliderValue(0);
        }
    }

    private void Slot03()
    {
        if (Time.time - last_teleport <= border.TELEPORT_COOLDOWN)
        {
            cooldown_slots[2].SetSliderMaxValue(border.TELEPORT_COOLDOWN);
            cooldown_slots[2].SetSliderValue(border.TELEPORT_COOLDOWN - (Time.time - last_teleport));
        }
        else
        {
            cooldown_slots[2].SetSliderValue(0);
        }
    }
}