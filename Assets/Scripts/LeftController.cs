﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftController : MonoBehaviour
{
    private readonly string[] unpassableBlocksTags = {"Wall", "Hole", "Lock", "UpOnly", "DownOnly", "RightOnly"};
    private readonly string[] movableBlocksTags = {"block"};
    public bool Movable = true;
    private bool blocked;
    private bool moveBlock;
    private GameObject Player;
    List<Collider2D> collider = new List<Collider2D>();

    public AudioSource HitWall;
    public AudioSource MoveSound;

    private void Start()
    {
        Player = transform.parent.gameObject;
    }

    private void Update()
    {
        blocked = false;
        moveBlock = false;
        if (collider != null)
        {
            CheckCollider();
        }

        if (Swipe.SwipeLeft && Movable && !moveBlock)
        {
            Player.transform.position += Vector3.left;
            MoveSound.Play();
            blocked = false;
            moveBlock = false;
            Swipe.SwipeLeft = false;
        }
        else if (Swipe.SwipeLeft && !moveBlock)
        {
            HitWall.Play();
        }

        Movable = !blocked;
    }

    private void CheckCollider()
    {
        foreach (var i in unpassableBlocksTags)
        {
            foreach (var j in collider)
            {
                if (j != null && j.CompareTag(i))
                {
                    blocked = true;
                }
            }
        }

        foreach (var i in movableBlocksTags)
        {
            foreach (var j in collider)
            {
                if (j != null && j.CompareTag(i))
                {
                    moveBlock = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (collider.Count < 3)
            collider.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collider.Clear();
    }
}