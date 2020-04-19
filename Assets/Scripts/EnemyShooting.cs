using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject betaBullet;
    public GameObject alphaBullet;
    public static List<GameObject> bBul = new List<GameObject>();
    public static List<GameObject> aBul = new List<GameObject>();
    public static List<int> bBulDir = new List<int>();
    public static List<int> aBulDir = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shooting", 0.0f, 1.0f);
    }
    private void Shooting()
    {
        if(Player.IsPlayerAlive == true && Player.isPlayerGrounded == true)
        {
            for(int i = 0; i < EnemySpawn1.be.Count; i++)
            {
                int x = Random.Range(1, 4);
                if (x == 1|| x==2)
                {
                    var newSprite = Instantiate(betaBullet) as GameObject;
                    int d = EnemySpawn1.enemDir[i];
                    newSprite.transform.position = new Vector3(EnemySpawn1.be[i].transform.position.x + (d * 1.1f), -3.71f, 0);
                    if (d == -1)
                        newSprite.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    bBul.Add(newSprite);
                    bBulDir.Add(d);
                }
            }
            for (int i = 0; i < EnemySpawn1.al.Count; i++)
            {
                int x = Random.Range(1, 3);
                if(x==1)
                {
                    var newSprite = Instantiate(alphaBullet) as GameObject;
                    int d = EnemySpawn1.aEnemDir[i];
                    newSprite.transform.position = new Vector3(EnemySpawn1.al[i].transform.position.x + (d * 1.9f), -3.45f, 0);
                    if (d == -1)
                        newSprite.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    aBul.Add(newSprite);
                    aBulDir.Add(d);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < bBul.Count; i++)
        {
            bBul[i].transform.position += new Vector3(bBulDir[i]* 6.0f * Time.deltaTime, 0, 0);
        }
        for (int i = 0; i < aBul.Count; i++)
        {
            aBul[i].transform.position += new Vector3(aBulDir[i] * 6.0f * Time.deltaTime, 0, 0);
        }
    }
}
