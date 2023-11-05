using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camcon : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;



    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        pos.z = -10f;
        pos.y = 0f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}
