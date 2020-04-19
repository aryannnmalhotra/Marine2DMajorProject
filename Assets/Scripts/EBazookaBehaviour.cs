using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBazookaBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player.isImmunity == false)
            {
                Player.health -= 50;
            }
            else
            {
                Player.isImmunity = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
