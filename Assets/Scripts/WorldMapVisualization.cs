using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapVisualization : MonoBehaviour {
    readonly Dictionary<string, List<VisListener>> listeners = new();
    readonly Dictionary<string, float> values = new();
    
    public void RegisterListener(VisListener listener, string isoCode) {
        if (!listeners.ContainsKey(isoCode)) {
            listeners.Add(isoCode, new List<VisListener>());
        }
        
        listeners[isoCode].Add(listener);
        
        if (values.ContainsKey(isoCode)) {
            listener.OnValueChanged(values[isoCode]);
        } else {
            listener.OnValueChanged(0);
        }
    }

    public void NotifyListeners(string isoCode) {
        foreach (var listener in listeners[isoCode]) {
            if (values.ContainsKey(isoCode)) {
                listener.OnValueChanged(values[isoCode]);
            } else {
                listener.OnValueChanged(0);
            }
        }
    }

    public void SetValue(string isoCode, float value) {
        if (!values.ContainsKey(isoCode)) {
            values.Add(isoCode, value);
        }
        else {
            values[isoCode] = value;
        }

        NotifyListeners(isoCode);        
    }
}