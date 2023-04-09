using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowDiver : MonoBehaviour
{
    [Header("Reference")]
    public GameObject diver;

    [Header("Throwing Force")]
    public float throwForce;

    [Header("UI Reference")]
    public TextMeshProUGUI throwForceText;

    private Transform throwPoint;
    private Rigidbody throwDiverRb;

    void Awake()
    {
        throwForceText.text = "Throw Force: " + throwForce;

        // Get Rigidbody component of diver
        throwDiverRb = diver.GetComponent<Rigidbody>();

        throwPoint = diver.transform;
    }

    public void Throw()
    {
        throwDiverRb.useGravity = true;
        // Add force to diver
        Vector3 forceToAdd = throwPoint.up * throwForce + throwPoint.right * throwForce;

        throwDiverRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    public void SetGravity(bool gravity)
    {   
        // Set gravity to true
        throwDiverRb.useGravity = gravity;
    }

    public void IncreaseThrowForce()
    {
        if (throwForce < 10)
        {
            throwForce += 1;
            throwForceText.text = "Throw Force: " + throwForce;
        }
    }

    public void DecreaseThrowForce()
    {
        if (throwForce > 2)
        {
            throwForce -= 1;
            throwForceText.text = "Throw Force: " + throwForce;
        }
    }
}
