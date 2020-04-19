using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn1 : MonoBehaviour
{
    public GameObject beta;
    public GameObject alpha;
    public GameObject mar;
    private bool isOver;
    public static int bcnt ;
    public static int acnt ;
    public static int tcnt ;
    public static List<GameObject> be = new List<GameObject>();
    public static List<GameObject> al = new List<GameObject>();
    public static List<int> bhlist = new List<int>();
    public static List<int> ahlist = new List<int>();
    public static List<int> enemDir = new List<int>();
    public static List<int> aEnemDir = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        bcnt = 0;
        acnt = 0;
        tcnt = 0;
        InvokeRepeating("Spawn", 0.0f, 5.0f);
    }
    void Spawn()
    {
        if (tcnt < 5 && Player.IsPlayerAlive == true)
        {
            int i = Random.Range(1, 7);
            if (i != 3 && i!=4 && bcnt < 4)
            {
                {
                    float x;
                    do
                    {
                        isOver = false;
                        x = Random.Range(-8.0f, 8.0f);
                        if (x > mar.transform.position.x)
                        {
                            if (x - mar.transform.position.x < 2.0f)
                            {
                                isOver = true;
                                //Debug.Log("Cannot spawn, coinciding with marine");
                            }
                        }
                        else
                        {
                            if (mar.transform.position.x - x < 2.0f)
                            {
                                isOver = true;
                                //Debug.Log("Cannot spawn, coinciding with marine");
                            }
                        }
                        for (int j = 0; j < be.Count; j++)
                        {
                            if (x > be[j].transform.position.x)
                            {
                                if (x - be[j].transform.position.x < 2.0f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn, coinciding with betaTerror");
                                }
                            }
                            else
                            {
                                if (be[j].transform.position.x-x < 2.0f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn, coinciding with betaTerror");
                                }
                            }
                        }
                        for (int j = 0; j < al.Count; j++)
                        {
                            if (x > al[j].transform.position.x)
                            {
                                if (x - al[j].transform.position.x < 2.3f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn, coinciding with alphaTerror");
                                }
                            }
                            else
                            {
                                if  (al[j].transform.position.x-x < 2.3f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn, coinciding with alphaTerror");
                                }
                            }
                        }
                    } while (isOver == true);
                    GameObject newSprite = Instantiate(beta) as GameObject;
                    newSprite.transform.position = new Vector3(x, -2.89f, 0.0f);
                    if (mar.transform.position.x < newSprite.transform.position.x)
                    {
                        newSprite.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                        enemDir.Add(-1);
                    }
                    else
                        enemDir.Add(1);
                    be.Add(newSprite);
                    bhlist.Add(0);
                    tcnt++;
                    bcnt++;
                }
            }
            else if ((i == 3 && acnt < 1)||(i==4&&acnt<1))
            {

                {
                    float x;
                    do
                    {
                        isOver = false;
                        x = Random.Range(-7.4f, 7.4f);
                        if (x > mar.transform.position.x)
                        {
                            if (x - mar.transform.position.x < 2.4f)
                            {
                                isOver = true;
                                //Debug.Log("Cannot spawn alphaTerror, coinciding with marine");
                            }
                        }
                        else
                        {
                            if (mar.transform.position.x-x < 2.4f)
                            {
                                isOver = true;
                                //Debug.Log("Cannot spawn alphaTerror, coinciding with marine");
                            }
                        }
                        for (int j = 0; j < be.Count; j++)
                        {
                            if (x > be[j].transform.position.x)
                            {
                                if (x - be[j].transform.position.x < 2.4f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn alphaTerror, coinciding with betaTerror");
                                }
                            }
                            else
                            {
                                if (be[j].transform.position.x-x < 2.4f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn alphaTerror, coinciding with betaTerror");
                                }
                            }
                        }
                        for (int j = 0; j < al.Count; j++)
                        {
                            if (x > al[j].transform.position.x)
                            {
                                if (x - al[j].transform.position.x < 2.5f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn alphaTerror, coinciding with alphaTerror");
                                }
                            }
                            else
                            {
                                if ( al[j].transform.position.x-x < 2.5f)
                                {
                                    isOver = true;
                                    //Debug.Log("Cannot spawn alphaTerror, coinciding with alphaTerror");
                                }
                            }
                        }
                    } while (isOver == true);
                    GameObject newSprite = Instantiate(alpha) as GameObject;
                    newSprite.transform.position = new Vector3(x, -1.98f, 0.0f);
                    if (mar.transform.position.x < newSprite.transform.position.x)
                    {
                        newSprite.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                        aEnemDir.Add(-1);
                    }
                    else
                        aEnemDir.Add(1);
                    al.Add(newSprite);
                    ahlist.Add(0);
                    tcnt++;
                    acnt++;
                }
            }
            else
                return;
        }
    }
    // Update is called once per frame
    void Update()
    {

        for (int j = 0; j < be.Count; j++)
        {
            if (mar.transform.position.x < be[j].transform.position.x)
            {
                be[j].transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                enemDir[j] = -1;
            }
            else
            {
                be[j].transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                enemDir[j] = 1;
            }

        }
        for (int j = 0; j < al.Count; j++)
        {
            if (mar.transform.position.x < al[j].transform.position.x)
            {
                al[j].transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                aEnemDir[j] = -1;
            }
            else
            {
                al[j].transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                aEnemDir[j] = 1;
            }

        }
    }
}
