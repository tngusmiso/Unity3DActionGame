using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public int level = 1;
	public int kill = 0;
	public int condition = 1;

	float span =1.0f;
	float delta = 0;

	public int levelUpFrom = 0;

	GameObject rock1;
	GameObject rock2;
	public GameObject gate;
	Animator animator;
	
	
	void Start(){
		rock1 = GameObject.Find("Rock1");
		rock2 = GameObject.Find("Rock2");
		
		animator = gate.GetComponent<Animator>();
	}
	void Update(){
		if(level == 3){
			this.delta +=Time.deltaTime;
			if(this.delta > this.span){
				this.delta = 0;
				kill+=1;
			}
		}
		if(kill>=condition){
			LeveUp();
		}
	}

	void UpdateCondition(){
		switch(level){
		case 1:
			kill = 0;
			condition = 1;
			break;
		case 2:
			kill = 0;
			condition = 1;
			break;
		case 3:
			kill = 0;
			condition = 10;
			break;
		}
	}

	void LeveUp(){
		switch(level){
		case 1:
			levelUpFrom = 1;
			Destroy(rock1,3);
			Invoke("CompletedLeveUP",4);
			break;
		case 2:
			levelUpFrom = 2;
			Destroy(rock2,3);
			Invoke("CompletedLeveUP",4);
			break;
		case 3:
			levelUpFrom = 3;
			Invoke("GateOpen",3);
			Invoke("CompletedLeveUP",5);
			break;

		}
		level += 1;
		UpdateCondition();
	}
	
	void GateOpen(){
		animator.SetBool("Opening",true);
	}

	void GateClose(){
		animator.SetBool("Opening",false);
	}
	void CompletedLeveUP(){
		kill = 0;
		levelUpFrom=0;
	}
}
