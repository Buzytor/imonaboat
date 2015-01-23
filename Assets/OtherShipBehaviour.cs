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
    private Vector3 startAngle;

    public float speed = 1.0f;

    private PlayerShipBehaviour player;

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

	// Use this for initialization
    void Start() {
        player = GameObject.Find("PlayerShip").GetComponent<PlayerShipBehaviour>();

        startPosition = generateStartPosition();

        float alpha = Random.Range(-180, 180);
        Debug.Log(alpha);
        Quaternion directionQuat = Quaternion.AngleAxis(alpha, Vector3.up);

        this.gameObject.transform.position = startPosition;
        this.gameObject.transform.rotation = directionQuat;
    }
	
	// Update is called once per frame
	void Update() {
        transform.Translate(Vector3.forward*speed*Time.deltaTime, Space.Self);
        transform.Translate(player.velocity*Time.deltaTime, Space.World);
	}
    
}
