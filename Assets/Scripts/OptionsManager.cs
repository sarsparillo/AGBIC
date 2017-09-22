using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsManager : MonoBehaviour {

	public LevelManager levelManager;

	public Slider volumeSlider;
	public Slider difficultySlider;

	//private MusicManager musicManager;

	void Start () {
	//	musicManager = GameObject.FindObjectOfType<MusicManager>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}

	public void SaveAndExit() {
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SetDifficulty(difficultySlider.value);
		levelManager.LoadLevel("01a StartMenu");
	}

	public void SetToDefaults() {
		volumeSlider.value = 0.8f;
		difficultySlider.value = 2f;
	}

	void Update() {
	//	musicManager.ChangeVolume(volumeSlider.value);
	}

}
