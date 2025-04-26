using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject axePrefab;
    private AxeThrown axeToThrow;
    private float axeTimer;
    private int timerDuration = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        axeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs((transform.position - player.transform.position).magnitude) <= 10 && axeTimer <= 0)
        {
            axeToThrow = Instantiate(axePrefab, transform.position, Quaternion.identity).GetComponent<AxeThrown>();
            axeToThrow.initialDirection = -1 * (transform.position - player.transform.position).normalized;
            axeToThrow.hasBeenCalled = true;
            axeTimer = timerDuration;
        }
        else if(axeTimer > 0)
        {
            axeTimer -= Time.deltaTime;
        }
    }
}
