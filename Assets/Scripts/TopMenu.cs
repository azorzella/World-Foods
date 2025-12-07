using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public GameObject addDishMenu;
    
    public void showUI()
    {
        addDishMenu.SetActive(true);
    }
    
    public void hideUI()
    {
        addDishMenu.SetActive(false);
    }
}
