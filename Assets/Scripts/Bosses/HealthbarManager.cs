using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    public BossHealthbar bossHealthbar;
    private Logic bossLogic;

    // Update is called once per frame
    void Update()
    {
        if (bossLogic != null)
        {
            bossHealthbar.SetHealth(Logic.currentHealthpoints, bossLogic.bossName);
        }
    }

    public void Init(GameObject newBossLogic)
    {
        bossLogic = newBossLogic.GetComponentInChildren<Logic>();
        bossHealthbar.border.sprite = bossLogic.sprite;
        bossHealthbar.border.SetNativeSize();
        bossHealthbar.SetMaxHealth(bossLogic.maxHealthpoints, bossLogic.bossName);
        bossHealthbar.healthbar.SetActive(true);
    }
}