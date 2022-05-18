using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {
	
	
	
	int lasercount=0;
	int slowcount=0;
	int fastcount=0;
	int expandcount=0;
	int shrinkcount=0;
	int deathcount=0;
	int bigcount=0;
	int timecount=0;
	public GUIStyle levelInfo;
	public GameObject laser;
	public GameObject slow;
	public GameObject fast;
	public GameObject expand;
	public GameObject shrink;
	public GameObject death;
	public GameObject bigball;
	public GameObject timeObject;
	public GUIStyle gscore;
	AudioSource[] a;
	float elapsedTime;
	void Awake()
	{
		a=gameObject.GetComponents<AudioSource>();
	}

	void Start() {
		elapsedTime=0;
		GetComponent<Rigidbody2D>().velocity=new Vector2(Random.Range(-2f,2f),Random.Range(1,4f)).normalized*DataManager.Instance.Speed;
	}

	
	void FixedUpdate()
	{
		if(DataManager.Instance.Speed==4 || DataManager.Instance.Speed==10)
		{
			GetComponent<Rigidbody2D>().velocity=GetComponent<Rigidbody2D>().velocity.normalized*DataManager.Instance.Speed;
			Invoke("normalSpeed", 8);
		}

		//player eski skorunu geçtiyse
		if(DataManager.Instance.UserScore>DataManager.Instance.BestScore)
		{
			
			PlayerPrefs.SetInt("BestScore", DataManager.Instance.UserScore);
			PlayerPrefs.Save();
			
		}
		 elapsedTime += Time.deltaTime;
	}

	void normalSpeed()
	{
		DataManager.Instance.Speed=6;
		GetComponent<Rigidbody2D>().velocity=GetComponent<Rigidbody2D>().velocity.normalized*DataManager.Instance.Speed;
	}
	void OnCollisionEnter2D(Collision2D col) 
	{
		
		if (col.gameObject.tag=="Player") 
		{
			a[1].Play();
			GetComponent<Rigidbody2D>().velocity =  new Vector2(Random.Range(-2f,2f), 
													Random.Range(1,4f)).normalized*DataManager.Instance.Speed;
		}
		if(col.gameObject.tag=="health")
		{
			a[0].Play();
			DataManager.Instance.Health+=1;
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag=="Boss")
		{
			a[0].Play();
			DataManager.Instance.BossHealth-=1;
			Instantiate(death, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);

			//BOSS HEALTH
			if(DataManager.Instance.BossHealth==0)
			{
				DataManager.Instance.UserScore+=1000;
				Destroy(col.gameObject);
			}
			

		}
		if(col.gameObject.tag=="brick")
		{
			
			//User Score
			DataManager.Instance.UserScore+=100;
			DataManager.Instance.BrickHitCount+=1;
			a[0].Play();

			
			//RAndom object uretme
			
			if(lasercount==(int)Random.Range(0,50))
			{
				 Instantiate(laser, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(slowcount==(int)Random.Range(0,50))
			{
				Instantiate(slow, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(fastcount==(int)Random.Range(0,50))
			{
				Instantiate(fast, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(expandcount==(int)Random.Range(0,50))
			{
				Instantiate(expand, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(shrinkcount==(int)Random.Range(0,50))
			{
				Instantiate(shrink, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(deathcount==(int)Random.Range(0,50))
			{
				Instantiate(death, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(bigcount==(int)Random.Range(0,50))
			{
				Instantiate(bigball, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(timecount==(int)Random.Range(0,50))
			{
				Instantiate(timeObject, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			//col.gameObject.SetActive(false);
			Destroy(col.gameObject);
			lasercount=slowcount=fastcount=expandcount=shrinkcount=deathcount=bigcount=timecount=(int)Random.Range(0,50);
		}
	}
	
	void OnGUI()
	{

		
		if(DataManager.Instance.BrickHitCount>=DataManager.Instance.NumberOfBricks )
		{
			DataManager.Instance.Level+=1;
			DataManager.Instance.BrickHitCount=0;
			GetComponent<Rigidbody2D>().velocity=Vector2.zero;
			if(DataManager.Instance.Level<3)
			{
				Invoke("next", 2);
			}
			else{
				SceneManager.LoadScene(DataManager.Instance.Level-1);
			}
			
		}
		if(elapsedTime<2f)
		{
			GUI.Label(new Rect(Screen.width/2-50f, Screen.height/2, 100, 10), "LEVEL "+DataManager.Instance.Level, levelInfo);
			
		}
		
		//SCORE
		GUI.Label(new Rect(Screen.width*.76f, Screen.height*.05f, Screen.width*.1f, 
						   Screen.height*.08f), "Score:"+DataManager.Instance.UserScore, gscore);
		
		GUI.Label(new Rect(Screen.width*.76f, Screen.height*.08f, Screen.width*.1f, 
						   Screen.height*.08f), "Level Time:"+DataManager.Instance.LevelTime, gscore);
		
		
		GUI.Label(new Rect(Screen.width*.76f, Screen.height*.02f, Screen.width*.1f, 
						   Screen.height*.08f), "Best Score:"+DataManager.Instance.BestScore, gscore);

		//Player Name
		GUI.Label(new Rect(Screen.width*.76f, Screen.height*.95f, Screen.width*.1f, 
						   Screen.height*.08f), DataManager.Instance.UserName+"'s Game", gscore);

		if(DataManager.Instance.Level==3)
		{
			//Health
		GUI.Label(new Rect(Screen.width*.76f, Screen.height*.12f, Screen.width*.1f, 
						   Screen.height*.08f), "Health:"+DataManager.Instance.Health, gscore);

		//Boss
		GUI.Label(new Rect(Screen.width*.76f, Screen.height*.16f, Screen.width*.1f, 
						   Screen.height*.08f), "Boss:"+DataManager.Instance.BossHealth, gscore);
		}
		
	}


	void next()
	{

		if(DataManager.Instance.Level==2)
		{
			DataManager.Instance.NumberOfBricks=36;
			DataManager.Instance.BrickHitCount=0;
			DataManager.Instance.RightBorder=2.3f;
			
		}
		if(DataManager.Instance.Level==1)
		{
			DataManager.Instance.NumberOfBricks=33;
			DataManager.Instance.BrickHitCount=0;
			
		}
		
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(DataManager.Instance.Level);
	}
}