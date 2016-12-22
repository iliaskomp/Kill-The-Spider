using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AppLogic : MonoBehaviour {
    List<GameObject> cylinders;

    // Use this for initialization
    void Start () {
        cylinders = new List<GameObject>();
        cylinders.Add(GameObject.Find("Cylinder"));
        cylinders.Add(GameObject.Find("Cylinder (1)"));
        cylinders.Add(GameObject.Find("Cylinder (2)"));
        cylinders.Add(GameObject.Find("Cylinder (3)"));
        cylinders.Add(GameObject.Find("Cylinder (4)"));
        cylinders.Add(GameObject.Find("Cylinder (5)"));
        cylinders.Add(GameObject.Find("Cylinder (6)"));
        cylinders.Add(GameObject.Find("Cylinder (7)"));

    }

    // Update is called once per frame
    void Update () {

        StartCoroutine(RandomActivateCylinder());

        //if (Input.GetMouseButtonDown(0)) {
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100))
        //    {
        //        Debug.Log(hit.transform.gameObject.name);

        //        if (hit.transform.gameObject.tag == "cylinder") {
        //            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        //        }

        //    }
        //}
    }

    private IEnumerator RandomActivateCylinder()
    {
        yield return new WaitForSeconds(5);

        Debug.Log("RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRandom");
        System.Random rnd = new System.Random();
        int randomInt = rnd.Next(0, cylinders.Count);
        String randomCylinder = "Cylinder (" + randomInt + ")";
        GameObject cyl = GameObject.Find(randomCylinder);
        cyl.GetComponent<Renderer>().material.color = Color.red;


    }
}
