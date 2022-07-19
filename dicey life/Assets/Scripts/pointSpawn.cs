using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSpawn : MonoBehaviour
{
    public GameObject point;
    public float maxX;
    public float minX;
    public float y;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn(){
        float randomX = Random.Range(minX,maxX);
        Instantiate(point, transform.position + new Vector3(randomX, y, 0), transform.rotation);
    }
}
