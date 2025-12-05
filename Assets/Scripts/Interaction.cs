using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Vector2 = UnityEngine.Vector2;

public class Interaction : MonoBehaviour {
    Camera camera;

    WorldMapVisualization vis;
    
    void Start() {
        camera = Camera.main;
        vis = FindFirstObjectByType<WorldMapVisualization>();
    }

    public void OnClick(InputAction.CallbackContext context) {
        if (!DishView.i.IsVisible()) {
            Vector2 position = context.ReadValue<Vector2>();
            
            Ray ray = camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out var hit)) {
                string isoCode = hit.transform.name;
                DishView.i.OpenDisplaying(vis.GetLoggedDishesFromCountry(isoCode));
            }
        }
    }   

    public void OnDrag(InputAction.CallbackContext context) {
        if (!DishView.i.IsVisible()) {
            Debug.Log(context.ReadValue<Vector2>());
        }        
    }
}