using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScore : MonoBehaviour
{
    public ScoreManager SM;

    bool isCollided;

    // Start is called before the first frame update
    void Start()
    {
        isCollided = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Interactable")
        {
            var normal = collision.GetContact(0).normal;

            if (normal.y > 0 && isCollided == false)
            { 
                //if the bottom side hit something 
                Debug.Log("You Hit the floor");
                SM.AddScore();
                isCollided = true;
            }
        }


        if (isCollided == true)
        {
            if(collision.gameObject.name == "Table top")
            {
                Debug.Log("Box falling");
                SM.ReduceScore();
                isCollided = false;
            }
                
        }
        

    }
}
