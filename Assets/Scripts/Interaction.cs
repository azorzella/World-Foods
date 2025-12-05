using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class Interaction : MonoBehaviour {
    Camera camera;

    WorldMapVisualization vis;

    public Transform cameraTarget;
    
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

    const float clampX = 10;
    
    public void OnDrag(InputAction.CallbackContext context) {
        if (!DishView.i.IsVisible()) {
            Vector2 currentPosition = cameraTarget.position;

            currentPosition += context.ReadValue<Vector2>();
            cameraTarget.position = new Vector3(Mathf.Clamp(currentPosition.x, -clampX, clampX), 0, -10);
        }        
    }
}