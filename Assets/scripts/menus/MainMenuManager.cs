﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnPlay(){
		SceneManager.LoadScene ("grid");
	}

    public void OnCredits()
    {
        SceneManager.LoadScene("credits");
    }
}
