using UnityEngine;
using System.Collections;

public class WavesBehaviour : MonoBehaviour {

    public float wobbleScale;

    Wobbler wob;

    // Use this for initialization
    void Start () {
        wob = new Wobbler(wobbleScale);
    }
    
    // Update is called once per frame
    void Update () {
        float w = wob.GetWobWob();
        transform.Translate(Vector3.up*w*Time.deltaTime, Space.World);
    }
}
