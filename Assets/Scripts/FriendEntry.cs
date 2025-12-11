using TMPro;
using UnityEngine;

public class FriendEntry : MonoBehaviour
{
    public void Initialize(UserData userData)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = userData.GetFirstName();
    }
}
