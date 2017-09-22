using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Text healthText;
	public Slider healthBar;

	void Start () {
		UpdateHealth(100f);
	}

	public void UpdateHealth(float health) {
		healthText.text = health.ToString();
		healthBar.value = health;
	}

}
