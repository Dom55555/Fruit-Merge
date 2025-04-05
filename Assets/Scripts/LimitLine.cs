using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLine : MonoBehaviour
{
    public Gamemanager game;

    private Dictionary<GameObject, float> fruits = new Dictionary<GameObject, float>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> keys = new List<GameObject>(fruits.Keys); 
        foreach (GameObject key in keys)
        {
            fruits[key] += Time.deltaTime;
            if (fruits[key]>=2)
            {
                game.GameOver();
                enabled = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        fruits.Add(collision.gameObject,0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        fruits.Remove(collision.gameObject);
    }
}
