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
}
