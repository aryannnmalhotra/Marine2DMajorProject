using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platform;
    public static GameObject pright;
    public static GameObject pleft;
    public GameObject healthP;
    public GameObject immunityP;
    public GameObject bulletP;
    public GameObject bazookaP;
    public static bool ispleft ;
    public static bool ispright;
    public static int pcnt;
    public static int powerupCnt;
    public static GameObject leftP;
    public static GameObject rightP;
    public static bool isRightP;
    public static bool isLeftP;
    public static bool RInvoked;
    public static bool LInvoked;
    private void Check()
    {
        if (pcnt < 2 && Player.IsPlayerAlive == true)
            PSpawn();
    }
    private void Reset()
    {
        Player.isImmunity = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Check",5.0f,5.0f);
        pcnt = 0;
        powerupCnt = 0;
        isRightP = false;
        isLeftP = false;
        ispleft = false;
        ispright = false;
        RInvoked = false;
        LInvoked = false;
    }
    private void PCheck()
    {
        LInvoked = false;
        if (isLeftP == false && ispleft == true)
        {
            Destroy(pleft);
            if (gameObject.transform.position.x<=0.6f && Player.onPlatform == true)
                Player.health -= 15;
            pcnt--;
            ispleft = false;
        } 
    }
    private void PCheckR()
    {
        RInvoked = false;
        if (isRightP == false && ispright == true)
        {
            Destroy(pright);
            if (gameObject.transform.position.x>=1.1f && Player.onPlatform == true)
                Player.health -= 15;
            pcnt--;
            ispright = false;
        }
    }
    private void PSpawn()
    {
        if (ispleft == false)
        {
            float x = Random.Range(-6.0f,-2.8f);
            float y = Random.Range(0.75f,1.4f);
            if (LInvoked == false && Player.LInvoked == false)
            {
                pleft = Instantiate(platform) as GameObject;
                pleft.transform.position = new Vector3(x, y, 0.0f);
                ispleft = true;
                pcnt++;
                if (powerupCnt < 2)
                {
                    int i = Random.Range(1, 11);
                    if (i == 1 || i == 4 || i == 7 || i == 10)
                    {
                        leftP = Instantiate(bulletP) as GameObject;
                        leftP.transform.position = new Vector3(pleft.transform.position.x, pleft.transform.position.y + 1.75f, 0.0f);
                    }
                    else if (i == 2 || i == 3 || i == 9)
                    {
                        leftP = Instantiate(healthP) as GameObject;
                        leftP.transform.position = new Vector3(pleft.transform.position.x, pleft.transform.position.y + 1.75f, 0.0f);
                    }
                    else if (i == 5 || i == 8)
                    {
                        leftP = Instantiate(immunityP) as GameObject;
                        leftP.transform.position = new Vector3(pleft.transform.position.x, pleft.transform.position.y + 1.75f, 0.0f);
                    }
                    else
                    {
                        leftP = Instantiate(bazookaP) as GameObject;
                        leftP.transform.position = new Vector3(pleft.transform.position.x, pleft.transform.position.y + 1.75f, 0.0f);
                    }
                    powerupCnt++;
                    isLeftP = true;
                }
            }
        }
        if (ispright == false)
        {
            float x = Random.Range(3.2f, 6.1f);
            float y = Random.Range(0.75f, 1.4f);
            if (RInvoked == false && Player.RInvoked == false)
            {
                pright = Instantiate(platform) as GameObject;
                pright.transform.position = new Vector3(x, y, 0.0f);
                ispright = true;
                pcnt++;
                if (powerupCnt < 2)
                {
                    int i = Random.Range(1, 11);
                    if (i == 1 || i == 4 || i == 7)
                    {
                        rightP = Instantiate(bulletP) as GameObject;
                        rightP.transform.position = new Vector3(pright.transform.position.x, pright.transform.position.y + 1.75f, 0.0f);
                    }
                    else if (i == 2 || i == 3 || i == 9 || i == 10)
                    {
                        rightP = Instantiate(healthP) as GameObject;
                        rightP.transform.position = new Vector3(pright.transform.position.x, pright.transform.position.y + 1.75f, 0.0f);
                    }
                    else if (i == 5 || i == 8)
                    {
                        rightP = Instantiate(immunityP) as GameObject;
                        rightP.transform.position = new Vector3(pright.transform.position.x, pright.transform.position.y + 1.75f, 0.0f);
                    }
                    else
                    {
                        rightP = Instantiate(bazookaP) as GameObject;
                        rightP.transform.position = new Vector3(pright.transform.position.x, pright.transform.position.y + 1.75f, 0.0f);
                    }
                    powerupCnt++;
                    isRightP = true;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletP"))
        {
            int j = Random.Range(6, 11);
            Player.bulletCnt += j;
            if (Player.bulletCnt > 30)
                Player.bulletCnt = 30;
            if (collision.gameObject.transform.position.x >= 3.2f)
            {
                isRightP = false;
                Invoke("PCheckR", 3.5f);
                RInvoked = true;
            }
            else
            {
                isLeftP = false;
                Invoke("PCheck", 3.5f);
                LInvoked = true;
            }
            Destroy(collision.gameObject);
            powerupCnt--;
        }
        if (collision.gameObject.CompareTag("BazookaP"))
        {
            int j = Random.Range(1, 3);
            Player.bazookaCnt += j;
            if (Player.bazookaCnt > 4)
                Player.bazookaCnt = 4;
            if (collision.gameObject.transform.position.x >= 3.2f)
            {
                isRightP = false;
                Invoke("PCheckR", 3.5f);
                RInvoked = true;
            }
            else
            {
                isLeftP = false;
                Invoke("PCheck", 3.5f);
                LInvoked = true;
            }
            Destroy(collision.gameObject);
            powerupCnt--;
        }
        if (collision.gameObject.CompareTag("ImmunityP"))
        {
            Player.isImmunity = true;
            Invoke("Reset", 8.0f);
            if (collision.gameObject.transform.position.x >= 3.2f)
            {
                isRightP = false;
                Invoke("PCheckR", 3.5f);
                RInvoked = true;
            }
            else
            {
                isLeftP = false;
                Invoke("PCheck", 3.5f);
                LInvoked = true;
            }
            Destroy(collision.gameObject);
            powerupCnt--;
        }
        if (collision.gameObject.CompareTag("HealthP"))
        {
            int j = Random.Range(15, 41);
            Player.health += j;
            if (Player.health > 100)
                Player.health = 100;
            if (collision.gameObject.transform.position.x >= 3.2f)
            {
                isRightP = false;
                Invoke("PCheckR", 3.5f);
                RInvoked = true;
            }
            else
            {
                isLeftP = false;
                Invoke("PCheck", 3.5f);
                LInvoked = true;
            }
            Destroy(collision.gameObject);
            powerupCnt--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
