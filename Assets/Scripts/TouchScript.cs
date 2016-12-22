using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchScript : MonoBehaviour {
    Text touchText;
	// Use this for initialization
	void Start () {
	    touchText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        Touch myTouch;
        //if (Input.touchCount > 0) {
            myTouch = Input.GetTouch(0);
        //};
        //myTouches = Input.touches;

        for (int i = 0; i < Input.touchCount; i++) {
            touchText.text = "X: " + myTouch.position.x + "Y: " + myTouch.position.y + "\n";
        }
	}
}
