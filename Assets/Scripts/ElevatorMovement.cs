using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{

    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;

    private GameObject nextTarget;

    [SerializeField] private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        nextTarget = point1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MoveToPosition(nextTarget);
    
    }
   
    
    private void MoveToPosition(GameObject moveToTarget)
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, moveToTarget.transform.position, speed * Time.fixedDeltaTime);
        if (gameObject.transform.position == moveToTarget.transform.position)
        {
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        if (nextTarget == point1)
        {
            nextTarget = point2;
        } else
        {
            nextTarget = point1;
        }
    }
}



