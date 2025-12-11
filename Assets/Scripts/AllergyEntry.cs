using TMPro;
using UnityEngine;

public class AllergyEntry : MonoBehaviour
{
    /// <summary>
    /// initiallizes an allergy object
    /// </summary>
    /// <param name="allergy"></param>
    public void Initialize(string allergy)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = allergy;
    }

}
