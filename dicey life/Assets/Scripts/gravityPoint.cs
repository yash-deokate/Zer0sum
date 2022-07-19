using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityPoint : MonoBehaviour
{
    public float gravityScale, planetRadius, gravityMinRange, gravityMaxRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D obj) {
        float gravitationalPower = gravityScale / planetRadius;
        float dist = Vector2.Distance(obj.transform.position, transform.position);

        if (dist > (planetRadius + gravityMinRange))
        {
            float min = planetRadius + gravityMinRange + 0.5f;
            gravitationalPower = gravitationalPower * (((min + gravityMaxRange) - dist) / gravityMaxRange);
        }

        Vector3 dir = (transform.position - obj.transform.position) * gravitationalPower;
        obj.GetComponent<Rigidbody2D>().AddForce(dir);

        if (obj.CompareTag("Player"))
        {
            obj.transform.up = Vector3.MoveTowards(obj.transform.up, -dir, gravitationalPower * Time.deltaTime * 5f);
        }
    }
}
