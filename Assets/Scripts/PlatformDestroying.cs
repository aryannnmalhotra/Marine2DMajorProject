using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroying : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            for (int i = 0; i < MarineShooting.bullets.Count; i++)
            {
                //Debug.Log("Running");
                if (MarineShooting.bullets[i] == collision.gameObject)
                {
                    //Debug.Log("Hit");
                    MarineShooting.bdir.RemoveAt(i);
                    var gameobject = MarineShooting.bullets[i];
                    MarineShooting.bullets.RemoveAt(i);
                    //Debug.Log("Removed");
                    Destroy(collision.gameObject);
                    //Debug.Log("Destroyed bullet");
                }
            }
        }
        if (collision.gameObject.CompareTag("Bazooka"))
        {
            for (int i = 0; i < MarineShooting.bazookas.Count; i++)
            {
                //Debug.Log("Running");
                if (MarineShooting.bazookas[i] == collision.gameObject)
                {
                    //Debug.Log("Hit");
                    MarineShooting.adir.RemoveAt(i);
                    var gameobject = MarineShooting.bazookas[i];
                    MarineShooting.bazookas.RemoveAt(i);
                    //Debug.Log("Removed");
                    Destroy(collision.gameObject);
                    //Debug.Log("Destroyed bullet");
                }
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
