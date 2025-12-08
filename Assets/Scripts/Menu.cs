using System.Collections.Generic;
using UnityEngine;

// TODO: Documentation by Chanel
public class Menu : MonoBehaviour {
    public List<GameObject> menus;
    int selectedIndex = 0;

    readonly List<MenuListener> listeners = new();

    public void RegisterListener(MenuListener listener) {
        listeners.Add(listener);
        listener.NotifyMenuStateChanged(selectedIndex);
    }

    void NotifyListeners() {
        foreach (var listener in listeners) {
            listener.NotifyMenuStateChanged(selectedIndex);
        }
    }
    
    public void ShowUi(int newIndex) {
        selectedIndex = newIndex;
        
        for (int i = 0; i < menus.Count; i++) {
            menus[i].SetActive(i == newIndex);
        }
        
        NotifyListeners();
    }
}