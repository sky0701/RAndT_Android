using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameObject player_runner;
    private bool play_start = false;
    private bool play_end = false;
    private float jumpForce = 500.0f;
    private bool waterdrop_trigger = false;
    private float timeCount = 0;
    public AudioClip dashAudioClip;
    public AudioClip JumpAudioClip;
    public bool isDead = false;
    private GameObject Startpanel;

    public GameObject Scorepanel;
    public GameObject Playpanel;
    public GameObject Menupanel;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        player_runner = GameObject.Find("turtle");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Startpanel = GameObject.Find("StartPage");
    }

    // Update is called once per frame
    void Update()
    {
        if (play_end)
        {
            Debug.Log("End");
            player_runner.GetComponentInChildren<Animator>().SetBool("game_end", true);
        }
        else
        {
            player_runner.GetComponentInChildren<Animator>().SetInteger("player_velocity", (int)player_runner.GetComponent<Rigidbody2D>().velocity.y);
            if (play_start && !play_end && Playpanel.activeSelf)
            {
                Debug.Log("�޷���2");
                Player_Move();
                Player_Jump();
                Player_Death();
                play_end = GM.play_end;
            }
            else if(SceneManager.GetActiveScene().name == "SampleScene")
            {
                if (GameObject.Find("BtnManager").GetComponent<BtnManager>().StartBtnClick && !Menupanel.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        play_start = true;
                    }
                        
                }
            }

            else if (SceneManager.GetActiveScene().name == "winter" || SceneManager.GetActiveScene().name == "summer")
            {
                if (!Menupanel.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        play_start = true;
                    }
                }

            }
        }
        //Player_Respawn();
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "waterDrop")
        {
            waterdrop_trigger = true;
            player_runner.GetComponent<AudioSource>().clip = dashAudioClip;
            player_runner.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }
    }
    void Player_Move()
    {
        Debug.Log("running");
        player_runner.GetComponentInChildren<Animator>().SetBool("running", true);
        if (waterdrop_trigger)
        {
            timeCount += Time.deltaTime;
            if (timeCount <= 1)
            {
                player_runner.transform.position += Vector3.right * 3f * Time.deltaTime;
                player_runner.GetComponentInChildren<Animator>().SetFloat("animation_speed", 4f);
                player_runner.GetComponentInChildren<Animator>().SetBool("runfast", true);
            }
            else
            {
                player_runner.GetComponentInChildren<Animator>().SetBool("runfast", false);
                waterdrop_trigger = false;
                timeCount = 0;
            }
        }
        else
        {
            player_runner.transform.position += Vector3.right * 0.2f * Time.deltaTime;
            player_runner.GetComponentInChildren<Animator>().SetFloat("animation_speed", 2f);
        }
        //player_runner.transform.position += Vector3.right * 0.7f * speedbytime;
        //player_runner.transform.position += Vector3.right * 0.75f * Time.deltaTime;
        //player_runner.GetComponentInChildren<Animator>().SetFloat("animation_speed", 3f);
       
    }
    void Player_Jump()
    {
        if(player_runner.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player_runner.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
                player_runner.GetComponent<AudioSource>().clip = JumpAudioClip;
                player_runner.GetComponent<AudioSource>().Play();
            }
        }
    }
    void Player_Death()
    {
        /*if (player_runner.transform.position.y < -10)
        {
            Playpanel.SetActive(false);
            Scorepanel.SetActive(true);
            isDead = true;
            play_end = true;
            Destroy(player_runner);
        }*/
    }
}
