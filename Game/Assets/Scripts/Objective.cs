﻿using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	public GameObject kid;
	public float duration = 5.0f;
	public int numberOfKids = 6;

	public bool objActive = false;
	private bool player1Active = false;
	private bool player2Active = false;
	private bool player1Using = false;
	private bool player2Using = false;

	private float startTimer;
	private float durationPerKid;
	private float kidTimer;

	private GameObject player1;
	private GameObject player2;


	// Use this for initialization
	void Start () {	
		durationPerKid = duration / numberOfKids;
		objActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!objActive && (player1Active || player2Active)) {
			if (player1Active && Input.GetButton ("B1")) {
				player1Using = true;
				startObj ();
			} 
			else if (player2Active && Input.GetButton ("B2")) {
				player2Using = true;
				startObj ();
			} 
		}

		else if (objActive) {
			float curTime = Time.time;

			if (player1Using && Input.GetButton ("B1")) {
				if (curTime >= kidTimer) {
					spawnKid (player1);
				}
			} 
			else if (player2Using && Input.GetButton ("B2")) {
				if (curTime >= kidTimer) {
					spawnKid (player2);
				}
			} 
			else {
				DestroyObjective ();
			}

			if (curTime >= startTimer + duration) {
				DestroyObjective ();
			}
		}

		//If the objective was being used but the player isnt holding the button anymore.
		if (!objActive && (player1Using || player2Using)) {
			DestroyObjective ();
		}
	}

	void startObj(){
		objActive = true;
		startTimer = Time.time;
		kidTimer = startTimer + durationPerKid;
	}

	void spawnKid(GameObject player){
		GameManger.instance.addScore(player.GetComponent<PlayerMovement> ().getPlayerNumb());
		kidTimer += durationPerKid;
		GameObject newKid = Instantiate (kid, gameObject.transform.position, Quaternion.identity) as GameObject;
		newKid.GetComponent<Kid> ().setColor (player.GetComponent<MeshRenderer> ().material.color);
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player1")) {
			player1Active = true;
			player1 = other.gameObject;
		}
		if(other.CompareTag("Player2")){
			player2Active = true;
			player2 = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player1")) {
			player1Active = false;
			player1Using = false;
		}
		if (other.CompareTag ("Player2")) {
			player2Active = false;
			player2Using = false;
		}
	}

	void DestroyObjective(){
		objActive = false;
		player1Active = false;
		player2Active = false;
		player1Using = false;
		player2Using = false;
		ObjectiveSpawner.instance.setObjAct ();
		Destroy (gameObject);
	}
}