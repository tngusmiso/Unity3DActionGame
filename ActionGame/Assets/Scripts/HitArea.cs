using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {
	void Damage(AttackArea.AttackInfo attackinfo){
		transform.root.SendMessage("Damage",attackinfo);
	}
}
