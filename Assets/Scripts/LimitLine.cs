using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLine : MonoBehaviour
{
    public Gamemanager game;

    private float timer = 0;
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activated) timer += Time.deltaTime;
        if(timer>=1) game.GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        timer = 0;
        activated = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        activated = false;
    }
}
