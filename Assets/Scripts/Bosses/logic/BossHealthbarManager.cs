using UnityEngine;

public class BossHealthbarManager : MonoBehaviour
{
    [SerializeField] Healthbar healthbar;
    BossLogic _boss_logic;

    void Update()
    {
        if (_boss_logic != null)
        {
            healthbar.SetHealth(_boss_logic.current_healthpoints);
        }
    }

    public void Init(GameObject newBossLogic)
    {
        healthbar.SetActive(true);
        _boss_logic = newBossLogic.GetComponentInChildren<BossLogic>();
        healthbar.border.sprite = _boss_logic.sprite;
        healthbar.border.SetNativeSize();
        healthbar.SetMaxHealth(_boss_logic.MAX_HEALTHPOINTS);
    }
}