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

            // ��X�G�[�O���W
            Rigidbody2D rb = food.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * throwForce);
            }
            else
            {
                Debug.LogWarning("Prefab �S�� Rigidbody2D�A�L�k��X�I");
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
        float y = left.position.y; // �T�w Y �b����
        return new Vector3(randomX, y, 0f);
    }
}
