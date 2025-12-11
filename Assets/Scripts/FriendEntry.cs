using TMPro;
using UnityEngine;

public class FriendEntry : MonoBehaviour
{
    /// <summary>
    /// sets text component to user's name
    /// </summary>
    /// <param name="userData"></param>
    public void Initialize(UserData userData)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = userData.GetFullName();
    }
}
