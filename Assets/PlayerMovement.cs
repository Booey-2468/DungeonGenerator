using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    [HideInInspector] public int playerHealth = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveVec = Vector3.Normalize(moveVec);

        //transform.position += moveVec * 20 * Time.deltaTime;

        body.MovePosition(body.position + (Vector2)moveVec * 15 * Time.deltaTime);
        //Debug.DrawLine(transform.position, moveVec * 20, Color.red, 600000);


        /*if (Physics.Raycast(transform.position, moveVec, out RaycastHit hitinfo, 200))
        {
            Debug.DrawRay(transform.position, moveVec * 500, Color.red, 600000);
            
        }*/
    }
}
