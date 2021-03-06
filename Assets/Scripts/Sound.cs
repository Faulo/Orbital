﻿using UnityEngine;


[System.Serializable]
public class Sound {
    public string name;

    public AudioClip clip = default;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(0f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}