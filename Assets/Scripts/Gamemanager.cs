using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public TMP_Text scoreText;
    public GameObject gameOverWindow;
    public TMP_Text endScore;
    public TMP_Text highScore;
    AudioSource[] sounds;

    public static int Score = 0;

    private float startY = 3.75f;
    private bool canGenerate = true;
    private bool movingTowardsPoint = false;
    private GameObject chosenFruit;
    private float pointX;
    private string chosenName;
    private float timer = 0;
    private string[] names = { "Blueberry", "Strawberry", "Apple", "Grape","Orange","Kiwi","Coconut","Watermelon"};
    // Start is called before the first frame update
    void Start()
    {
        GenerateNewFruit();
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canGenerate && timer > 0.5f && Input.GetMouseButtonDown(0))
        {
            timer = 0;
            pointX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,-2.5f,2.5f);
            movingTowardsPoint = true;
            canGenerate = false;
            sounds[2].Play();
        }
        if(movingTowardsPoint)
        {
            chosenFruit.transform.position = Vector2.Lerp(chosenFruit.transform.position,new Vector2(pointX,startY),Time.deltaTime*5);
            if(Vector2.Distance(chosenFruit.transform.position,new Vector2(pointX,startY)) < 0.1f)
            {
                chosenFruit.transform.position = Vector2.MoveTowards(chosenFruit.transform.position, new Vector2(pointX, startY), Time.deltaTime/3);
                if(chosenFruit.transform.position.x == pointX)
                {
                    movingTowardsPoint = false;
                    if (!gameOverWindow.activeSelf) canGenerate = true;
                    canGenerate = true;
                    chosenFruit.GetComponent<Rigidbody2D>().gravityScale = 1;
                    chosenFruit.GetComponent<CircleCollider2D>().enabled = true;
                    sounds[1].Play();
                    GenerateNewFruit();
                }
            }
        }
        scoreText.text = Score.ToString();
        timer += Time.deltaTime;
    }
    void GenerateNewFruit()
    {
        int num = Random.Range(0,20);
        if (num < 14) num = 0;
        else if (num < 19) num = 1;
        else num = 2;
        chosenName = names[num];
        chosenFruit = Instantiate(fruitPrefabs[num], new Vector2(0, startY), Quaternion.identity);
        chosenFruit.transform.name = chosenName;
        chosenFruit.GetComponent<Fruit>().game = this;
    }
    public void UpgradeFruit(Vector2 position, string name)
    {
        var fruit = Instantiate(fruitPrefabs[System.Array.IndexOf(names,name)+1],position,Quaternion.identity);
        fruit.transform.name = names[System.Array.IndexOf(names, name) + 1];
        fruit.GetComponent<Rigidbody2D>().gravityScale = 1;
        fruit.GetComponent<CircleCollider2D>().enabled = true;
        fruit.GetComponent<Fruit>().game = this;
        Score += (int)Mathf.Pow(2,System.Array.IndexOf(names,name));
        sounds[0].Play();

    }
    public void GameOver()
    {
        canGenerate = false;
        gameOverWindow.SetActive(true);
        endScore.text = Score.ToString();
        if (Score > PlayerPrefs.GetInt("HighScore", 0)) PlayerPrefs.SetInt("HighScore",Score);
        highScore.text = "HIGHSCORE:\n"+PlayerPrefs.GetInt("HighScore", 0).ToString();
        sounds[3].Play();
    }
    public void Restart()
    {
        Score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
