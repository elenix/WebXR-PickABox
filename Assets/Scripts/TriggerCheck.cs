using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("object entered");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("object stay");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("object out");
    }
}
