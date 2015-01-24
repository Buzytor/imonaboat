using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameFailureBehaviour : MonoBehaviour {
    public float gameOverWaitSeconds = 5.0f;
    public Text guiGameOverText;
    public Text guiPressSpaceText;

    private bool isGameOver = false;

	// Use this for initialization
	void Start () {
        isGameOver = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(isGameOver && Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel(0);
        }
	}

    internal void GameOver() {
        guiGameOverText.text = "Game Over!";
        guiPressSpaceText.text = "Press SPACE to restart!";
        isGameOver = true;
    }
}
