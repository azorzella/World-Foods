using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Interaction : MonoBehaviour {
    Camera camera;

    WorldMapVisualization vis;
    
    void Start() {
        camera = Camera.main;
        vis = FindFirstObjectByType<WorldMapVisualization>();
    }

    public void OnClick(InputAction.CallbackContext context) {
        Debug.Log(context.ReadValue<Vector2>());
        
        if (!DishView.i.IsVisible()) { // && Mouse.current.leftButton.wasPressedThisFrame) {
            // Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Ray ray = camera.ScreenPointToRay(context.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out var hit)) {
                string isoCode = hit.transform.name;
                Debug.Log(isoCode);
                
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