using UnityEngine;
using System.Collections;
using Vuforia;
using System;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IVirtualButtonEventHandler
{
    GameObject buttonCube;
    UnityEngine.UI.Text textCount;
    int buttonPressedCount;
    // Use this for initialization
    void Start()
    {
        textCount = GameObject.Find("CountText").GetComponent<Text>();
        buttonPressedCount = 0;
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
        buttonPressedCount++;
        Debug.Log("Button Pressed Count: " + buttonPressedCount);
        textCount.text = "Count: " + buttonPressedCount;
        buttonCube.GetComponent<Renderer>().material.color = Color.green;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(buttonPressedCount * 0.1f, 0, 0);
        cube.transform.localScale -= new Vector3(0.9F, 0.9F, 0.9F);

    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
   //     Debug.Log("Button Released");
    //    transform.Translate(new Vector3(1, 0, 0));

    }
}
