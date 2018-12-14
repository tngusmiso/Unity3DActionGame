using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	public int level = 1;
	public int kill = 0;
	public int condition = 3;

	float span =1.0f;
	float delta = 0;

	public int levelUpFrom = 0;
	public bool levelStart = true;

	GameObject rock1;
	GameObject rock2;
	
	public GameObject gate;
	public Text Kill;
	Animator gateAnimator;
	
	
	void Start(){
		rock1 = GameObject.Find("Rock1");
		rock2 = GameObject.Find("Rock2");
		
		gateAnimator = gate.GetComponent<Animator>();
	}
	void Update(){
		SetText();
		if(level == 5 ){

		}
		if(level == 3 && levelStart){
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
		case 4:
			kill = 0;
			condition = 1;
			break;
		case 5:
			kill = 0;
			condition = 300;
			break;
		}
	}

	void LeveUp(){
		switch(level){
		case 1:
			levelStart=false;
			levelUpFrom = 1;
			Destroy(rock1,3);
			Invoke("CompletedLeveUP",4);
			break;
		case 2:
			levelStart=false;
			levelUpFrom = 2;
			Destroy(rock2,3);
			Invoke("CompletedLeveUP",4);
			break;
		case 3:
			levelStart=false;
			levelUpFrom = 3;
			Invoke("GateOpen",3);
			Invoke("CompletedLeveUP",5);
			break;
		}
		level += 1;
		UpdateCondition();
	}
	
	void StoneOpen(){
		rock1.transform.Translate(rock1.transform.position.x, 10, rock1.transform.position.z);
		rock2.transform.Translate(rock1.transform.position.x, 10, rock2.transform.position.z);
	}
	public int CheckStone(){
		if(rock2==null){
			return 2;
		}
		if(rock1==null){
			return 1;
		}
		else {return 0;}
	}
	void GateOpen(){
		gateAnimator.SetBool("Opening",true);
	}

	public void GateClose(){
		gateAnimator.SetBool("Opening",false);
	}

	void CompletedLeveUP(){
		kill = 0;
		levelUpFrom=0;
	}

	void SetText(){
		if(level == 1&&levelStart){
			Kill.text = "Kill: " + kill +" / 3";
		}
		if(level == 2&&levelStart){
			Kill.text = "Kill: " + kill +" / 5";
		}
		if(level == 3&&levelStart){
			Kill.text = "Second: " + kill +" / 20";
		}
		if(level == 4&&levelStart){
			Kill.text = "Mission: Kill Dragon";
		}
	}
	
}
