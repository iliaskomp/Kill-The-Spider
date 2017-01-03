using UnityEngine;
using System.Collections;

public class CylinderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
