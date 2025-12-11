using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialView : MonoBehaviour, MenuListener
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(UserData userData)
    {
        FindFirstObjectByType<Menu>().RegisterListener(this);
        
        verticalLayoutGroup = parentEntriesTo.GetComponent<VerticalLayoutGroup>();
        
        PopulateEntries(userData);
    }
    
    public RectTransform parentEntriesTo;
    VerticalLayoutGroup verticalLayoutGroup;
    readonly List<GameObject> entries = new();

    public void PopulateEntries(UserData userData)
    {
        ClearEntries();

        GameObject friendEntry = ResourceLoader.LoadObject("FriendEntry");

        GameObject newEntry = null;
        
        foreach (var f in userData.GetFriends()) {
            newEntry = Instantiate(friendEntry, parentEntriesTo);
            newEntry.GetComponent<FriendEntry>().Initialize(f);
            entries.Add(newEntry);
        }

        int friendCount = entries.Count;

        if (friendCount > 0) {
            Vector2 newSize = new Vector2(
                parentEntriesTo.sizeDelta.x,
                newEntry.GetComponent<RectTransform>().sizeDelta.y * friendCount + verticalLayoutGroup.spacing * friendCount);
            parentEntriesTo.sizeDelta = newSize;
        }
    }
    void ClearEntries() {
        while (entries.Count > 0) {
            Destroy(entries[0]);
            entries.RemoveAt(0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyMenuStateChanged(int currentIndex)
    {
        gameObject.SetActive(currentIndex == 2);
    }
}
