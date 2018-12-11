﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaActivator : MonoBehaviour {

	// 공격 판정 컬라이더 배열
	Collider[] attackAreaColliders;

	void Start(){
		// 자식 오브젝트에서 AttackArea 스크립트가 추가된 오브젝트를 찾는다.
		AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
		attackAreaColliders = new Collider[attackAreas.Length];

		for(int attackAreaCnt = 0; attackAreaCnt<attackAreas.Length; attackAreaCnt++){
			// AttackArea 스크립트가 추가된 오브젝트의 컬라이더를 배열에 저장한다.
			attackAreaColliders[attackAreaCnt] = attackAreas[attackAreaCnt].GetComponent<Collider>();

			// 초깃값은 false로 한다.
			attackAreaColliders[attackAreaCnt].enabled = false;
		}
	}

	// 애니메이션 이벤트의 StartAttackHit로 컬라이더를 유효로 한다.
	void StartAttackHit(){
		foreach(Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = true;
	}

	// 애니메이션 이벤트의 EndAttackHit로 컬라이드를 무효로 한다.
	void EndAttackHit(){
		foreach(Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = false;
	}
}
