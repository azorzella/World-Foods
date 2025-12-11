using TMPro;
using UnityEngine;

public class AllergyEntry : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Initialize(string allergy)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = allergy;
    }

}
