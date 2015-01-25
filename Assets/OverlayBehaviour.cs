using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OverlayBehaviour : MonoBehaviour {

    public RectTransform sailor;

    public Vector3 targetPosition;
    public Vector3 startPosition;
    public float overlayTargetAlpha;

    private bool show = false;
    private bool shown = false;
    Color overlayColor;
    private float t = 0.0f;

	// Use this for initialization
	void Start () {
        sailor.anchoredPosition = startPosition;
        overlayColor = GetComponent<Image>().color;
        overlayColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("GOWNO");
        if(shown) {
            Debug.Log("PICASSO");
            if(Input.GetKey(KeyCode.Space)) {
                Destroy(gameObject);
            }
        } else if(show) {
            Debug.Log("MALY");
            t += Time.deltaTime;
            sailor.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            overlayColor.a = Mathf.Lerp(0, overlayTargetAlpha, t);
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
