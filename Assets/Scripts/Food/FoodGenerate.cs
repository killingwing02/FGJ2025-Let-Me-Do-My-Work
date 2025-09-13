using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerate : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject[] foodPrefabs;

    [Header("GenerateNumber")]
    public int foodCount = 3;

    [Header("food_RandomGenerate")]
    public Transform left;
    public Transform right;

    private void Start()
    {
        SpawnFood();
    }
    void SpawnFood()
    {
        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, foodPrefabs.Length);
            } while (usedIndexes.Contains(randomIndex));

            usedIndexes.Add(randomIndex);

            Vector3 spawnPos = GetRandomPositionInArea();
            Instantiate(foodPrefabs[randomIndex], spawnPos, Quaternion.identity);
        }

        Vector3 GetRandomPositionInArea()
        {
            float minX=left.position.x;
            float maxX=right.position.x;

            float randomFood=Random.Range(minX, maxX);
            return new Vector3(randomFood, 0f);
        }
    }
}
