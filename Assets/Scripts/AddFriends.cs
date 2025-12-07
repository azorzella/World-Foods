using System.Collections.Generic;
using TMPro;
using UnityEngine;

    public class AddFriends : MonoBehaviour
    {
        readonly List<Dish> filteredResults = new();
        public TMP_Dropdown friendDropdown;
        
        private void Start()
        {
            friendDropdown.gameObject.SetActive(false);
        }

        public void EntryFilter(string entry)
        {
            friendDropdown.gameObject.SetActive(false);
        
            filteredResults.Clear();
        
            if (entry.Length < 3)
            {
                return;
            }
        
            List<string> filteredFriendNames = new();
        
            // foreach (var f in friends)
            // {
            //     if(f.GetName().ToLower().Contains(entry.ToLower()))
            //     {
            //         filteredResults.Add(f);
            //         filteredFriendNames.Add(f.GetName());
            //     }
            //
            // }
    }
}