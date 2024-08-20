using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponCard[] weaponCardNew;
    [SerializeField] Outline[] outlines;
    void Awake()
    {
        for (int i = 0; i < weaponCardNew.Length; i++)
        {
            weaponCardNew[i].outline = outlines[i];
            weaponCardNew[i].outline.enabled = false;
        }
    }
}
