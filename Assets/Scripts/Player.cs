using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D marine;
    public static bool IsPlayerAlive;
    public static int health;
    public float speed;
    public static int score;
    public static int highScore;
    public static int bazookaCnt;
    public static int bulletCnt;
    public static bool isPlayerRight;
    public static bool isPlayerGrounded;
    public static bool onPlatform;
    public static bool isImmunity;
    public Text healthText;
    public static string immunity;
    public Text immunityText;
    public Text scoreText;
    public Text bazookaText;
    public Text bulletText;
    public int airCnt;
    private int checker;
    public static bool RInvoked;
    public static bool LInvoked;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerRight = true;
        score = 0;
        onPlatform = false;
        health = 100;
        IsPlayerAlive = true;
        bulletCnt = 20;
        bazookaCnt = 2;
        isImmunity = false;
        isPlayerGrounded = true;
        immunity = "Not Immune";
        airCnt = 0;
        InvokeRepeating("AirTime", 0.0f, 0.1f);
        checker = 40;
        RInvoked = false;
        LInvoked = false;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            isPlayerGrounded = false;
        }
        /*if (collision.gameObject.CompareTag("Platform") && (marine.transform.position.y - collision.gameObject.transform.position.y < 2.09))
        {
            Debug.Log("OffPlatform");
            onPlatform = false;
        }*/
    }
    private void PDestroy()
    {
        if (gameObject.transform.position.x >= 1.1f && onPlatform == true)
            health -= 15;
            Destroy(PlatformSpawn.pright);
            PlatformSpawn.ispright = false;
            RInvoked = false;
            if (PlatformSpawn.isRightP == true)
            {
                Destroy(PlatformSpawn.rightP);
                PlatformSpawn.powerupCnt--;
            }
            PlatformSpawn.pcnt--;
    }
    private void PDestroyL() 
    {
        if (gameObject.transform.position.x <= 0.6f && onPlatform == true)
            health -= 15;
        Destroy(PlatformSpawn.pleft);
                PlatformSpawn.ispleft = false;
        LInvoked = false;
                if (PlatformSpawn.isLeftP == true)
                {
                    Destroy(PlatformSpawn.leftP);
                    PlatformSpawn.powerupCnt--;
                }
                PlatformSpawn.pcnt--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            for (int i = 0; i < EnemyShooting.bBul.Count; i++)
            {
                if (EnemyShooting.bBul[i] == collision.gameObject)
                {
                    EnemyShooting.bBul.RemoveAt(i);
                    EnemyShooting.bBulDir.RemoveAt(i);
                    Destroy(collision.gameObject);
                }
            }
        }
        if (collision.gameObject.CompareTag("EnemyBazooka"))
        {
            for (int i = 0; i < EnemyShooting.aBul.Count; i++)
            {
                if (EnemyShooting.aBul[i] == collision.gameObject)
                {
                    EnemyShooting.aBul.RemoveAt(i);
                    EnemyShooting.aBulDir.RemoveAt(i);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            isPlayerGrounded = true;
        }
        if (collision.gameObject.CompareTag("Platform") && (marine.transform.position.y > collision.gameObject.transform.position.y))
        {
            if (collision.gameObject.transform.position.x >= 3.2f)
            {
                Invoke("PDestroy", 3.5f);
                RInvoked = true;
            }
            else
            {
                Invoke("PDestroyL", 3.5f);
                LInvoked = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && (marine.transform.position.y - collision.gameObject.transform.position.y > 2.09))
        {
            onPlatform = true;
        }
        else
            onPlatform = false;
    }
    private void AirTime()
    {
        if(isPlayerGrounded==false && onPlatform==false)
            airCnt++;
        if (airCnt % checker == 0 && airCnt != 0)
        {
            health -= 5;
            checker += 40;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
            health = 0;
        if (isImmunity == true)
            immunity = "Immune";
        if (isImmunity == false)
            immunity = "Not Immune";
        immunityText.text = immunity.ToString();
        scoreText.text = score.ToString();
        healthText.text = health.ToString();
        bazookaText.text = bazookaCnt.ToString();
        bulletText.text = bulletCnt.ToString();
        if (IsPlayerAlive == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (isPlayerRight == true)
                {
                    marine.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    isPlayerRight = false;
                }
                else
                    marine.transform.position += new Vector3(-(speed * Time.deltaTime), 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {

                if (isPlayerRight == false)
                {
                    marine.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                    isPlayerRight = true;
                }
                else
                    marine.transform.position += new Vector3((speed * Time.deltaTime), 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                marine.velocity = Vector2.zero;
                marine.AddForce(new Vector2(0, speed * 10));
            }
            if (health <= 0)
            {
                IsPlayerAlive = false;
                Destroy(PlatformSpawn.pleft);
                Destroy(PlatformSpawn.pright);
                Destroy(PlatformSpawn.leftP);
                Destroy(PlatformSpawn.rightP);
                for(int k = MarineShooting.bullets.Count-1; k >= 0; k--)
                {
                    
                    var gameobject = MarineShooting.bullets[k];
                    MarineShooting.bullets.RemoveAt(k);
                    Destroy(gameobject);
                }
                for (int k = MarineShooting.bazookas.Count - 1; k >= 0; k--)
                {

                    var gameobject = MarineShooting.bazookas[k];
                    MarineShooting.bazookas.RemoveAt(k);
                    Destroy(gameobject);
                }
                for (int k = MarineShooting.bdir.Count - 1; k >= 0; k--)
                {

                    MarineShooting.bdir.RemoveAt(k);
                }
                for (int k = MarineShooting.adir.Count - 1; k >= 0; k--)
                {

                    MarineShooting.adir.RemoveAt(k);
                }
                for (int k = EnemySpawn1.be.Count-1; k >=0; k--)
                {
                    var gameobject = EnemySpawn1.be[k];
                    EnemySpawn1.be.RemoveAt(k);
                    Destroy(gameobject);
                }
                for (int k = EnemySpawn1.al.Count-1; k >=0; k--)
                {
                    var gameobject = EnemySpawn1.al[k];
                    EnemySpawn1.al.RemoveAt(k);
                    Destroy(gameobject);
                }
                for (int k = EnemySpawn1.ahlist.Count - 1; k >= 0; k--)
                {
                    EnemySpawn1.ahlist.RemoveAt(k);
                }
                for (int k = EnemySpawn1.bhlist.Count - 1; k >= 0; k--)
                {
                    EnemySpawn1.bhlist.RemoveAt(k);
                }
                for (int k = EnemySpawn1.enemDir.Count - 1; k >= 0; k--)
                {
                    EnemySpawn1.enemDir.RemoveAt(k);
                }
                for (int k = EnemySpawn1.aEnemDir.Count - 1; k >= 0; k--)
                {
                    EnemySpawn1.aEnemDir.RemoveAt(k);
                }
                for (int k = EnemyShooting.bBul.Count - 1; k >= 0; k--)
                {
                    var gameobject = EnemyShooting.bBul[k];
                    EnemyShooting.bBul.RemoveAt(k);
                    Destroy(gameobject);
                }
                for (int k = EnemyShooting.aBul.Count - 1; k >= 0; k--)
                {
                    var gameobject = EnemyShooting.aBul[k];
                    EnemyShooting.aBul.RemoveAt(k);
                    Destroy(gameobject);
                }
                for (int k = EnemyShooting.bBulDir.Count - 1; k >= 0; k--)
                {
                    EnemyShooting.bBulDir.RemoveAt(k);
                }
                for (int k = EnemyShooting.aBulDir.Count - 1; k >= 0; k--)
                {
                    EnemyShooting.aBulDir.RemoveAt(k);
                }
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("HighScore", highScore);
                }
                SceneManager.LoadScene("GameOver");
            }
            marine.transform.eulerAngles += new Vector3(0,0,-(marine.transform.rotation.z));
        }
    }
}
