
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private const int visabilityDistance = 30;
    private Transform playerT;
    private bool isVisible;
    private Vector3 position;
    private void Start()
    {
        playerT = GameObject.Find("Player").transform;
        position = transform.position;
        isVisible = true;
    }

    private void Update()
    {
        float distance = Vector3.Distance(position, new Vector3 (playerT.position.x, 0f, playerT.position.z));
        if (distance > visabilityDistance)
        {
            SetActivity(false);
        }
        else if (distance < visabilityDistance && !isVisible)
        {
            SetActivity (true);
        }
    }
    private void SetActivity(bool isActive)
    {
        int childrenCount = transform.childCount;
        for(int i  = 0; i < childrenCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
            isVisible = isActive;
        }
    }
}