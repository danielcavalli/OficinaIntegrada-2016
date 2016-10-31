﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetResult : MonoBehaviour {
	#region Properties
	public GameObject[] Score = new GameObject[4];
	public GameObject[] players;
	public GameObject container;

	public Image winnerImage;
	public Sprite drawImage;
	public Text winnerText;

	private string winnerName;
	private bool isSet;


	#endregion

	#region Methods
	void Start () {
		container.transform.position = new Vector2 ((400 - (PlayerPrefs.GetInt ("numPlayers") * 100)) / 45, 0);	
		GameObject.Find ("AudioHandler").GetComponent<AudioBehaviour> ().audios [15].Play ();
		StartCoroutine (changeBoolInSec (isSet, 5.65f));
		isSet = false;
		SettingPlayers ();
	}

	void SettingText() {
		for (int i = 0; i < players.Length; i++) {
			if (PlayerPrefs.GetString ("Player " + (i + 1).ToString ()) != "none") {
				Score [i].GetComponent<Text> ().text = PlayerPrefs.GetInt ("RoundWinner " + (i + 1).ToString ()).ToString ();
				Score [i].GetComponent<Text> ().color = new Vector4 (255, 255, 255, 255);
			}
		}
	}

	void SettingPlayers() {
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			players[i] = GameObject.Find("Player" + ( i + 1 ).ToString() + " " + "HUD");
		}
	}

	void SettingWinner() {
		int highestScore = 0;
		for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
			if (highestScore == 0) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
				winnerImage.sprite = players [i].GetComponent<Image> ().sprite;
				winnerText.text = winnerName + " is the Winner";
			} else if (highestScore < int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
				winnerImage.sprite = players [i].GetComponent<Image> ().sprite;
				winnerText.text = winnerName + " is the Winner";
			} else if (highestScore == int.Parse (Score [i].GetComponent<Text> ().text)) {
				highestScore = int.Parse (Score [i].GetComponent<Text> ().text);
				winnerName = players [i].GetComponent<Image> ().sprite.name;
				winnerImage.sprite = players [i].GetComponent<Image> ().sprite;
				winnerImage.sprite = drawImage;
				winnerText.text = "Ohhh! It's a draw!";
			}
		}
	}

	IEnumerator changeBoolInSec(bool boolean, float sec){
		yield return new WaitForSeconds (sec);
		isSet = true;
	}

	void randomNumbers( Text text) {
		text.text = Random.Range (0, 99).ToString();
	}
				
	void Update() {
		if (isSet) {
			SettingText ();
			SettingWinner ();
		} else {
			for (int i = 0; i < PlayerPrefs.GetInt ("numPlayers"); i++) {
				randomNumbers (players[i].GetComponentInChildren<Text>());			
			}
		}
	}
	#endregion
}
