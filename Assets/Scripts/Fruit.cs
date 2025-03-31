using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Gamemanager game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == transform.name && transform.name!="Watermelon") 
        {
            if (GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                Destroy(collision.gameObject); 
                game.UpgradeFruit(transform.position, transform.name);
                Destroy(gameObject);
            }
        }
    }
}
