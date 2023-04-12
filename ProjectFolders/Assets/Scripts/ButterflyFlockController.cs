using System.Collections.Generic;
using UnityEngine;

public class ButterflyFlockController : MonoBehaviour
{
    public GameObject butterflyPrefab;
    public int flockSize = 20;
    public float spawnRadius = 5f;
    public float separationDistance = 1f;
    public float keepDistance = 3f;
    public bool followPlayer = true;

    public List<ButterflyController> butterflies = new List<ButterflyController>();

    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject butterflyInstance = Instantiate(butterflyPrefab, spawnPosition, Quaternion.identity);
            butterflyInstance.transform.SetParent(transform);

            ButterflyController butterflyController = butterflyInstance.GetComponent<ButterflyController>();
            butterflyController.flockController = this;
            butterflies.Add(butterflyController);
        }
    }

    void Update()
    {
        if (followPlayer)
        {
            foreach (var butterfly in butterflies)
                butterfly.UpdateButterfly();
        }
        else
        {
            foreach (var butterfly in butterflies)
                butterfly.StopMoving();
        }
    }

    public void StopFollowPlayer()
    {
        followPlayer = false;
    }

    public void FollowPlayer()
    {
        followPlayer = true;
    }
}