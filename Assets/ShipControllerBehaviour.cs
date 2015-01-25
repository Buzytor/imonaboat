using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControllerBehaviour : MonoBehaviour {

    public List<Transform> shipPrefabs;

    private List<GameObject> ships = new List<GameObject>();

    public void CreateShip() {
        if(shipPrefabs.Count > 0) {
            Vector3 startPosition = OtherShipBehaviour.generateStartPosition();

            float alpha = Random.Range(-180, 180);
            Quaternion directionQuat = Quaternion.AngleAxis(alpha, Vector3.up);

            Transform g = (Transform)Instantiate(shipPrefabs[Random.Range(0, shipPrefabs.Count-1)],
                                                    startPosition, directionQuat);

            ships.Add(g.gameObject);
        }
    }

    public int CheckAndDeleteShips() {
        int count = 0;
        ships.RemoveAll(ship =>
                            ship.GetComponent<OtherShipBehaviour>().IsOutsideOfMap()
                            && ship.GetComponent<OtherShipBehaviour>().Delete()
                            && ++count >= 0 // DIRTY HAGZ IMMA MAGICIAN
                            );
        return count;
    }

    public void AddShips(int count) {
        for(int i=0; i<count; i++) {
            CreateShip();
        }
    }

	// Use this for initialization
	void Start () {
        AddShips(20);
	}

	// Update is called once per frame
	void Update () {
        int toAdd = CheckAndDeleteShips();
        AddShips(toAdd);
	}
}
