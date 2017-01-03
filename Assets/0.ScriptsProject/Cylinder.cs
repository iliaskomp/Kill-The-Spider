using UnityEngine;
using System.Collections;

//public class Cylinder : MonoBehaviour {
public class Cylinder { 
    private string name;
    private GameObject gameObject;
    private bool isSpiderOn;

    public Cylinder(string name) {
        this.name = name;
        this.gameObject = GameObject.Find(name);
        isSpiderOn = false;
    }




    // Setters and Getters
    public void setSpiderOn() {
        isSpiderOn = true;
    }

    public void setSpiderOff() {
        isSpiderOn = false;
    }

    public bool getSpiderOnState() {
        return isSpiderOn;
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
