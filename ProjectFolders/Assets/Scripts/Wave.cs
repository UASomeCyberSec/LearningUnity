using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wave : MonoBehaviour
{
    [Header("Wave Parameters")]
    public float amplitude = 2;
    public float velocity = 2;
    public float waveLength = 2;
    public float speedOfDecay = 2;
    public float distanceCovered = 2;
    public bool collided = false;

    [Header("UI References")]
    public TextMeshProUGUI amplitudeText;
    public TextMeshProUGUI waveLengthText;
    public TextMeshProUGUI speedOfDecayText;

    [Header("Particle Reference")]
    public GameObject particle;
    private ParticleSystem particleSystem;

    private Mesh mesh;
    private Vector3[] verts;
    private float x0;
    private float z0;
    private float x;
    private float z;
    private float radius;
    private float nr;
    private float impactTime;
    private float distance;
    private float objectDimension;

    [Header("Score Manager")]
    public GameManager scoreCall;

    void Awake()
    {
        amplitudeText.text = "Amplitude: " + amplitude;
        waveLengthText.text = "Wavelength: " + waveLength;
        speedOfDecayText.text = "Speed of Decay: " + speedOfDecay;

        particleSystem = particle.GetComponent<ParticleSystem>();

        if(this.GetComponent<MeshFilter>() == null)
            this.gameObject.AddComponent<MeshFilter>();
        // Mesh filter mesh
        mesh = this.GetComponent<MeshFilter>().mesh;
        // Mesh vertices
        verts = mesh.vertices;
        // x given distance
        x = distanceCovered;
        // z given distance
        z = distanceCovered;

        float objectLength = this.transform.localScale.x;
        float objectWidth = this.transform.localScale.z; // effectively the same as this.transform.localScale.x
        objectDimension = Mathf.Sqrt(objectLength * objectLength + objectWidth * objectWidth);
    }

    void Update()
    {
        if(collided)
        {
            Vector3[] verts = mesh.vertices;
            for (var v = 0; v < verts.Length; ++v)
            {
                Vector3 vertex = verts[v];
                float x = transform.TransformPoint(vertex).x;
                float z = transform.TransformPoint(vertex).z;
                radius = Mathf.Sqrt((x-x0)*(x-x0)+(z-z0)*(z-z0));
                distance = radius / objectDimension;
                float timeDifference = Time.time - impactTime;
                vertex.y = findY(timeDifference, radius);
                verts[v] = vertex;
            }

            mesh.vertices = verts;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");

        // Particle system
        particleSystem.Play();

        impactTime = Time.time;
        GetComponent<Collider>().enabled = false;
        // Collision point
        Vector3 collisionPoint = collision.contacts[0].point;

        // Set equal to x axis of collision point
        x0 = collisionPoint.x;
        // Set equal to z axis of collision point
        z0 = collisionPoint.z;

        collided = true;

        // Call the score update function
        scoreCall.UpdateScore();
    }

    private float findY(float time, float radius)
    {
        return amplitude * Mathf.Exp(-distance - (speedOfDecay * time)) * Mathf.Cos(2 * Mathf.PI * (radius - velocity * time) / waveLength);
    }

    public void ResetWave()
    {
        GetComponent<Collider>().enabled = true;
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void IncreaseAmp()
    {
        if (amplitude < 10.0f)
        {
            amplitude += 0.5f;
            amplitudeText.text = "Amplitude: " + amplitude;
        }
    }

    public void DecreaseAmp()
    {
        if (amplitude > 0.5f)
        {
            amplitude -= 0.5f;
            amplitudeText.text = "Amplitude: " + amplitude;
        }
    }

    public void IncreaseWaveLength()
    {
        if (waveLength < 10.0f)
        {
            waveLength += 0.5f;
            waveLengthText.text = "Wavelength: " + waveLength;
        }
    }

    public void DecreaseWaveLength()
    {
        if (waveLength > 0.5f)
        {
            waveLength -= 0.5f;
            waveLengthText.text = "Wavelength: " + waveLength;
        }
    }

    public void IncreaseSpeedOfDecay()
    {
        if (speedOfDecay < 10.0f)
        {
            speedOfDecay += 0.5f;
            speedOfDecayText.text = "Speed of Decay: " + speedOfDecay;
        }
    }

    public void DecreaseSpeedOfDecay()
    {
        if (speedOfDecay > 0.5f)
        {
            speedOfDecay -= 0.5f;
            speedOfDecayText.text = "Speed of Decay: " + speedOfDecay;
        }
    }
}
