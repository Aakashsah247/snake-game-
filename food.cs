using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class food : MonoBehaviour
{
  public BoxCollider2D foodSpawn;
    public TextMeshProUGUI scoreText;

    private float score;
    private NewBehaviourScript snakeController; 

    private void Start()
    {
        snakeController = FindObjectOfType<NewBehaviourScript>();
        RandomPose();
    }


private void Update()
{
     scoreText.text ="Score: "+score;
}
    private void RandomPose()
    {
        Bounds bounds = this.foodSpawn.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RandomPose();

            // Increase score based on snake speed
            float speedMultiplier = snakeController.moveSpeed;
            score += 1 * speedMultiplier;
        }
}
}

