using UnityEngine;

public class PCamera : MonoBehaviour {
    static PCamera i;

    public PCamera Get() {
        return i;
    }
    
    public float smoothTime;

    public Transform target;

    Vector2 velocity;

    void Awake() {
        i = this;
    }

    void Update() {
        MoveCamera();
    }

    void MoveCamera() {
        var targetPosition = target != null ? (Vector2)target.position : Vector2.zero;

        var finalPosition = targetPosition;

        var posX = Mathf.SmoothDamp(transform.position.x, finalPosition.x, ref velocity.x,
            smoothTime);
        var posY = Mathf.SmoothDamp(transform.position.y, finalPosition.y, ref velocity.y,
            smoothTime);

        transform.position = new Vector3(posX, posY, -10);
    }
}