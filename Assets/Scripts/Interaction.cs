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
        Initialize();
    }
    
    /// <summary>
    /// Creates a new instance of the user input script and then
    /// listens for the drag, press started, and press cancelled
    /// actions. It then caches the camera, its transform, and the
    /// first instance of WorldMapVisualization.cs it can find.
    /// Finally, it registers itself to the first menu it can find
    /// </summary>
    void Initialize() {
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

    /// <summary>
    /// Caches the position that the user presses the screen
    /// whenever they touch it
    /// </summary>
    /// <param name="context"></param>
    void OnPress(InputAction.CallbackContext context) {
        positionLastPressed = Pointer.current.position.ReadValue();
    }

    /// <summary>
    /// Checks if the user released the screen close to where they pressed it. If they
    /// moved their finger too much, it means they were probably dragging the map around.
    /// Otherwise, if the dish view isn't open already, the dish view displaying all the
    /// dishes that the user has eaten from that country
    /// </summary>
    /// <param name="context"></param>
    void OnRelease(InputAction.CallbackContext context) {
        Vector2 positionLastReleased = Pointer.current.position.ReadValue();
        
        Vector2 difference = positionLastPressed - positionLastReleased;
        float magnitude = difference.magnitude;
        
        if (active && !DishView.i.IsVisible() && magnitude < 0.02F) {
            Ray ray = camera.ScreenPointToRay(positionLastReleased);

            if (Physics.Raycast(ray, out var hit)) {
                string isoCode = hit.transform.name;
                DishView.i.OpenDisplaying(vis.GetLoggedDishesFromCountry(isoCode));
            }
        }
    }

    const float clampX = 10;
    const float sensitivity = 0.0045F;
    
    /// <summary>
    /// Moves the camera whenever the user drags their finger on the screen
    /// according to the delta of their drag scaled by a sensitivity factor
    /// </summary>
    /// <param name="context"></param>
    void OnDrag(InputAction.CallbackContext context) {
        if (active && !DishView.i.IsVisible()) {
            Vector2 currentPosition = cameraTransform.position;

            currentPosition -= context.ReadValue<Vector2>() * sensitivity;
            cameraTransform.position = new Vector3(Mathf.Clamp(currentPosition.x, -clampX, clampX), 0, -10);
        }        
    }

    /// <summary>
    /// Sets itself to active if the map is selected in the menu
    /// </summary>
    /// <param name="currentIndex"></param>
    public void NotifyMenuStateChanged(int currentIndex) {
        active = currentIndex < 0;
    }
}