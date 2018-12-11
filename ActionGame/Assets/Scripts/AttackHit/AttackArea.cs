using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour {
	CharacterStatus status;
	void Start () {
		status = transform.root.GetComponent<CharacterStatus>();
	}
	
	public class AttackInfo{
		public int attackPower;		// 공격력
		public Transform attacker;	// 공격자
	}

	AttackInfo GetAttackInfo(){
		AttackInfo attackInfo = new AttackInfo();

		// 공격력 계산
		attackInfo.attackPower = status.Power;
		attackInfo.attacker = transform.root;

		return attackInfo;
	}

	// 맞았다
	void OnTriggerEnter(Collider other){
		// 공격 당한 상대의 Damage 메세지를 보낸다.
		other.SendMessage("Damage", GetAttackInfo());
		// 공격한 대상을 저장하낟.
		status.lastAttackTarget = other.transform.root.gameObject;
	}

	//공격 판정을 유효로 한다.
	void OnAttack(){
		// collider.enabled= true;
	}

	// 공격 판정을 무효로 한다.
	void OnAttackTermination(){
		// collider.enabled = false;
	}
}
