using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccelerometerScript : MonoBehaviour {
    Text debugText;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.acceleration.x, 0, Input.acceleration.z);
	}
}
