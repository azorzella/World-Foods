using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    public List<GameObject> menus;
    int selectedIndex = 0;

    readonly List<MenuListener> listeners = new();
    
    /// <summary>
    /// registers a listener to keep track of menu presses
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(MenuListener listener) {
        listeners.Add(listener);
        listener.NotifyMenuStateChanged(selectedIndex);
    }
    
    /// <summary>
    /// notifies listeners about menu changes
    /// </summary>
    void NotifyListeners() {
        foreach (var listener in listeners) {
            listener.NotifyMenuStateChanged(selectedIndex);
        }
    }
    
    /// <summary>
    /// toggles menu on based on what menu button is pressed
    /// </summary>
    /// <param name="newIndex"></param>
    public void ShowUi(int newIndex) {
        selectedIndex = newIndex;
        
        for (int i = 0; i < menus.Count; i++) {
            menus[i].SetActive(i == newIndex);
        }
        
        NotifyListeners();
    }
}