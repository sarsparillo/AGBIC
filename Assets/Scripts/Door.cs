﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();		
	}
	
	public void OpenDoor() {
		anim.SetTrigger("Open");
	}
}
