using System;
using UnityEngine;

public interface VisListener {
    void OnValueChanged(float newValue);
}

public class CountryObject : MonoBehaviour, VisListener {
    string isoCode;

    Renderer renderer;
    
    void Start() {
        renderer = GetComponent<Renderer>();
        isoCode = gameObject.name.ToUpper();
        transform.root.GetComponent<WorldMapVisualization>().RegisterListener(this, isoCode);
    }

    public void OnValueChanged(float newValue) {
         renderer.material.color = VisualizationProfile.i.colorGradient.Evaluate(newValue);
    }
}