using UnityEngine;
using System.Collections;

public class OtherShipBehaviour : MonoBehaviour {

    //full range
    public static float posRanZm = -40.0f;
    public static float posRanZM = 40.0f;
    public static float posRanXm = -40.0f;
    public static float posRanXM = 40.0f;
    //exclusion range
    public static float excRanZm = -40.0f;
    public static float excRanZM = -10.0f;
    public static float excRanXm = -20.0f;
    public static float excRanXM = 20.0f;


    private Vector3 startPosition;
    private Vector3 startDirection;

    public float speed = 0.1f;

    static Vector3 generateStartPosition() {
        float x, y, z;
        y = 0;
        do {
            x = Random.Range( posRanXm, posRanXM );
        } while(x < excRanXm || x > excRanXM);
        do {
            z = Random.Range( posRanZm, posRanZM );
        } while(z < excRanZm || z > excRanZM);
        return new Vector3(x, y, z);
    }

    static Vector3 generateStartDirection() {
        float x, y, z;
        y = 0;
        x = Random.Range(posRanXm, posRanXM);
        z = Random.Range(posRanZm, posRanZM);
        Vector3 result = new Vector3(x, y, z);
        return result.normalized;
    }

	// Use this for initialization
    void Start() {
        startPosition = generateStartPosition();
        startDirection = generateStartDirection();

        Quaternion directionQuat = new Quaternion();
        directionQuat.SetLookRotation(startDirection-startPosition, Vector3.forward); //it's down because the ship is scaled by -Z

        this.gameObject.transform.position = startPosition;
        this.gameObject.transform.rotation = directionQuat;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up*speed);
	}
}
