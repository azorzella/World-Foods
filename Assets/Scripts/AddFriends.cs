using System.Collections.Generic;
using TMPro;
using UnityEngine;

// {
    public class AddFriends : MonoBehaviour
    {
        
        readonly List<Dish> filteredResults = new();
        public TMP_Dropdown friendDropdown;
        
        /// <summary>
        /// displays friend dropdown
        /// </summary>
        private void Start()
        {
            friendDropdown.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// filters friends based on text entry
        /// </summary>
        /// <param name="entry"></param>
        public void EntryFilter(string entry)
        {
            friendDropdown.gameObject.SetActive(false);

            filteredResults.Clear();

            if (entry.Length < 3)
            {
                return;
            }
        }

    }