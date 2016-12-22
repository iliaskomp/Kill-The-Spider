using UnityEngine;
using System.Collections;
using Vuforia;
using System;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IVirtualButtonEventHandler
{
    GameObject buttonCube;
    Text textCount;

    // Use this for initialization
    void Start()
    {
        textCount = GameObject.Find("CountText").GetComponent<Text>();
        buttonCube = GameObject.Find("ButtonCube");
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
           vbs[i].RegisterEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}


    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        GameObject[] test = GameObject.FindGameObjectsWithTag("test");

//        foreach (GameObject g in test) {
//            if (g is CylinderTarget )
//        }


        buttonCube.GetComponent<Renderer>().material.color = Color.green;

        
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
   //     Debug.Log("Button Released");
    //    transform.Translate(new Vector3(1, 0, 0));

    }
}
