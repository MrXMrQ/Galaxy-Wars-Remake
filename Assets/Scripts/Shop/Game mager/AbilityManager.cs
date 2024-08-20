using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] AbilityCard[] abilities;
    [SerializeField] Outline[] outlines;
    void Awake()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].outline = outlines[i];
            abilities[i].outline.enabled = false;
        }
    }
}
