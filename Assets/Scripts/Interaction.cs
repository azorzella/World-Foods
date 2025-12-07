using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour, MenuListener {
    Camera camera;
    Transform cameraTransform;
    
    WorldMapVisualization vis;

    Vector2 positionLastPressed;

    UserInput userInput;

    bool active = false;
    
    void Start() {
        userInput = new();
        userInput.Enable();

        userInput.User.Drag.performed += OnDrag;
        userInput.User.Press.started += OnPress;
        userInput.User.Press.canceled += OnRelease;
        
        camera = Camera.main;
        cameraTransform = camera.gameObject.transform;
        vis = FindFirstObjectByType<WorldMapVisualization>();
        
        FindFirstObjectByType<Menu>().RegisterListener(this);
    }
    
    void OnPress(InputAction.CallbackContext context) {
        positionLastPressed = Pointer.current.position.ReadValue();
    }

    void OnRelease(InputAction.CallbackContext context) {
        Vector2 positionLastReleased = Pointer.current.position.ReadValue();
        
        Vector2 difference = positionLastPressed - positionLastReleased;
        float magnitude = difference.magnitude;
        
        if (active && DishView.i.IsVisible() && magnitude < 0.02F) {
            Ray ray = camera.ScreenPointToRay(positionLastReleased);

            if (Physics.Raycast(ray, out var hit)) {
                string isoCode = hit.transform.name;
                DishView.i.OpenDisplaying(vis.GetLoggedDishesFromCountry(isoCode));
            }
        }
    }

    const float clampX = 10;
    const float sensitivity = 0.0045F;
    
    void OnDrag(InputAction.CallbackContext context) {
        if (active && !DishView.i.IsVisible()) {
            Vector2 currentPosition = cameraTransform.position;

            currentPosition -= context.ReadValue<Vector2>() * sensitivity;
            cameraTransform.position = new Vector3(Mathf.Clamp(currentPosition.x, -clampX, clampX), 0, -10);
        }        
    }

    public void NotifyMenuStateChanged(bool nowActive) {
        active = !nowActive;
    }
}