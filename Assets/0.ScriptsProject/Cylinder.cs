using UnityEngine;
using System.Collections;

//public class Cylinder : MonoBehaviour {
public class Cylinder { 
    private string name;
    private GameObject gameObject;
    private bool isMoleOn;

    public Cylinder(string name) {
        this.name = name;
        this.gameObject = GameObject.Find(name);
        isMoleOn = false;
    }




    // Setters and Getters
    public void setMoleOn() {
        isMoleOn = true;
    }

    public void setMoleOff() {
        isMoleOn = false;
    }

    public bool getMoleOnState() {
        return isMoleOn;
    }

    public GameObject getGameObject() {
        return gameObject;
    }
    
    public string getName() {
        return name;
    }


	//// Use this for initialization
	//void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//}
}
