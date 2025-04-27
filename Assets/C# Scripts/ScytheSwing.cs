using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private float timerDuration = 0.5f;
    private float timer;
    private int scytheDamage = 5;
    private int pushBackMagnitude = 500;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDuration;
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChooseScytheLocation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (IsEnemy(collision) && collision.gameObject.GetComponent<EnemyMovement>() != null && Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 pushBackDirection = (gameObject.transform.position - collision.gameObject.transform.position) * -1;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushBackDirection * pushBackMagnitude * Time.deltaTime, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<EnemyMovement>().enemyHealth -= scytheDamage;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (IsEnemy(collision) && Input.GetKey(KeyCode.Mouse0))
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else if (collision.gameObject.GetComponent<EnemyMovement>() != null && collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 pushBackDirection = (gameObject.transform.position - collision.gameObject.transform.position) * -1;

                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushBackDirection * pushBackMagnitude * Time.deltaTime, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<EnemyMovement>().enemyHealth -= scytheDamage;
                timer = timerDuration;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsEnemy(collision))
        {
            timer = timerDuration;
        }
    }

    private void ChooseScytheLocation()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            int scytheX = 0;
            int scytheY = 0;
            transform.Rotate(new Vector3(0, 0, -10));
            if(Input.GetAxis("Horizontal") > 0)
            {
                scytheX = 1;
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                scytheX = -1;
            }

            if (Input.GetAxis("Vertical") > 0)
            {
                scytheY = 1;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                scytheY = -1;
            }

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                transform.localPosition = originalPosition;
            }
            else
            {
                transform.localPosition = new Vector3Int(scytheX, scytheY, 0);
            }
        }
        else
        {
            transform.localPosition = originalPosition;
            transform.rotation = originalRotation;
        }
    }
    private bool IsEnemy(Collider2D collision)
    {
        return PrefabUtility.GetPrefabInstanceHandle(collision.gameObject) == PrefabUtility.GetPrefabInstanceHandle(thumper)
            || PrefabUtility.GetPrefabInstanceHandle(collision.gameObject) == PrefabUtility.GetPrefabInstanceHandle(axeGirl)
            || PrefabUtility.GetPrefabInstanceHandle(collision.gameObject) == PrefabUtility.GetPrefabInstanceHandle(goliathas);
    }
}
