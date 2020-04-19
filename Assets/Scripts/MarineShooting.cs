using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bazooka;
    public GameObject marin;
    public struct Direction
    {
        public int direction;
    };
    public static List<Direction> adir = new List<Direction>();
    public static List<Direction> bdir = new List<Direction>();
    public static List<GameObject> bullets = new List<GameObject>();
    public static List<GameObject> bazookas = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.IsPlayerAlive == true)
        {
            if (Input.GetKeyDown(KeyCode.J) && Player.bulletCnt > 0)
            {
                if (Player.isPlayerRight == true)
                {
                    Direction newObj = new Direction();
                    newObj.direction = 1;
                    bdir.Add(newObj);
                    var newSprite = Instantiate(bullet) as GameObject;
                    newSprite.transform.position = new Vector3(marin.transform.position.x + 1.5f, marin.transform.position.y - 0.31f, 0.0f);
                    bullets.Add(newSprite);
                }
                else
                {
                    Direction newObj = new Direction();
                    newObj.direction = -1;
                    bdir.Add(newObj);
                    var newSprite = Instantiate(bullet) as GameObject;
                    newSprite.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    newSprite.transform.position = new Vector3(marin.transform.position.x - 1.5f, marin.transform.position.y - 0.31f, 0.0f);
                    bullets.Add(newSprite);
                }
                Player.bulletCnt--;
            }
        }
        if (Player.IsPlayerAlive == true)
        {
            if (Input.GetKeyDown(KeyCode.K) && Player.bazookaCnt > 0)
            {
                if (Player.isPlayerRight == true)
                {
                    Direction newObj = new Direction();
                    newObj.direction = 1;
                    adir.Add(newObj);
                    var newSprite = Instantiate(bazooka) as GameObject;
                    newSprite.transform.position = new Vector3(marin.transform.position.x + 2.15f, marin.transform.position.y - 0.1f, 0.0f);
                    bazookas.Add(newSprite);
                }
                else
                {
                    Direction newObj = new Direction();
                    newObj.direction = -1;
                    adir.Add(newObj);
                    var newSprite = Instantiate(bazooka) as GameObject;
                    newSprite.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    newSprite.transform.position = new Vector3(marin.transform.position.x - 2.15f, marin.transform.position.y - 0.1f, 0.0f);
                    bazookas.Add(newSprite);
                }
                Player.bazookaCnt--;
            }
            for (int j = 0; j < bazookas.Count; j++)
            {
                bazookas[j].transform.position += new Vector3(adir[j].direction * 8.0f * Time.deltaTime, 0, 0);
            }
            for (int j = 0; j < bullets.Count; j++)
            {
                bullets[j].transform.position += new Vector3(bdir[j].direction * 8.0f * Time.deltaTime, 0, 0);
            }
        }
    }
}
