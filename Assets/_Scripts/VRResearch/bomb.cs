﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    //just check on collision enter
    public ParticleSystem explosion;
    public int pointVal = -4;
    
    void OnDestroy() {
        explosion.Play();
    }
}