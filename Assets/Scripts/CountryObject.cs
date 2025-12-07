using System;
using UnityEngine;

public interface VisListener {
    void OnValueChanged(float newValue);
}

public class CountryObject : MonoBehaviour, VisListener {
    static readonly Color defaultColor = new Color(0.4608599F, 0.7672955F, 0.7201515F);
    
    string isoCode;

    Renderer renderer;
    
    void Start() {
        renderer = GetComponent<Renderer>();
        isoCode = gameObject.name.ToUpper();
        transform.root.GetComponent<WorldMapVisualization>().RegisterListener(this, isoCode);
    }

    public void OnValueChanged(float newValue) {
        LeanTween.cancel(gameObject);

        Color newColor = newValue > 0 ? VisualizationProfile.i.colorGradient.Evaluate(newValue) : defaultColor;

        LeanTween.color(gameObject, newColor, VisualizationProfile.colorShiftRate).setEase(LeanTweenType.easeOutCubic);
    }
}
