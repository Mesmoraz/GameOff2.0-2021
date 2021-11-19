using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    private float length;
    private float startPos;
    public float paralaxFactor;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - paralaxFactor);
        float distance = cam.transform.position.x * paralaxFactor;

        Vector2 newPosition = new Vector2(startPos + distance, transform.position.y);

        transform.position = newPosition;


        if(temp > startPos + (length / 2))
        {
            startPos += length;
        }
        else if(temp < startPos - (length / 2))
        {
            startPos -= length;
        }
        
    }


}
