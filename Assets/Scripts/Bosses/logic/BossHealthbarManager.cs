using TMPro;
using UnityEngine;

public class BossHealthbarManager : MonoBehaviour
{
    [SerializeField] Healthbar healthbar;
    [SerializeField] TextMeshProUGUI boss_name_text;
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
        boss_name_text.text = _boss_logic.boss_name;
        healthbar.border.sprite = _boss_logic.sprite;
        healthbar.border.SetNativeSize();
        healthbar.SetMaxHealth(_boss_logic.MAX_HEALTHPOINTS);
    }
}