﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Planet : MonoBehaviour {
    [SerializeField, Range(0, 10)]
    public float coreRadius = 0;
    [SerializeField, Range(0, 10)]
    public float atmosphereRadius = 0;
    [SerializeField, Range(0, 10)]
    public float gravityRadius = 0;
	public float coreMass
	{
		get => 4 / 3 * Mathf.PI * Mathf.Pow(coreRadius, 3)* 2; 
	}

    Core core => GetComponentInChildren<Core>();
    Atmosphere atmosphere => GetComponentInChildren<Atmosphere>();
    Gravity gravity => GetComponentInChildren<Gravity>();

    void Start() {

    }

    void Update() {
#if UNITY_EDITOR
        core.transform.localScale = Vector3.one * coreRadius * 2;

        atmosphere.transform.localScale = Vector3.one * atmosphereRadius * 2;

        var shape = atmosphere.particleSystem.shape;
        shape.radiusThickness = 1 - coreRadius / atmosphereRadius * 2;

        gravity.transform.localScale = Vector3.one * gravityRadius * 2;
#endif
    }
}
