using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Bg : MonoBehaviour
{
    public GameObject cam;
    public float parallax;
    float startPosX;

    void Start()
    {
        startPosX = transform.position.x;
    }


    void Update()
    {
        float disX = (cam.transform.position.x * (1 - parallax));
        transform.position = new Vector3(startPosX + disX, transform.position.y, transform.position.z);

    }
}
