using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour {

	public float dropSpeed;
	//public GameObject player;

	void Update () {
		transform.Translate(0,this.dropSpeed,0);
		if(transform.position.y<-1.0f){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		// 공격 당한 상대의 Damage 메세지를 보낸다.
		if(other.tag == "Player"){
			CharacterStatus status = other.gameObject.GetComponent<CharacterStatus>();
			status.HP -=20;
		}
	}
}
