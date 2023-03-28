using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header("Wave Parameters")]
    public float amplitude = 2;
    public float velocity = 2;
    public float waveLength = 2;
    public float speedOfDecay = 2;
    public float distanceCovered = 2;
    public bool collided = false;

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

    void Awake()
    {
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
        float objectDimension = Mathf.Sqrt(objectLength * objectLength + objectWidth * objectWidth);
        nr = radius / objectDimension;
    }

    void Start()
    {

    }

    void Update()
    {
        if(collided)
        {
            Vector3[] verts = mesh.vertices;
            for (var v = 0; v < verts.Length; v++)
            {
                Vector3 vertex = verts[v];
                float x = transform.TransformPoint(vertex).x;
                float z = transform.TransformPoint(vertex).z;
                radius = Mathf.Sqrt((x-x0)*(x-x0)+(z-z0)*(z-z0));
                distance = radius / nr;
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

        impactTime = Time.time;
        GetComponent<Collider>().enabled = false;
        // Get the collision point
        Vector3 collisionPoint = collision.contacts[0].point;

        // Draw a line from the origin of the wave to the collision point
        Debug.DrawLine(transform.position, collisionPoint, Color.red, 1.0f);

        // Set equal to x axis of collision point
        x0 = collisionPoint.x;
        // Set equal to z axis of collision point
        z0 = collisionPoint.z;

        collided = true;
    }

    public float findY(float time, float radius)
    {
        return amplitude * Mathf.Pow((float)System.Math.E, -radius - amplitude * time) * Mathf.Cos(2 * Mathf.PI * (radius - velocity * time) / waveLength);
    }
}
