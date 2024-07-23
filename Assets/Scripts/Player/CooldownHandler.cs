using System.Threading;
using UnityEngine;

public class CooldownHandler : MonoBehaviour
{
    public PlayerController playerController;
    public Borders borders;
    public Slot[] slots;
    public float lastDash;
    public float lastShot;
    public float lastTeleport;

    // Start is called before the first frame update
    void Start()
    {
        lastDash = -playerController.dashCooldownDefaultValue;
        lastShot = -playerController.shootingCooldownDefaultValue;
        lastTeleport = -borders.teleportCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastDash <= playerController.dashCooldown)
        {
            slots[0].SetSliderMax(playerController.dashCooldown);
            slots[0].SetSliderValue(playerController.dashCooldown - (Time.time - lastDash));
        }
        else
        {
            slots[0].SetSliderValue(0);
        }

        if (Time.time - lastShot <= playerController.shootingCooldown)
        {
            slots[1].SetSliderMax(playerController.shootingCooldown);
            slots[1].SetSliderValue(playerController.shootingCooldown - (Time.time - lastShot));
        }
        else
        {
            slots[1].SetSliderValue(0);
        }

        if (Time.time - lastTeleport <= borders.teleportCooldown)
        {
            slots[2].SetSliderMax(borders.teleportCooldown);
            slots[2].SetSliderValue(borders.teleportCooldown - (Time.time - lastTeleport));
        }
        else
        {
            slots[2].SetSliderValue(0);
        }
    }
}
