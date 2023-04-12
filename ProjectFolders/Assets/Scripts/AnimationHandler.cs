using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [Header("Wave Reset Reference")]
    public Wave callReset;

    [Header("Water Collision Handler Reference")]
    public WaterCollisionHandler collisionHandler;
    
    private ThrowDiver throwDiver;
    private bool isOnBoard = false;
    private bool isOnPoolLedge = true;
    private bool throwTrigger = false;
    private float endTime = 0.0f;
    private float walkTo;
    private float runOn;

    void Awake()
    {
        throwDiver = gameObject.GetComponent<ThrowDiver>();
    }

    public void ControlAnimation(Animator anim)
    {
        if ((Input.GetKeyDown(KeyCode.C) && !isOnBoard) && (Time.time > endTime))
        {
            anim.gameObject.GetComponent<Animator>().enabled = true;
            throwDiver.SetGravity(false);
            anim.Play("DiverWalkToDivingBoard");
            isOnBoard = true;
            isOnPoolLedge = false;
            endTime = walkTo + Time.time;
            callReset.ResetWave();
            collisionHandler.ResetButterfly();
        }
        else if ((Input.GetKeyDown(KeyCode.R) && !isOnPoolLedge) && (Time.time > endTime))
        {
            anim.Play("DiverRunOnDivingBoard");
            isOnBoard = false;
            isOnPoolLedge = true;
            throwTrigger = true;
            endTime = runOn + Time.time;
        }
        if (throwTrigger && Time.time > endTime)
        {
            anim.gameObject.GetComponent<Animator>().enabled = false;
            throwDiver.Throw();
            throwTrigger = false;
        }
    }

    public void UpdateAnimClipTimes(Animator anim)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "DiverWalkToDivingBoard":
                    walkTo = clip.length;
                    break;
                case "DiverRunOnDivingBoard":
                    runOn = clip.length;
                    break;
            }
        }
    }
}
