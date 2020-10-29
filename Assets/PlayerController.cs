using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;

    bool carpa;


    float currentTime;

    bool invincible;

    public GameObject fireShield;

    [SerializeField]
    AudioClip win, death, idestory, destory, bounce;


    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public PlayerState playerstate = PlayerState.Prepare;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    


    // Update is called once per frame
    void Update()
    {
        if (playerstate == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                carpa = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                carpa = false;
            }


            if (invincible)
            {
                currentTime -= Time.deltaTime * .35f;
                if (!fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(true);
                }
            }
            else
            {
                if (fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(false);
                }

                if (carpa)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }


            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                Debug.Log("invincible");
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                Debug.Log("-----------");
            }
        }


        if (playerstate == PlayerState.Prepare)
        {
            if (Input.GetMouseButton(0))
            {
                playerstate = PlayerState.Playing;
            }
        }

        if (playerstate == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }





    }


    public void shatterObstacles()
    {


        if (invincible)
        {
            ScoreManager.intance.addScore(1);
        }
        else
        {
            ScoreManager.intance.addScore(2);
        }
        
    }



    private void FixedUpdate()
    {
        //

        if (playerstate == PlayerState.Playing)
        {
            if (carpa)
            {

                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
                

            }
        }

       


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!carpa)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                   //Destroy(collision.transform.parent.gameObject);
                   collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacles();
                    SoundManager.instance.playSoundFX(idestory, 0.5f);
                }
               
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacles();
                    SoundManager.instance.playSoundFX(destory, 0.5f);

                }
                else if (collision.gameObject.tag == "plane")
                {
                    Debug.Log("GameOver");
                    ScoreManager.intance.ResetScore();
                    SoundManager.instance.playSoundFX(death, 0.5f);
                 
                }
            }


          
           
        }  
        
        if(collision.gameObject.tag=="Finish" && playerstate == PlayerState.Playing)
        {
            playerstate = PlayerState.Finish;
            SoundManager.instance.playSoundFX(win, 0.5f);
        }


    }


    private void OnCollisionStay(Collision collision)
    {
        if (!carpa || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.instance.playSoundFX(bounce, 0.5f);

        }
    }



}
