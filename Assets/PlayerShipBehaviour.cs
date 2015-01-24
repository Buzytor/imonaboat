using UnityEngine;
using System.Collections;

public class PlayerShipBehaviour : MonoBehaviour {

   
    public float speed = 1.0f;
    public Vector3 velocity;

    Wobbler wob;

	// Use this for initialization
	void Start () {
        velocity = -Vector3.forward*speed;
        wob = new Wobbler();
    }
	
	// Update is called once per frame
	void Update () {
        float w = wob.GetWobWob();
        transform.Translate(Vector3.up*w*Time.deltaTime, Space.World);
	}

    void OnCollisionEnter(Collision collider) {
        // TODO failure state
        Debug.Log("Collision!");
    }
}
