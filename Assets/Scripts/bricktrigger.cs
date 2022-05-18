using UnityEngine;
using System.Collections;

public class bricktrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="laser")
		{
			Destroy(col.gameObject);
			DataManager.Instance.BrickHitCount+=1;
			DataManager.Instance.UserScore+=100;
			Destroy(gameObject);
		}
		
		
	}
}
