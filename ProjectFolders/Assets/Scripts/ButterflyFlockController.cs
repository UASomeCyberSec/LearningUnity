using System.Collections.Generic;
using UnityEngine;

public class ButterflyFlockController : MonoBehaviour
{
    public GameObject butterflyPrefab;
    public int flockSize = 10;
    public float spawnRadius = 5f;
    public float separationDistance = 1f;

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
        foreach (var butterfly in butterflies)
        {
            butterfly.UpdateButterfly();
        }
    }
}