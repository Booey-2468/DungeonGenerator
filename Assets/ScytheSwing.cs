using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

public class ScytheSwing : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    [SerializeField] private GameObject thumper;
    [SerializeField] private GameObject axeGirl;
    [SerializeField] private GameObject goliathas;
    private float timerDuration = 3;
    private float timer;
    private int scytheDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDuration;
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        ChooseScytheLocation();

    }
    void ChooseScytheLocation()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.Rotate(new Vector3(0, 0, -10));
            if (Input.GetAxis("Horizontal") > 0.1)
            {
                transform.localPosition = Vector3.right;
            }
            else if (Input.GetAxis("Horizontal") < -0.1)
            {
                transform.localPosition = Vector3.left;
            }
            else if (Input.GetAxis("Vertical") > 0.1)
            {
                transform.localPosition = Vector3.up;

            }
            else if (Input.GetAxis("Vertical") < -0.1)
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
        if (collision.gameObject == thumper || collision.gameObject == axeGirl || collision.gameObject == goliathas)
        {
            collision.gameObject.GetComponent<EnemyMovement>().enemyHealth -= scytheDamage;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == thumper || collision.gameObject == axeGirl || collision.gameObject == goliathas)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                collision.gameObject.GetComponent<EnemyMovement>().enemyHealth -= scytheDamage;
                timer = 3;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == thumper || collision.gameObject == axeGirl || collision.gameObject == goliathas)
        {
            timer = timerDuration;
        }
    }
}
