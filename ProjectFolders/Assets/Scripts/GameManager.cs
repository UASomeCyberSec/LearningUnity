using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    [Header("Diver References")]
    public GameObject diver;
    public AnimationHandler diverAnim;
    
    [Header("Wave Reference")]
    public Wave wave;

    [Header("ThrowDiver Reference")]
    public ThrowDiver throwDiver;

    [Header("Water Collision Handler Reference")]
    public WaterCollisionHandler collisionHandler;

    private Animator animator;
    private int score = 0;

    void Awake()
    {
        diverAnim = gameObject.GetComponent<AnimationHandler>();
        animator = diver.GetComponent<Animator>();
        diverAnim.UpdateAnimClipTimes(animator);
    }

    void Update()
    {
        diverAnim.ControlAnimation(animator);

        if (Input.anyKeyDown)
        {
            string input = Input.inputString;
            char key = input[0];
            if (key == 'A')
                wave.IncreaseAmp();
            if (key == 'a')
                wave.DecreaseAmp();
            if (key == 'L')
                wave.IncreaseWaveLength();
            if (key == 'l')
                wave.DecreaseWaveLength();
            if (key == 'V')
                wave.IncreaseSpeedOfDecay();
            if (key == 'v')
                wave.DecreaseSpeedOfDecay();
            if (key == 'T')
                throwDiver.IncreaseThrowForce();
            if (key == 't')
                throwDiver.DecreaseThrowForce();

        }
    }

    public void UpdateScore()
    {
        ++score;
        scoreText.text = "Score: " + score;
    }
}
