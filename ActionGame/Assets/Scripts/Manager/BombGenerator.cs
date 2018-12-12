using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour {

	public GameObject bomb;
	float span = 0.1f;
	float delta = 0;
	float speed = -0.4f;
	LevelManager levelManager;
	public void SetParameters(float span, float speed){
		this.span = span;
		this.speed = speed;
	}

	void Awake(){
		levelManager = GameObject.Find("levelManager").GetComponent<LevelManager>();
	}
	void Update(){
		if(levelManager.level == 3){
			this.delta += Time.deltaTime;
			if(this.delta>this.span){
				this.delta = 0;
				GameObject item = Instantiate(bomb) as GameObject;
				float x = Random.Range(-75,-5);
				float y = Random.Range(-70,-7);
				item.transform.position = new Vector3 (x, 40, y);
				item.GetComponent<ItemDropController>().dropSpeed =this.speed;
			}

		}
		
	}
}
