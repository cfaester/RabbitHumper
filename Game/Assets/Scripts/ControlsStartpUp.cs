﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsStartpUp : MonoBehaviour {

	public float timer = 8.0f;
	public float screenFader = 1.0f;
	public GameObject canvas;
	private float start;
	public RawImage img;
	public Color imgColor;
	// Use this for initialization
	void Start () {
		start = Time.time;	
		img = canvas.GetComponent<RawImage> ();
		imgColor = img.color;
		img.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time <= (start+screenFader)){
			float t = (Time.time -start)/screenFader;
			if (t < 1.0f) {
				img.color = Color.Lerp (img.color, imgColor, t);
			}
		}

		if(Time.time >= (start+timer-screenFader)){
			float t = (Time.time - (start+timer-screenFader))/screenFader;
			if (t < 1.0f) {
				img.color = Color.Lerp (img.color, Color.black, t);
			}
		}	

		if (Time.time >= (start + timer)) {
			SceneManager.LoadScene(1);
		}	
	}
}
