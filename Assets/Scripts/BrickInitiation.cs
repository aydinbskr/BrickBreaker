using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BrickInitiation : MonoBehaviour {

	

	private static int counter;

	private string prefabPath;

	private static List<GameObject> clones = new List<GameObject>();

	private GameObject boss;

	private Vector3 pos1 = new Vector3(-1.6f,4f,0);
     private Vector3 pos2 = new Vector3(1.6f,4f,0);
	
	float[] pos_x = new float[] { -2.6f,-1.9f,-1.2f,-0.5f,0.2f,0.9f,1.6f,2.3f };
	float[] pos_y = new float[] { 4.3f,3.9f,3.5f,3.1f,2.7f,2.3f,1.9f,1.5f };
	// Use this for initialization
	void Start () {
		
		counter=0;
        DataManager.Instance.Health=0;
		DataManager.Instance.BossHealth=10;
		if(DataManager.Instance.Level==1)
		{
			prefabPath="brick";
			for (int i = 0; i < 3; i++)
        	{
				InvokeRepeating("CreateBrick", 0.2f, 3.0f);
			}	
		}
		else if(DataManager.Instance.Level==2)
		{
			prefabPath="brick2";
			for (int i = 0; i < 3; i++)
        	{
				InvokeRepeating("CreateBrick", 0.2f, 3.0f);
			}	
		}
		else if(DataManager.Instance.Level==3)
		{
			DataManager.Instance.Health=3;
			prefabPath="Boss";
			UnityEngine.Object prefab=Resources.Load(prefabPath,typeof(GameObject));
			//UnityEngine.Object prefab=AssetDatabase.LoadAssetAtPath(prefabPath,typeof(GameObject));
			boss=Instantiate(prefab, new Vector3(0f, 4f,0f), Quaternion.identity)as GameObject;
			InvokeRepeating("CreateHealth", 0.2f, 20.0f);
		}
		
	}

	private void FixedUpdate() {

			moveBlock();
			moveBoss();

			if(DataManager.Instance.BossHealth==5)
			{
				
				Sprite mySprite =Resources.Load("boss2",typeof(Sprite)) as Sprite;
				boss.GetComponent<SpriteRenderer>().sprite = mySprite;
				
			}
	}
	void moveBoss()
	{
		if(boss!=null)
		{
			
			float t = Mathf.PingPong(Time.time,2f) / 2f;
 
        	boss.transform.position = Vector3.Lerp(pos1,pos2, t);
		}
		
	}
	void moveBlock()
	{
		//level1.transform.localPosition -=new Vector3(0,0.1f,0)*Time.deltaTime;	
		for (int i = 0; i < clones.Count; i++)
        {
			if(clones[i]!=null)
			{
				
				clones[i].transform.position-=new Vector3(0,0.1f,0)*Time.deltaTime;
				if(clones[i].transform.position.y<-2.9f)
				{
					DataManager.Instance.Gameover=true;
				}
			}
		}	
	}
	void CreateBrick()
	{
		if(counter<DataManager.Instance.NumberOfBricks)
		{

			UnityEngine.Object prefab=Resources.Load(prefabPath,typeof(GameObject));
		
			int index_x = Random.Range (0, pos_x.Length);
			int index_y = Random.Range (0, pos_y.Length);
			GameObject clone=Instantiate(prefab,new Vector3(pos_x[index_x],pos_y[index_y],0),Quaternion.identity) as GameObject;
			clone.tag="brick";
			clones.Add(clone);
			counter++;
		}
    		
	}
	void CreateHealth()
	{
		float[] pos_y = new float[] {3.1f,2.7f,2.3f,1.9f,1.5f };
		int index_x = Random.Range (0, pos_x.Length);
		int index_y = Random.Range (0, pos_y.Length);
		UnityEngine.Object prefab=Resources.Load("health",typeof(GameObject));
		GameObject clone=Instantiate(prefab,new Vector3(pos_x[index_x],pos_y[index_y],0),Quaternion.identity) as GameObject;
	}
	

}
