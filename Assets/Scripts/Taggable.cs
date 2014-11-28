﻿using UnityEngine;
using System.Collections;

public class Taggable : MonoBehaviour {

    public bool isIt;
    public bool isImmune;

    private float immunityTime = 2f; // 2 second immunity after tagging
    float immunityTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Add the time since Update was last called to the timer.
        immunityTimer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (immunityTimer >= immunityTime && isImmune)
        {
            isImmune = false;
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        // only do something if you are it
        if (isIt)
        {
            Taggable taggableCol = other.gameObject.GetComponent<Taggable>();
            bool notSelf = other.gameObject != this.gameObject;

            // If the entering collider is Taggable and not yourself and it isn't immune
            if (taggableCol != null && notSelf && !taggableCol.isImmune)
            {
                taggableCol.Tag(GetComponent<Taggable>());
            }
        }
    }

    public void Tag(Taggable tagger)
    {
        print(tagger.gameObject.name + " TAGS " + this.gameObject.name);

        tagger.isIt = false; // tagger is no longer it
        tagger.isImmune = true; // tagger gets immunity
        tagger.immunityTimer = 0f; // reset the timer
        
        isIt = true; // become it

        
    }
}
