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
        LeanTween.cancel(gameObject);

        Color newColor = VisualizationProfile.i.colorGradient.Evaluate(newValue);

        LeanTween.color(gameObject, newColor, VisualizationProfile.colorShiftRate).setEase(LeanTweenType.easeOutCubic);
    }
}