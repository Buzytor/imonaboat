using UnityEngine;
using System.Collections;

public class PlayerShipBehaviour : MonoBehaviour {

   
    public float speed = 1.0f;
    public Vector3 velocity;

    Wobbler wob = new Wobbler();

	// Use this for initialization
	void Start () {
        velocity = -Vector3.forward*speed;
	}
	
	// Update is called once per frame
	void Update () {
        float w = wob.GetWobWob();
        Debug.Log(w);
        transform.Translate(Vector3.up*w*Time.deltaTime);
	}

    void OnTrigerEnter(Collider collider) {
        // TODO failure state
        Debug.Log("Collision!");
    }
}
