using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerShipAudioManager))]
public class PlayerShipBehaviour : MonoBehaviour {
    public float speed = 2.0f;
    public Vector3 velocity;

    Wobbler wob;

    private bool spacePressedDown;

	// Use this for initialization
	void Start () {
        velocity = -Vector3.forward*speed;
        wob = new Wobbler(0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        float w = wob.GetWobWob();
        transform.Translate(Vector3.up*w*Time.deltaTime, Space.World);

   

        if(Input.GetKey(KeyCode.Space) && !spacePressedDown) {
            // TODO do horn stuff

            // play sounds
            GetComponent<PlayerShipAudioManager>().StartHorn();
            spacePressedDown  = true;
        } else if(!Input.GetKey(KeyCode.Space) && spacePressedDown) {
            // TODO do horn stuff
            GetComponent<PlayerShipAudioManager>().StopHorn();
            spacePressedDown = false;
        }

	}

    void OnTriggerEnter(Collider collider) {
        GameObject c = GameObject.Find("GameController");
        c.GetComponent<GameFailureBehaviour>().GameOver();
    }
}
