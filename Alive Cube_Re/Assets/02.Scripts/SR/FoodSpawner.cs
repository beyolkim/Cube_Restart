using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] food;

    void Start()
    {
        StartCoroutine(SpawnFood());
    }
    
    IEnumerator SpawnFood()
    {
        while (true)
        {
            GameObject go = Instantiate(food[Random.Range(0, food.Length)]); //food의 배열안에 있는 0 ~ 2 번 중 랜덤으로 생성
            Rigidbody temp = go.GetComponent<Rigidbody>();

            temp.velocity = new Vector3(0f, 5f, 0.5f);
            temp.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            temp.useGravity = true;

            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            go.transform.position = pos;

            yield return new WaitForSeconds(1f);
        }
    }
}
