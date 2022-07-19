using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public GameObject bullet5;
    public GameObject bullet6;
    GameObject dice;
    public float maxX;
    public float minX;
    public float y;
    public float timeBetweenSpawn;
    private float spawnTime;
    private int spawnAmount;

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
        int side=diceController.instance.getSide();
        switch (side)
        {
        case 6: 
            dice= bullet6;
            spawnAmount= 6;
            break;
        case 5:
            dice= bullet5;
            spawnAmount= 5;
            break;
        case 4:
            dice= bullet4;
            spawnAmount= 4;
            break;
        case 3:
            dice= bullet3;
            spawnAmount= 3;
            break;
        case 2:
            dice= bullet2;
            spawnAmount= 2;
            break;
        case 1:
            dice= bullet1;
            spawnAmount= 1;
            break;
        default:
            dice= bullet1;
            spawnAmount= 1;
            break;
        }
        for(int i = 0; i < spawnAmount; i++)
        {
            Instantiate(dice, transform.position + new Vector3(randomX, y, 0), transform.rotation);
        }
        
    }
}
