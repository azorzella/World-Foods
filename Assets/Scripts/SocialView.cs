using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SocialView : MonoBehaviour, MenuListener
{
    public TextMeshProUGUI greeting;

    /// <summary>
    /// Initializes welcome text, friend and allergy lists
    /// </summary>
    /// <param name="userData"></param>
    public void Initialize(UserData userData)
    {
        FindFirstObjectByType<Menu>().RegisterListener(this);

        verticalLayoutGroup = parentEntriesTo.GetComponent<VerticalLayoutGroup>();

        PopulateEntries(userData);
        PopulateAllergies(userData);

        greeting.text = $"Hello {userData.GetFullName()}!";

        currentUser = userData;

    }

    private UserData currentUser;

    /// <summary>
    /// Adds an allergy to user's allergies
    /// </summary>
    public void AddAllergy()
    {
        if(inputField.text != string.Empty)
        {
            currentUser.AddAllergy(inputField.text);
            inputField.text = string.Empty;
            PopulateAllergies(currentUser);
        }
    }

    public RectTransform parentEntriesTo;
    VerticalLayoutGroup verticalLayoutGroup;
    readonly List<GameObject> entries = new();
    readonly List<GameObject> allergyEntries = new();

    /// <summary>
    /// Populates user's friend entries.
    /// </summary>
    /// <param name="userData"></param>
    public void PopulateEntries(UserData userData)
    {
        ClearAllergyEntries();

        GameObject friendEntry = ResourceLoader.LoadObject("FriendEntry");

        GameObject newEntry = null;

        foreach (var f in userData.GetFriends())
        {
            newEntry = Instantiate(friendEntry, parentEntriesTo);
            newEntry.GetComponent<FriendEntry>().Initialize(f);
            allergyEntries.Add(newEntry);
        }

        int friendCount = entries.Count;

        if (friendCount > 0)
        {
            Vector2 newSize = new Vector2(
                parentEntriesTo.sizeDelta.x,
                newEntry.GetComponent<RectTransform>().sizeDelta.y * friendCount +
                verticalLayoutGroup.spacing * friendCount);
            parentEntriesTo.sizeDelta = newSize;

        }


    }

    /// <summary>
    /// Clears friend list entries
    /// </summary>
    void ClearEntries()
    {
        while (entries.Count > 0)
        {
            Destroy(entries[0]);
            entries.RemoveAt(0);
        }
    }
    
    /// <summary>
    /// Clears allergy list entries
    /// </summary>
    void ClearAllergyEntries()
    {
        while (allergyEntries.Count > 0)
        {
            Destroy(allergyEntries[0]);
            allergyEntries.RemoveAt(0);
        }
    }

    public RectTransform allergyParentEntriesTo;
    VerticalLayoutGroup allergyVerticalLayoutGroup;
    readonly List<string> allergies = new();
    
    /// <summary>
    /// Populates user's allergy list
    /// </summary>
    /// <param name="userData"></param>
    public void PopulateAllergies(UserData userData)
    {
        ClearEntries();

        GameObject AllergyEntry = ResourceLoader.LoadObject("AllergyEntry");

        GameObject newEntry = null;

        foreach (var a in userData.GetAllergies())
        {
            newEntry = Instantiate(AllergyEntry, allergyParentEntriesTo);
            newEntry.GetComponent<AllergyEntry>().Initialize(a);
            entries.Add(newEntry);
        }

        int allergyCount = entries.Count;
        int numAllergyEntries = allergyCount + 1;
        
        if (allergyCount > 0)
        {
            Vector2 newSize = new Vector2(
                allergyParentEntriesTo.sizeDelta.x,
                (newEntry.GetComponent<RectTransform>().sizeDelta.y +
                verticalLayoutGroup.spacing) * numAllergyEntries);
            allergyParentEntriesTo.sizeDelta = newSize;
        }
    }

    /// <summary>
    /// sets the social view tab to active
    /// </summary>
    /// <param name="currentIndex"></param>
    public void NotifyMenuStateChanged(int currentIndex)
    {
        gameObject.SetActive(currentIndex == 2);
    }

    public TMP_InputField inputField;
    
    /// <summary>
    /// toggles input field off and on
    /// </summary>
    public void toggleInputField()
    {
        inputField.gameObject.SetActive(!inputField.gameObject.activeSelf);
    }
}