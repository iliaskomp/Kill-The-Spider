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
    GameObject spider;
    Cylinder cylinderWithSpider;
    Text timeText;
    Text scoreText;
    Text highScoreText;
    Text gameOverText;
    Text startGameText;
    
    // Audio
    AudioSource audioSrc;
    public AudioClip spiderAppearsSound;
    public AudioClip spiderDestroyedSound;

    // Variables
    private int highScore; // highest score
    private int score; // score at the end of the game
    public static bool gameOver; // boolean for if game is over or not
    private bool toStartGame;

    // Time variables
    private float currentTime;
    private float totalGameTime = 20.0f; //fixed time of game
    private float maxSpiderWaitTime = 2.0f; // time player has to hit spider, decreases as game goes on
    private float spiderSelfDestroyTime = 0.7f; // Time that spider self destroys if not being killed
    private float timeLastSpiderWasDestroyed; // Time the last spider was destroyed
    private float timeSpiderAppeared;
    private float startTime;

    private System.Random rnd = new System.Random();
    // Use this for initialization
    void Start () {

        gameOver = false;
        toStartGame = true;

        InitCylinderObjects();
        
        imageTarget   = GameObject.Find("ImageTarget");
        timeText      = GameObject.Find("timeText").GetComponent<Text>();
        scoreText     = GameObject.Find("scoreText").GetComponent<Text>();
        highScoreText = GameObject.Find("highScoreText").GetComponent<Text>();
        gameOverText  = GameObject.Find("gameOverText").GetComponent<Text>();
        startGameText = GameObject.Find("startGameText").GetComponent<Text>();

        spider = GameObject.Find("spider");
        spider.SetActive(false);

        score = 0;        
        scoreText.text = "Score: 0";
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0);
        startTime = 1000;

        audioSrc = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        audioSrc.Play();
    }


    // Update is called once per frame
    void Update () {
        

        if (toStartGame) {
            startGameText.text = "Touch to start playing! \nCurrent High Score: " + PlayerPrefs.GetInt("highscore", 0);
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R)) {
                toStartGame = false;
                startGameText.text = "";
                print("Time when touch to start: " + Time.time);
                startTime = Time.timeSinceLevelLoad;

            }
        }


        // start time: 8 seconds since level loaded
        // total game time : 5 seconds
        // game range: 8 - 13 seconds

        print("Current Time: " + Time.timeSinceLevelLoad);
        print("Start game time: " + startTime);
        

        currentTime = Time.timeSinceLevelLoad;

        // Time/Game is over
        //if 13 > 5 seconds + 8 seconds
        if (currentTime > startTime + totalGameTime) {
            gameOver = true;
        }
        // If there is no spider, create a spider at a random time
        if (!gameOver && !toStartGame) {
            timeText.text = "Time: " + Math.Floor(totalGameTime - currentTime);

            // If there is no spider, create one at random time/place
            if (!IsSpiderUp() ) {
                double randomTime = rnd.NextDouble() * maxSpiderWaitTime; // based on maxspiderWaitTime
                if (currentTime > randomTime + timeLastSpiderWasDestroyed) {
                    CreateSpider();
                }
            // If there is a spider, check if it's been touched 
            } else {

                double randomSelfDestroyTime = rnd.NextDouble() * spiderSelfDestroyTime + spiderSelfDestroyTime; // based on maxspiderWaitTime

                if (currentTime > timeSpiderAppeared + randomSelfDestroyTime) {

                    score = score - 1;
                    DestroySpider();

                }

                if (Input.GetMouseButtonDown(0)) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100)) {
                        //    Debug.Log("Game Object clicked: " + hit.transform.gameObject.name);
                        if (hit.transform.gameObject.name == "spider" || hit.transform.gameObject.name == cylinderWithSpider.getName()) {
                            score++;
                            audioSrc.PlayOneShot(spiderDestroyedSound);

                            DestroySpider();

                        }
                    }
                }
            }
        }

        // If game is over
        if (gameOver) {
            StoreHighscore(score);
            audioSrc.Stop();
            //TextMesh t = (TextMesh)gameOverText.GetComponent(typeof(TextMesh));
            
            //t.text = "GAME OVER \n (Touch to restart);
            if (score > PlayerPrefs.GetInt("highscore", 0)) {
                gameOverText.text = "GAME OVER \n(Touch to restart) \nCongrats! New high score!";
            } else {
                gameOverText.text = "GAME OVER \n(Touch to restart)";
            }

            // Restart game if screen touched or pressed R.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene("ProjectScene");
            }
        }
    }

    private void CreateSpider() {
        int randomInt = rnd.Next(0, cylinders.Count - 1);
        Cylinder cyl = cylinders[randomInt];

        //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        spider.transform.position = cyl.getGameObject().transform.position;
        spider.transform.parent = imageTarget.transform;
        spider.SetActive(true);
        audioSrc.PlayOneShot(spiderAppearsSound);

        spider.name = "Spider";

        cyl.setSpiderOn();
        cylinderWithSpider = cyl;
        timeSpiderAppeared = currentTime;

    }

    private void DestroySpider() {
        //Destroy(spider);
        spider.SetActive(false);
        timeLastSpiderWasDestroyed = currentTime;
        float reactionTime = timeSpiderAppeared - timeLastSpiderWasDestroyed;
        maxSpiderWaitTime -= 0.1f;

        cylinderWithSpider.setSpiderOff();

        scoreText.text = "Score: " + score;

    }

    private bool IsSpiderUp() {
        foreach (Cylinder c in cylinders) {
            if (c.getSpiderOnState() == true) {
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

    public static bool IsGameOver() {
        return gameOver;
    }
}
