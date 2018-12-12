using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

	public Image hpGage;
	public Image hpContent;
	public GameObject HeadUpPosition;
	private CharacterStatus status;

	void Start () {
		status=gameObject.GetComponent<CharacterStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		hpContent.fillAmount = (float)status.HP / status.MaxHP;
		hpGage.transform.position = HeadUpPosition.transform.position;
	}
}
