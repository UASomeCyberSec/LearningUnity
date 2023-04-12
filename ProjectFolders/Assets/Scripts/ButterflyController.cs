using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    public ButterflyFlockController flockController;
    public float moveSpeed = 4f;
    public float rotationSpeed = 4f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateButterfly()
    {
        Vector3 cohesion = ComputeCohesion() * 2f; // Increase the weight of cohesion
        Vector3 separation = ComputeSeparation();
        Vector3 alignment = ComputeAlignment();

        Vector3 targetDirection = cohesion + separation + alignment;
        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.velocity = transform.forward * moveSpeed;
    }

    private Vector3 ComputeCohesion()
    {
        Vector3 center = flockController.transform.position;
        Vector3 directionToCenter = (center - transform.position);
        float distanceToCenter = directionToCenter.magnitude;

        if (distanceToCenter > flockController.keepDistance)
        {
            return directionToCenter.normalized;
        }
        else if (distanceToCenter < flockController.keepDistance)
        {
            return -directionToCenter.normalized;
        }
        else
        {
            return Vector3.zero;
        }
    }


    private Vector3 ComputeSeparation()
    {
        Vector3 separation = Vector3.zero;
        foreach (var butterfly in flockController.butterflies)
        {
            if (butterfly != this)
            {
                float distance = Vector3.Distance(transform.position, butterfly.transform.position);
                if (distance < flockController.separationDistance)
                {
                    separation += (transform.position - butterfly.transform.position).normalized / distance;
                }
            }
        }
        return separation;
    }

    private Vector3 ComputeAlignment()
    {
        Vector3 alignment = flockController.transform.forward;
        return alignment;
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}