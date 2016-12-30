using UnityEngine;
using System.Collections;

public class CrateScript2 : MonoBehaviour {

    //GameObject stones;
   // GameObject tarmac;
    Vector3 stonesPos = GameObject.Find("ImageTargetStones").transform.position;
    Vector3 tarmacPos = GameObject.Find("ImageTargetTarmac").transform.position;
    Vector3 pos;
    
    float speed = 0.1f;
    // Use this for initialization
    void Start () {
        pos = transform.position;
    //    stones = GameObject.Find("ImageTargetStones");
     //   tarmac = GameObject.Find("ImageTargetTarmac");
    }
	
	// Update is called once per frame
	void Update () {
        //   transform.Rotate(100 * Time.deltaTime, 100 * Time.deltaTime , 100 * Time.deltaTime);

        pos = transform.position;

        transform.position = Vector3.Lerp(stonesPos, tarmacPos, speed * Time.deltaTime);

    }
}
