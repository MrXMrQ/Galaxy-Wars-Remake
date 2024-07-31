using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("HEALTH")]
    [SerializeField] public Healthbar healthbar;
    [SerializeField] int MAXHEALTHPOINTS;
    int _currentHealthpoints;

    [HideInInspector]
    public int currentHealthpoints
    {
        get
        {
            return _currentHealthpoints;
        }
        set
        {
            if (!ItemHandler.isImmortal)
            {
                _currentHealthpoints = value;
                healthbar.SetHealth(currentHealthpoints);
            }

            if (currentHealthpoints <= 0)
            {
                PlayerMovement.Instance.score.OnDeath();
                SceneManager.LoadScene(5);
            }
        }
    }

    void Start()
    {
        Load();
        currentHealthpoints = MAXHEALTHPOINTS;
        healthbar.SetHealth(currentHealthpoints);
    }

    private void Load()
    {
        GameData gameData = SaveSystem.Load();

        MAXHEALTHPOINTS = gameData.maxHealthpoints;
    }
}