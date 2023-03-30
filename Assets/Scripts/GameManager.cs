using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject diver;
    private Animator anim;
    private bool isOnBoard = false;

    void Awake()
    {
        anim = diver.GetComponent<Animator>();
    }

    void Update()
    {
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOnBoard)
        {
            anim.Play("DiverWalkToDivingBoard");
            isOnBoard = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && anim.GetCurrentAnimatorStateInfo(0).IsName("DiverWalkToDivingBoard"))
        {
            anim.Play("DiverRunOnDivingBoard");
            isOnBoard = false;
        }
    }
}
