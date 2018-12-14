using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollider : MonoBehaviour {

	public LevelManager levelManager;
	public GameObject rock1;
	public GameObject rock2;

	void OnTriggerExit(Collider other){
		if(other.tag =="Player"){
			if(levelManager.level==4){
				levelManager.GateClose();
				levelManager.levelStart=true;
			}
			else {
				if(levelManager.CheckStone()==1){
					Instantiate(rock1);
				}
				if(levelManager.CheckStone()==2){
					Instantiate(rock2);
				}
				levelManager.levelStart=true;
			}
		}
	}
}
