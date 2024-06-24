using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour
{
    private float startingPos;
    private float lengthOfSprite;
    public float amountOfParallax;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position.x;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = mainCamera.transform.position;
        float temp = position.x * (1 - amountOfParallax);
        float distance = position.x * amountOfParallax;

        Vector3 newPosition = new Vector3(startingPos + distance, transform.position.y, transform.position.z);

        transform.position = newPosition;

        if (temp > startingPos + (lengthOfSprite / 2))
        {
            startingPos += lengthOfSprite;
        }
        else if (temp < startingPos - (lengthOfSprite / 2))
        {
            startingPos -= lengthOfSprite;
        }
    }
}
