using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxResetPosition : MonoBehaviour
{
    Vector3 originalPost;

    private void Start()
    {
        originalPost = gameObject.transform.position;
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Base")
        {
            gameObject.transform.position = originalPost;
        }
    }
}
