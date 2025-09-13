using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodGenerate : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject[] foodPrefabs;

    [Header("GenerateNumber")]
    public int foodCount = 3;

    [Header("food_RandomGenerate")]
    public Transform left;
    public Transform right;

    [Header("Throw Settings")]
    public float throwForce = 400f; 


    public void SpawnFood()
    {
        for (int i = 0; i < foodCount; i++)
        {
            Vector3 spawnPos = GetRandomPositionInArea();
            GameObject food = Instantiate(GetRandomFood(), spawnPos, Quaternion.identity);

            // 丟出：加力往上
            Rigidbody2D rb = food.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * throwForce);
            }
            else
            {
                Debug.LogWarning("Prefab 沒有 Rigidbody2D，無法丟出！");
            }
        }
    }

    GameObject GetRandomFood()
    {
        int index = Random.Range(0, foodPrefabs.Length);
        return foodPrefabs[index];
    }
    Vector3 GetRandomPositionInArea()
    {
        float minX = Mathf.Min(left.position.x, right.position.x);
        float maxX = Mathf.Max(left.position.x, right.position.x);
        float randomX = Random.Range(minX, maxX);
        float y = left.position.y; // 固定 Y 軸高度
        return new Vector3(randomX, y, 0f);
    }
}
