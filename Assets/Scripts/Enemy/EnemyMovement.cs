using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 50f;
    public float rotationSpeed = 1000f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        
        if(isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight)
        {
            transform.Rotate(new Vector3(0,0,rotationSpeed * Time.deltaTime));
        }
        if(isRotatingLeft)
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
        }
        rb.AddForce(transform.right * movementSpeed * Time.deltaTime);
    }
    IEnumerator Wander()
    {
        float rotationTime = Random.Range(0.1f,2.0f);
        float rotateWait = Random.Range(0.1f, 1.5f);
        float rotateDirection = Random.Range(1,3);
        //float moveWait = Random.Range(1,3);
        //float moveTime = Random.Range(1,3);

        isWandering = true;

        //yield return new WaitForSeconds(moveTime);

        //yield return new WaitForSeconds(moveWait);


        yield return new WaitForSeconds(rotateWait);

        if(rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }


        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }
}
