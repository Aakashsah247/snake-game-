using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rb;

    private List<Transform> _snakeSpawn;
    public Transform snakePrefab;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity= new Vector2(moveSpeed,0);
        _snakeSpawn= new List<Transform>();
        _snakeSpawn.Add(this.transform);
    }
    private void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Debug.Log("Game Restart");
}
private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    
    void Update()
{
    if (Input.GetKeyDown(KeyCode.W) && rb.velocity.y == 0)
    {
        rb.velocity = new Vector2(0, moveSpeed);
    }
    else if (Input.GetKeyDown(KeyCode.X) && rb.velocity.y == 0)
    {
        rb.velocity = new Vector2(0, -moveSpeed);
    }
    else if (Input.GetKeyDown(KeyCode.D) && rb.velocity.x == 0)
    {
        rb.velocity = new Vector2(moveSpeed, 0);
    }
    else if (Input.GetKeyDown(KeyCode.A) && rb.velocity.x == 0)
    {
        rb.velocity = new Vector2(-moveSpeed, 0);
    }
    else if (Input.GetKeyDown(KeyCode.P))
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Game Push");
    }
    else if (Input.GetKeyDown(KeyCode.R))
    {
        RestartGame();
    }
    else if (Input.GetKeyDown(KeyCode.Q))
    {
        QuitGame();
    }
}


private void FixedUpdate()
{
    for(int i=_snakeSpawn.Count -1; i>0; i--)
    {
        _snakeSpawn[i].position= _snakeSpawn[i-1].position;
    }
}
    private void grow()
    {
        Transform snakeSpawn= Instantiate(this.snakePrefab);
        snakeSpawn.position= _snakeSpawn[_snakeSpawn.Count-1].position;

        _snakeSpawn.Add(snakeSpawn);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="food")
        {
            grow();
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "snakePrefab" )
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}