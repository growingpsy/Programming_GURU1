using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove Player;
    public GameObject[] Stages;

    public AudioSource bgmPlayer;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;
    // Start is called before the first frame update

    void Start()
    {
        bgmPlayer.Play();
    }

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + (stageIndex + 1);
        }
        else
        {
            Time.timeScale = 0;

            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            ViewBtn();
        }



        totalPoint += stagePoint;
        stagePoint = 0;
               
    }


    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1,0,0,0.4f);
        }
           
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);

            Time.timeScale = 0;

            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Retry?";
            ViewBtn();


            Player.OnDie();

        
        }
    }


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (health > 1)
            {
                PlayerReposition();
            }

            HealthDown();
        }
           
    }

    void PlayerReposition()
    {
        Player.transform.position = new Vector3(0, 0, -1);
        Player.VelocityZero();
    }
 
    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void ViewBtn()
    {
        RestartBtn.SetActive(true);
    }
}
