using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;
    private float cameraSize;

    // Start is called on creation
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraSize = GetComponent<Camera>().orthographicSize;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * gameManager.Instance.movementSpeed * Time.deltaTime; // Adjust this to be player movement speed
        GetComponent<Camera>().orthographicSize = cameraSize + ((gameManager.Instance.movementSpeed - 5) / 10);
    }
}
