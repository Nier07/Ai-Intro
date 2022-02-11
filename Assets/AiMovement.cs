using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject position0;
    public GameObject position1;

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, position0.transform.position,
                                                                        //Time.deltaTime);
        // Vector2 AiPosition = transform.position;

        /*if (transform.position.x < position0.transform.position.x)
        {
            //Move right
            AiPosition.x += (1 * Time.deltaTime);
            transform.position = AiPosition;
        }
        else
        {
            //Move left
            AiPosition.x -= (1 * Time.deltaTime);
            transform.position = AiPosition;
        }
        if (transform.position.y < position0.transform.position.y)
        {
            // .up gives the upward direction
            // (vector 3) sets as vector3 and vector2 gets the relvant 2D info
                                // as to not have a redundant value in a vector3
            //Move up
            transform.position += (Vector3) Vector2.up * 1 * Time.deltaTime;
        }
        else
        {
            //Move down
            transform.position -= (Vector3) Vector2.up * 1 * Time.deltaTime;
        } */

        //direction form A to B
        // is B - A

        Vector2 directionToPos0 = position0.transform.position - transform.position;
        // Normalize converts a direction and makes the length of it = 1
        // if normalize wasnt here would make object move faster if it was further away and slower when closer
        directionToPos0.Normalize();
        transform.position += (Vector3) directionToPos0 * 1 * Time.deltaTime;
    }
}
