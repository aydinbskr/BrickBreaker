﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

   
    public Rigidbody2D laser;
    bool isLaserBrick = false;
    float nextlaunch = 0f;
    
    
    Transform big;
    
    public GUIStyle replay;
    public GUIStyle home;
    public GUIStyle gameOverBG;
    public GUIStyle score;
    public GUIStyle title;

    float elapsedTime=0;

    void Start()
    {
        big = GameObject.FindWithTag("ball").GetComponent<Transform>();
        
        DataManager.Instance.Gameover=false;
        DataManager.Instance.LevelTime=30;
        if(DataManager.Instance.Level==3)
        {
            transform.localScale = new Vector2(.5f, transform.localScale.y);
            DataManager.Instance.RightBorder=2.05f;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        //decrease time per second
        if (elapsedTime >= 2f) {
            elapsedTime=0f;
            DataManager.Instance.LevelTime-=1;
        }
        if(DataManager.Instance.LevelTime<=0)
        {
            DataManager.Instance.Gameover=true;
        }
        if (Time.timeScale > 0)
        {
            //player movement
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float xPosition = Mathf.Clamp(ray.GetPoint(1).x, -DataManager.Instance.RightBorder, DataManager.Instance.RightBorder);
            Vector2 pedalPozisyonu = new Vector2(xPosition, transform.position.y);
            transform.position = pedalPozisyonu;
        }
        //laser instantiation
        if (isLaserBrick && Time.time >= nextlaunch)
        {
            nextlaunch = Time.time + .2f;
            GetComponent<AudioSource>().Play();
            Rigidbody2D laserclone = (Rigidbody2D)Instantiate(laser, new Vector3(transform.position.x + .5f, -2.8f), Quaternion.identity);
            laserclone.velocity = new Vector2(transform.position.x + .5f, 10) * 3;
            Rigidbody2D laserclone1 = (Rigidbody2D)Instantiate(laser, new Vector3(transform.position.x - .5f, -2.8f), Quaternion.identity);
            laserclone1.velocity = new Vector2(transform.position.x - .5f, 10) * 3;
            Invoke("laserCheck", 5);
        }

    }


    void laserCheck()
    {
        isLaserBrick = false;
        GetComponent<AudioSource>().Stop();
    }

   
    //power functionalities
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "laserbrick")
        {
            Destroy(col.gameObject);
            isLaserBrick = true;
        }
        if (col.gameObject.tag == "fastbrick")
        {
            Destroy(col.gameObject);
            DataManager.Instance.Speed=10;  
        }
        if (col.gameObject.tag == "slowbrick")
        {
            Destroy(col.gameObject);
            DataManager.Instance.Speed=4;
        }
        if (col.gameObject.tag == "death")
        {
            DataManager.Instance.Health-=1;
            if(DataManager.Instance.Health<0)
            {
                DataManager.Instance.Gameover=true;
                DataManager.Instance.LevelTime=40;
            }
            
            Destroy(col.gameObject);    
        }
       
        if (col.gameObject.tag == "expand")
        {
            transform.localScale = new Vector2(.45f, transform.localScale.y);
            DataManager.Instance.RightBorder-=0.20f;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "shrink")
        {
            transform.localScale = new Vector2(.25f, transform.localScale.y);
            DataManager.Instance.RightBorder+=0.25f;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "bigball")
        {
            big.localScale = new Vector2(big.localScale.x * 6 / 2, big.localScale.y * 6 / 2);
            Destroy(col.gameObject);
        }
         if (col.gameObject.tag == "time")
        {
            DataManager.Instance.LevelTime+=10;
            Destroy(col.gameObject);
        }
    }

    //gameover gui (also attached on bottom script)
    void OnGUI()
    {
        
        if ( DataManager.Instance.Gameover)
        {
			Time.timeScale=0;

            if (GUI.Button(new Rect(Screen.width * .1f, Screen.height * .6f, Screen.width * .38f, Screen.height * .08f), "", replay))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               
                DataManager.Instance.BrickHitCount=0;
                DataManager.Instance.UserScore=0;
                DataManager.Instance.RightBorder=2.3f;
                DataManager.Instance.Speed=6;
               
               
            }
            if (GUI.Button(new Rect(Screen.width * .52f, Screen.height * .6f, Screen.width * .38f, Screen.height * .08f), "", home))
            {
               
                DataManager.Instance.UserScore=0;
				DataManager.Instance.BrickHitCount=0;
                DataManager.Instance.RightBorder=2.3f;
				DataManager.Instance.Speed=6;
                SceneManager.LoadScene(0);
            }
            GUI.Box(new Rect(Screen.width * .1f, Screen.height * .2f, Screen.width * .8f, Screen.height * .35f), "", gameOverBG);

            GUI.Label(new Rect(Screen.width * .45f, Screen.height * .43f, Screen.width * .1f, Screen.height * .08f),DataManager.Instance.UserScore.ToString(), score);
            //balls.velocity = Vector2.zero.normalized;

        }
    }

}