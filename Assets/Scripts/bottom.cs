using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class bottom : MonoBehaviour {

	public GUIStyle replay;
	public GUIStyle home;
	public GUIStyle gameOver;
	public GUIStyle score;
	public GUIStyle title;
	public GUIStyle victory;

	bool isBallDrop=false;
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="ball")
		{
			print("top dustu");
			isBallDrop=true;
			
		}
	
		
		Destroy(col.gameObject);
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		
		Destroy(col.gameObject,1f);
		
	}

	void OnGUI()
	{
		 GUI.depth = 0;
		 
		if(isBallDrop)
		{
			Time.timeScale=0;
			DataManager.Instance.LevelTime=40;
			if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.6f, Screen.width*.38f, Screen.height*.08f), "", replay))
			{
				
				Time.timeScale=1;
			
				DataManager.Instance.RightBorder=2.3f;
				DataManager.Instance.BrickHitCount=0;
				DataManager.Instance.UserScore=0;
				DataManager.Instance.Speed=6;
				
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

				
			}
			if(GUI.Button(new Rect(Screen.width*.52f, Screen.height*.6f, Screen.width*.38f, Screen.height*.08f), "", home))
			{
				DataManager.Instance.UserScore=0;
				DataManager.Instance.BrickHitCount=0;
                DataManager.Instance.RightBorder=2.3f;
				DataManager.Instance.Speed=6;
				SceneManager.LoadScene(0);
			}
			//Gameover background image
			if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.08f, Screen.width*.8f, Screen.height*.50f), "", gameOver))
			{
				
			}
			
			GUI.Label(new Rect(Screen.width*.45f, Screen.height*.42f, Screen.width*.1f, Screen.height*.08f), DataManager.Instance.UserScore.ToString(), score);

			
		}
		//VICTORY
		else if(DataManager.Instance.BossHealth==0)
		{
			Time.timeScale=0;
			
			if(GUI.Button(new Rect(Screen.width*.25f, Screen.height*.08f, Screen.width*.5f, Screen.height*1f), "", victory))
			{
				DataManager.Instance.UserScore=0;
				DataManager.Instance.BrickHitCount=0;
                DataManager.Instance.RightBorder=2.3f;
				DataManager.Instance.Speed=6;
				SceneManager.LoadScene(0);
			}
			GUI.Label(new Rect(Screen.width*.37f, Screen.height*.48f, Screen.width*.1f, Screen.height*.08f), "Game is Completed\n     Click here", title);
		}

	}
}