using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
	public GameObject monster;
	public GameObject stoneMon;
	public GameObject smallDrag;
	LevelManager levelManager;

	void Awake(){
		levelManager = GameObject.Find("levelManager").GetComponent<LevelManager>();
	}
	
	void Update(){
		// if(levelManager.level == 4){
		// 	if(levelManager.level == 1){
		// 		Generate(4, 1);
		// 	}
		// 	else if(levelManager.level  == 2){
		// 		Generate(2,2);
		// 	}
		// }
	}
	
	void Generate(int count, int type){
		switch(type){
			case 1:
				monster = stoneMon;
				break;
			case 2:
				monster = smallDrag;
				break;
		}
		for(int i = 0;i<count; i++){
			GameObject item = Instantiate(monster) as GameObject;
			float x = Random.Range(25,-5);
			float y = Random.Range(-45,-15);
			item.transform.position = new Vector3 (x, 0, y);
		}
	}
}
