﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using KeyboardInput;

public class SliderBehaviour : MonoBehaviour {


	public GameObject soundSlider;

	private string setSlider;

	void Awake() {

		if (PlayerPrefs.HasKey ("SoundValue")) {
			soundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("SoundValue");
		} else {	
			soundSlider.GetComponent<Slider> ().value = 1;
		}
	}

	void Start() {
		setSlider = "sound";	
	}

	void setPosition() {
		float keyboard = Input.GetAxisRaw ("SliderV");
		float Xbox = XCI.GetAxisRaw (XboxAxis.LeftStickY);

		if (keyboard == 1 || Xbox == 1) {
			Setting("sound");
		}

	}

	void Setting(string position){

		if (position == "sound") {
			transform.position = new Vector3(0, 0, 0);
			setSlider = position;
		}

	}

	void setValue(){
		float keyboard = Input.GetAxisRaw ("SliderH") * 0.01f;
		float Xbox = XCI.GetAxisRaw (XboxAxis.LeftStickX) * 0.01f;
		
		if (setSlider == "sound") {
			soundSlider.GetComponent<Slider>().value += (keyboard + Xbox);
			AudioListener.volume = soundSlider.GetComponent<Slider>().value;
			PlayerPrefs.SetFloat("SoundValue", soundSlider.GetComponent<Slider>().value);
		}
	}

	void Update () {
		setPosition ();
		setValue ();
	}
}