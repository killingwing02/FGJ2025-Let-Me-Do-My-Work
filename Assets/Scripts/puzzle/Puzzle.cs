using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject[] puzzlePrefabs;

    [Header("GenerateNumber")]
    public int puzzleCount = 3;

    [Header("puzzle_RandomGenerate")]
    public Transform topLeft;
    public Transform topRight;
    public Transform bottomLeft;
    public Transform bottomRight;

    

    void Start()
    {
        SpawnThreeUniquePuzzles();
    }

    void SpawnThreeUniquePuzzles()
    {
        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < 7; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, puzzlePrefabs.Length);
            } while (usedIndexes.Contains(randomIndex));

            usedIndexes.Add(randomIndex);

            Vector3 spawnPos = GetRandomPositionInArea();
            Instantiate(puzzlePrefabs[randomIndex], spawnPos, Quaternion.identity);
        }

        Vector3 GetRandomPositionInArea()
        {
            float minX = Mathf.Min(topLeft.position.x, bottomLeft.position.x);
            float maxX = Mathf.Max(topRight.position.x, bottomRight.position.x);
            float minY = Mathf.Min(bottomLeft.position.y, bottomRight.position.y);
            float maxY = Mathf.Max(topLeft.position.y, topRight.position.y);

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            return new Vector3(randomX, randomY, 0f); // stay in 2D
        }
    }
    
}
