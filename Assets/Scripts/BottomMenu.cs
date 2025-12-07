using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BottomMenu : MonoBehaviour
{
    public List<GameObject> menus;
    
    public void showUI(int index)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].SetActive(i == index);
        }
    }
}
