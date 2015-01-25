using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OverlayBehaviour : MonoBehaviour {
    public Image overlay;
    public RectTransform sailor;

    public Vector3 targetPosition;
    public Vector3 startPosition;
    public float overlayTargetAlpha;

    private bool show = false;
    private bool shown = false;
    private float t = 0.0f;

	// Use this for initialization
	void Start () {
        sailor.anchoredPosition = startPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if(shown) {
            if(Input.GetKey(KeyCode.Space)) {
                Destroy(gameObject);
            }
        } else if(show) {
            t += Time.deltaTime;
            sailor.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            Color c = overlay.color;
            c.a = Mathf.Lerp(0, overlayTargetAlpha, t);
            overlay.color = c;
            if(Vector3.Distance(sailor.anchoredPosition, targetPosition) < 0.1f) {
                shown = true;
                show = false;
            }
        }
	}

    public void Activate() {
        show = true;
    }
}
