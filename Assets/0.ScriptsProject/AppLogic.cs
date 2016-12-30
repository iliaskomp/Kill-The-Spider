using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AppLogic : MonoBehaviour {
    private List<Cylinder> cylinders;
    GameObject mole;
    Cylinder cylinderWithMole;

    private List<float> timeKillingMoles = new List<float>();
    private int highScore; // highest score
    private int score; // score at the end of the game
    private float totalGameTime = 10.0f; //fixed time of game
  //  private float hitMoleWaitTime = 2.0f; // time player has to hit mole, decreases as game goes on
    float timeMoleAppeared;
    
    
    // Use this for initialization
    void Start () {
        InitCylinderObjects();
     //   StartCoroutine(CreateFirstMole());
    }


    // Update is called once per frame
    void Update () {

        // If there is no mole, show a mole
        if (!IsAnyMoleActivated() && Time.time < totalGameTime) {
            ShowMoleOnRandomCylinder();
        } else {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100)) {
                //    Debug.Log("Game Object clicked: " + hit.transform.gameObject.name);
                    if (hit.transform.gameObject.name == "Mole" || hit.transform.gameObject.name == cylinderWithMole.getName()) {
                        print("Mole hit!");
                        DestroyMole();
                    }
                }
            }
        }




    }

    private void ShowMoleOnRandomCylinder() {
        System.Random rnd = new System.Random();
        int randomInt = rnd.Next(0, cylinders.Count - 1);

        // Get cylinder object from cylinders list
        Cylinder cyl = cylinders[randomInt];
        CreateMole(cyl.getGameObject().transform.position);
        cyl.setMoleOn();
        cylinderWithMole = cyl;

//        Debug.Log("Cylinder with mole: " + cyl.getName() + ", time: " + Time.time);
    }

    private void CreateMole(Vector3 cylPos) {
        mole = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mole.transform.position = new Vector3(cylPos.x, cylPos.y, cylPos.z);
        mole.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        mole.GetComponent<Renderer>().material.color = Color.red;
        mole.name = "Mole";
        timeMoleAppeared = Time.time;
    }

    private void DestroyMole() {
        Destroy(mole);
        float reactionTime = timeMoleAppeared - Time.time;
        timeKillingMoles.Add(reactionTime);
        cylinderWithMole.setMoleOff();
    }

    private bool IsAnyMoleActivated() {
        foreach (Cylinder c in cylinders) {
            if (c.getMoleOnState() == true) {
                return true;
            }
        }
        return false;
    }

    private void InitCylinderObjects()
    {
        cylinders = new List<Cylinder>();
        cylinders.Add(new Cylinder("Cylinder (0)"));
        cylinders.Add(new Cylinder("Cylinder (1)"));
        cylinders.Add(new Cylinder("Cylinder (2)"));
        cylinders.Add(new Cylinder("Cylinder (3)"));
        cylinders.Add(new Cylinder("Cylinder (4)"));
        cylinders.Add(new Cylinder("Cylinder (5)"));
        cylinders.Add(new Cylinder("Cylinder (6)"));
        cylinders.Add(new Cylinder("Cylinder (7)"));
        cylinders.Add(new Cylinder("Cylinder (8)"));
    }


    // Not used

    private IEnumerator CreateFirstMole() {
        yield return new WaitForSeconds(1);
        ShowMoleOnRandomCylinder();
    }
    //private IEnumerator RandomActivateCylinder() {
    //    GameObject cyl;
    //    while (true) {
    //        System.Random rnd = new System.Random();
    //        int randomInt = rnd.Next(0, cylinders.Count - 1);
    //        String randomCylinder = "Cylinder (" + randomInt + ")";
    //        Debug.Log("Random Cylinder: " + randomCylinder);

    //        cyl = GameObject.Find(randomCylinder);
    //        cyl.GetComponent<Renderer>().material.color = Color.red;
    //        Debug.Log(Time.time);
    //        yield return new WaitForSeconds(3);

    //    }
    //}

}
