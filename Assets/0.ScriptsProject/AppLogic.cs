using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class AppLogic : MonoBehaviour {
    // GameObjects
    private List<Cylinder> cylinders;
    GameObject imageTarget;
    GameObject mole;
    Cylinder cylinderWithMole;
    Text scoreText;
    Text timeText;

    // Variables
    private List<float> timeKillingMoles = new List<float>();
    private int highScore; // highest score
    private int score; // score at the end of the game
    private int molesHit = 0;
    private float totalGameTime = 10.0f; //fixed time of game
    private float maxMoleWaitTime = 1.0f; // time player has to hit mole, decreases as game goes on
    private float timeLastMoleWasDestroyed;
    float timeMoleAppeared;
    System.Random rnd = new System.Random();

    bool test;
    // Use this for initialization
    void Start () {
        InitCylinderObjects();
        imageTarget = GameObject.Find("ImageTarget");

        timeText = GameObject.Find("timeText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        scoreText.text = "Score: 0";


        //   StartCoroutine(trackTime());
    }


    // Update is called once per frame
    void Update () {
        timeText.text = "Time: " + Math.Floor(totalGameTime - Time.time);

        // If there is no mole, create a mole at a random time
        if (!IsMoleUp() && Time.time < totalGameTime) {            
            double randomTime = rnd.NextDouble() * maxMoleWaitTime; // based on maxMoleWaitTime
            print("random time: " + randomTime);
            if ( Time.time > 1 + timeLastMoleWasDestroyed) {
                CreateMole();
            }
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



    //private void ShowMoleOnRandomCylinder() {
    //    int randomInt = rnd.Next(0, cylinders.Count - 1);


    //    CreateMole(cyl);
    //}

    private void CreateMole() {
        int randomInt = rnd.Next(0, cylinders.Count - 1);
        Cylinder cyl = cylinders[randomInt];

        mole = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        mole.transform.position = cyl.getGameObject().transform.position;
        mole.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        mole.transform.parent = imageTarget.transform;

        mole.GetComponent<Renderer>().material.color = Color.red;
        mole.name = "Mole";

        cyl.setMoleOn();
        cylinderWithMole = cyl;
        timeMoleAppeared = Time.time;

    }

    private void DestroyMole() {
        Destroy(mole);

        timeLastMoleWasDestroyed = Time.time;
        float reactionTime = timeMoleAppeared - timeLastMoleWasDestroyed;
        timeKillingMoles.Add(reactionTime);
        maxMoleWaitTime -= 0.1f;

        cylinderWithMole.setMoleOff();

        molesHit++;
        scoreText.text = "Score: " + molesHit;
    }
    
    private bool IsMoleUp() {
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


    // Not used ===================================================

    //private IEnumerator CreateFirstMole() {
    //    yield return new WaitForSeconds(1);
    //    ShowMoleOnRandomCylinder();
    //}
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

    //private IEnumerator WaitRandomTime() {
    //    print("wait random time start: " + Time.time);

    //    while (true) {
    //        if (!IsMoleUp()) {
    //            CreateMole();
    //        }
    //        yield return new WaitForSeconds(maxHitMoleWaitTime);
    //    }

    //}
}
