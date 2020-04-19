using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Beta"))
        {
            //Debug.Log("Hit Beta");
            for (int i = 0; i < EnemySpawn1.be.Count; i++)
            {
                if (EnemySpawn1.be[i] == collision.gameObject)
                {
                    EnemySpawn1.bhlist[i]++;
                    if (EnemySpawn1.bhlist[i] >= 3)
                    {
                        EnemySpawn1.bhlist.RemoveAt(i);
                        EnemySpawn1.be.RemoveAt(i);
                        //Debug.Log("Removed from list");
                        Destroy(collision.gameObject);
                        Player.score += 10;
                        //Debug.Log("Destroyed object");
                        EnemySpawn1.bcnt--;
                        EnemySpawn1.tcnt--;
                    }
                }
            }
        }
        else if (collision.gameObject.CompareTag("Alpha"))
        {
            //Debug.Log("Hit Alpha");
            for (int i = 0; i < EnemySpawn1.al.Count; i++)
            {
                if (EnemySpawn1.al[i] == collision.gameObject)
                {
                    EnemySpawn1.ahlist[i]++;
                    if (EnemySpawn1.ahlist[i] >= 5)
                    {
                        EnemySpawn1.ahlist.RemoveAt(i);
                        EnemySpawn1.al.RemoveAt(i);
                        Destroy(collision.gameObject);
                        Player.score += 30;
                        EnemySpawn1.acnt--;
                        EnemySpawn1.tcnt--;
                    }
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
