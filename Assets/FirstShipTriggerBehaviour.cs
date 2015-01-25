using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class FirstShipTriggerBehaviour : MonoBehaviour {
    public OverlayBehaviour overlay;

    void OnTriggerEnter(Collider collider) {
        overlay.Activate();
        Destroy(gameObject);
    }
}
