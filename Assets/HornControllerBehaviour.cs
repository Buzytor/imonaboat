using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HornControllerBehaviour : MonoBehaviour {
    public enum SignalType {
        Short,
        Long
    };

    public enum Signal {
        NoSignal,
        NotRecognized,
        Left,
        Right
    }

    private List<SignalType> registeredSignals = new List<SignalType>();

    internal void RegisterSignal(SignalType signal) {
        registeredSignals.Add(signal);
    }

    internal void SignalsActivate() {
        Signal s = RecognizeSignal(registeredSignals.ToArray());
        registeredSignals.Clear();

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Obstacle")) {
            g.GetComponent<OtherShipBehaviour>().ReactToSignal(s);
        }
    }

    private Signal RecognizeSignal(SignalType[] signals) {
        if(signals.Length < 1) {
            return Signal.NoSignal;
        }
        if(signals.Length == 1) {
            if(signals[0] == SignalType.Short) {
                return Signal.Left;
            }
        }
        if(signals.Length == 2) {
            if(signals[0] == SignalType.Short) {
                if(signals[1] == SignalType.Short) {
                    return Signal.Right;
                }
            }
        }

        return Signal.NotRecognized;
    }
}
