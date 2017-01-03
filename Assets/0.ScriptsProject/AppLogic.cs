using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppLogic : MonoBehaviour {
    // GameObjects
    private List<Cylinder> cylinders;
    GameObject imageTarget;
    GameObject mole;
    Cylinder cylinderWithMole;
    Text timeText;
    Text scoreText;
    Text highScoreText;
    GameObject gameOverText;

    // Variables
    //private List<float> timeKillingMoles = new List<float>();
    private int highScore; // highest score
    private int score; // score at the end of the game
    public bool gameOver; // boolean for if game is over or not
    
    private float currentTime;
    private float totalGameTime = 5.0f; //fixed time of game
    private float maxMoleWaitTime = 2.0f; // time player has to hit mole, decreases as game goes on
    private float moleSelfDestroyTime = 0.7f; // Time that mole self destroys if not being killed
    private float timeLastMoleWasDestroyed; // Time the last mole was destroyed
    private float timeMoleAppeared;
    private System.Random rnd = new System.Random();

    bool test;
    // Use this for initialization
    void Start () {
        InitCylinderObjects();
        
        imageTarget = GameObject.Find("ImageTarget");
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        highScoreText = GameObject.Find("highScoreText").GetComponent<Text>();
        gameOverText = GameObject.Find("gameOverText");
        
        score = 0;
        
        scoreText.text = "Score: 0";
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0);
    }


    // Update is called once per frame
    void Update () {
        currentTime = Time.timeSinceLevelLoad;

        // Time/Game is over
        if (currentTime > totalGameTime) {
            gameOver = true;
        }
        // If there is no mole, create a mole at a random time
        if (!gameOver) {
            timeText.text = "Time: " + Math.Floor(totalGameTime - currentTime);

            // If there is no mole, create one at random time/place
            if (!IsMoleUp() ) {
                double randomTime = rnd.NextDouble() * maxMoleWaitTime; // based on maxMoleWaitTime
                if (currentTime > randomTime + timeLastMoleWasDestroyed) {
                    CreateMole();
                }
            // If there is a mole, check if it's been touched 
            } else {

                double randomSelfDestroyTime = rnd.NextDouble() * moleSelfDestroyTime + moleSelfDestroyTime; // based on maxMoleWaitTime

                if (currentTime > timeMoleAppeared + randomSelfDestroyTime) {
                    print("mole self destroyed! score: " + score);

                    score = score - 1;
                    DestroyMole();

                }

                if (Input.GetMouseButtonDown(0)) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100)) {
                        //    Debug.Log("Game Object clicked: " + hit.transform.gameObject.name);
                        if (hit.transform.gameObject.name == "Mole" || hit.transform.gameObject.name == cylinderWithMole.getName()) {
                            print("Mole hit!");
                            score++;

                            DestroyMole();

                        }
                    }
                }
            }
        }

        // If game is over
        if (gameOver) {
            StoreHighscore(score);

            TextMesh t = (TextMesh)gameOverText.GetComponent(typeof(TextMesh));
            //t.text = "GAME OVER \n (Touch to restart);
            if (score > PlayerPrefs.GetInt("highscore", 0)) {
                t.text = "GAME OVER \n(Touch to restart) \nCongrats! \nNew high score!";
            } else {
                t.text = "GAME OVER \n(Touch to restart)";
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                Application.LoadLevel(Application.loadedLevel);
            }

            if (Input.GetMouseButtonDown(0)) {
                Application.LoadLevel(Application.loadedLevel);
                print("touch");
            }
        }
    }



    private void createRestartButton() {
        //var button = Instantiate(RoomButton, Vector3.zero, Quaternion.identity) as Button;
        //var rectTransform = button.GetComponent<RectTransform>();
        //rectTransform.SetParent(Canvas.transform);
        //rectTransform.offsetMin = Vector2.zero;
        //rectTransform.offsetMax = Vector2.zero;
        //button.onClick.AddListener(SpawnPlayer);\

        

        //mole.transform.position = cyl.getGameObject().transform.position;
        //mole.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        //mole.transform.parent = imageTarget.transform;

        //mole.GetComponent<Renderer>().material.color = Color.red;
        //mole.name = "Mole";

    }

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
        timeMoleAppeared = currentTime;

    }

    private void DestroyMole() {
        Destroy(mole);

        timeLastMoleWasDestroyed = currentTime;
        float reactionTime = timeMoleAppeared - timeLastMoleWasDestroyed;
      //  timeKillingMoles.Add(reactionTime);
        maxMoleWaitTime -= 0.1f;

        cylinderWithMole.setMoleOff();

        scoreText.text = "Score: " + score;
        

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

    void StoreHighscore(int newHighscore) {
        
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore) {
            PlayerPrefs.SetInt("highscore", newHighscore);
        }

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

    //private void ShowMoleOnRandomCylinder() {
    //    int randomInt = rnd.Next(0, cylinders.Count - 1);


    //    CreateMole(cyl);
    //}
}
