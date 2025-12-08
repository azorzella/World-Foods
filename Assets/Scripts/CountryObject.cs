using System;
using UnityEngine;

public class CountryObject : MonoBehaviour, VisListener {
    static readonly Color defaultColor = new(0.4608599F, 0.7672955F, 0.7201515F);
    
    string isoCode;
    
    void Start() {
        Initialize();
    }

    /// <summary>
    /// Caches the country object's ISO code and registers it to the WorldMapVisualization
    /// </summary>
    void Initialize() {
        isoCode = gameObject.name.ToUpper();
        transform.root.GetComponent<WorldMapVisualization>().RegisterListener(this, isoCode);
    }

    /// <summary>
    /// Updates the object's color whenever the WorldMapVisualization notifies it of its new gradient value
    /// </summary>
    /// <param name="newValue"></param>
    public void OnValueChanged(float newValue) {
        LeanTween.cancel(gameObject);

        Color newColor = newValue > 0 ? VisualizationProfile.i.colorGradient.Evaluate(newValue) : defaultColor;

        LeanTween.color(gameObject, newColor, VisualizationProfile.colorShiftRate).setEase(LeanTweenType.easeOutCubic);
    }
}
