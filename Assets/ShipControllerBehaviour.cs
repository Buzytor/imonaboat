using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ShipControllerBehaviour : MonoBehaviour {

    public List<Transform> shipPrefabs;

    private List<GameObject> ships = new List<GameObject>();

    private string LevelsJSON = "[[{'pos': [0, 0 , 46], 'angle': 180}]]";

    private JSONNode Levels;

    private int curLevel = 0;

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

    public void CreateShip(Vector3 startPosition, float alpha) {
        if(shipPrefabs.Count > 0) {
            Quaternion directionQuat = Quaternion.AngleAxis(alpha, Vector3.up);

            Transform g = (Transform)Instantiate(shipPrefabs[Random.Range(0, shipPrefabs.Count-1)],
                                                    startPosition, directionQuat);

            ships.Add(g.gameObject);
        }
    }

    public void LoadLevel(int lvl) {
        for(int i = 0; i < Levels[lvl].Count; i ++) {
            Vector3 pos = new Vector3(Levels[lvl][i]["pos"][0].AsFloat, Levels[lvl][i]["pos"][1].AsFloat, Levels[lvl][i]["pos"][2].AsFloat);
            float angle = Levels[lvl][i]["angle"].AsFloat;
            CreateShip(pos, angle);
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
        Levels = JSON.Parse(LevelsJSON);
	}

	// Update is called once per frame
	void Update () {
        if(curLevel < Levels.Count) {
            if(CheckAndDeleteShips() == 0) {
                LoadLevel(curLevel);
                curLevel ++;
            }
        } else if(curLevel == Levels.Count) {
            AddShips(20);
            curLevel++;
        } else {
            int toAdd = CheckAndDeleteShips();
            AddShips(toAdd);
        }
	}
}
