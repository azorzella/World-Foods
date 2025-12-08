using UnityEngine;

public class VisualizationProfile : MonoBehaviour {
    public Gradient colorGradient;
    public const float colorShiftRate = 1F;
    
    static VisualizationProfile _i;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// Returns the static instance of VisualizationProfile. If the instance has
    /// not been set, then it instantiates a new instance of VisualizationProfile
    /// using the resource loader, caches it,and returns it
    /// </summary>
    public static VisualizationProfile i {
        get {
            if (_i == null) {
                VisualizationProfile x = Resources.Load<VisualizationProfile>("VisualizationProfile");

                _i = Instantiate(x);
            }
            return _i;
        }
    }
}