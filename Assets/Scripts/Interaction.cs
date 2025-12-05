using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour {
    Camera camera;

    WorldMapVisualization vis;
    
    void Start() {
        camera = Camera.main;
        vis = FindFirstObjectByType<WorldMapVisualization>();
    }

    void Update() {
        if (!DishView.i.IsVisible() && Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit)) {
                string isoCode = hit.transform.name;
                Debug.Log(isoCode);
                
                DishView.i.OpenDisplaying(vis.GetLoggedDishesFromCountry(isoCode));
            }
        }
    }
}