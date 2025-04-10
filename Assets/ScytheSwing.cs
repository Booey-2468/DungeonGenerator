using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScytheSwing : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            transform.Rotate(new Vector3(0, 0, -1));
            if(Input.GetAxis("Horizontal") > 0.1)
            {
                transform.localPosition = Vector3.right;
            }
            else if(Input.GetAxis("Horizontal") < -0.1)
            {
                transform.localPosition = Vector3.left;
            }
            else if(Input.GetAxis("Vertical") > 0.1)
            {
                transform.localPosition = Vector3.up;

            }
            else if(Input.GetAxis("Vertical") < -0.1)
            {
                transform.localPosition = Vector3.down;
            }
        }
        else
        {
            transform.localPosition = originalPosition;
            transform.rotation = originalRotation;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
