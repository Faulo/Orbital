﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private new Rigidbody2D rigidbody => GetComponent<Rigidbody2D>();
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void LaunchTowards(Vector3 target, float speed) {
        //transform.LookAt(target, Vector3.forward);
        rigidbody.AddForce((target - transform.position).normalized * speed, ForceMode2D.Impulse);
    }
}
