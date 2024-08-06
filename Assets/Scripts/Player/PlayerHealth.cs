using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("HEALTH")]
    [SerializeField] public Healthbar healthbar;
    [SerializeField] int MAXHEALTHPOINTS;
    int _current_healthpoints;

    [HideInInspector]
    public int current_healthpoints
    {
        get
        {
            return _current_healthpoints;
        }
        set
        {
            if (!ItemLogic.is_immortal)
            {
                _current_healthpoints = value;
                healthbar.SetHealth(current_healthpoints);
            }

            if (current_healthpoints <= 0)
            {
                PlayerMovement.Instance.score.OnDeath();
                SceneManager.LoadScene(5);
            }
        }
    }

    void Start()
    {
        Load();
        current_healthpoints = MAXHEALTHPOINTS;
        healthbar.SetMaxHealth(MAXHEALTHPOINTS);
    }

    private void Load()
    {
        GameData gameData = SaveSystem.Load();

        MAXHEALTHPOINTS = gameData.max_healthpoints;
    }
}