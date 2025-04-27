using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public int playerHealth;
    [HideInInspector] public int maxPlayerHealth;
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;
    [SerializeField] private Image heart4;
    [SerializeField] private Image heart5;
    [SerializeField] private Sprite brokenHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private GameObject pauseMenu;
    private Rigidbody2D body;


    // Start is called before the first frame update
    void Start()
    {
        maxPlayerHealth = 10;
        playerHealth = maxPlayerHealth;
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("GameStart");
        }
        UpdateHealth();

        if (Input.GetKeyDown(KeyCode.P) && pauseMenu != null && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Instantiate(pauseMenu);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveVec = Vector3.Normalize(moveVec);

        //transform.position += moveVec * 20 * Time.deltaTime;

        body.MovePosition(body.position + (Vector2)moveVec * 10 * Time.deltaTime);
        //Debug.DrawLine(transform.position, moveVec * 20, Color.red, 600000);


        /*if (Physics.Raycast(transform.position, moveVec, out RaycastHit hitinfo, 200))
        {
            Debug.DrawRay(transform.position, moveVec * 500, Color.red, 600000);
            
        }*/
    }

    void UpdateHealth()
    {
        List<Image> hearts = new List<Image>() { heart1, heart2, heart3, heart4, heart5 };

        int heartHealth = playerHealth;

        foreach(Image heart in hearts)
        {
            if (heartHealth >= 2)
            {
                heart.sprite = fullHeart;
                heartHealth -= 2;
                Debug.Log(playerHealth);
            }
            else if (heartHealth == 1)
            {
                heart.sprite = halfHeart;
                heartHealth--;
            }
            else
            {
                heart.sprite = brokenHeart;
            }
        }
    }
}
