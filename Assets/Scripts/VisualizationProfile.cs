using UnityEngine;

public class VisualizationProfile : MonoBehaviour {
    public Gradient colorGradient;
    
    static VisualizationProfile _i;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }
	
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