using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HpManager : MonoBehaviour {

	public GameObject gameObject;
	Image hpbar;
	CharacterStatus status;
	
	void Start () {
		status = gameObject.GetComponent<CharacterStatus>();
		hpbar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		hpbar.fillAmount = (float)status.HP / status.MaxHP;
	}

	public void HandleBar(float HP, float Max){
		 hpbar.fillAmount = HP/Max;
	}

	// private float Map (float value, float inMin, float inMax, float outMin,float outMax){
	// 	return (value - inMin) * (outMax - outMin) / (inMin - inMax) + outMin;
	// 	// (80 - 0) * (1 - 0) / (100 - 0) + 0
	// }
}
