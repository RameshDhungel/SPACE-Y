﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCoontroller : MonoBehaviour
{

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);

    }

}
