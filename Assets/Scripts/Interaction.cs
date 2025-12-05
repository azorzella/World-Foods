using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour {
    Camera camera;

    void Start() {
        camera = Camera.main;
    }

    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit)) {
                Debug.Log(hit.transform.name);
            }
        }
    }
}