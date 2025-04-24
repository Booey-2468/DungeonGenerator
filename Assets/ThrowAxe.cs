using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject axePrefab;
    private AxeThrown axeToThrow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs((transform.position - player.transform.position).magnitude) <= 20)
        {
            axeToThrow = Instantiate(axePrefab, transform.position, Quaternion.identity).GetComponent<AxeThrown>();
            axeToThrow.initialDirection = (transform.position - player.transform.position).normalized;
            axeToThrow.hasBeenCalled = true;
        }
    }
}
