using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {
    bool gameOver;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameOver = AppLogic.IsGameOver();

        if (gameOver) {
        }
    }
}
