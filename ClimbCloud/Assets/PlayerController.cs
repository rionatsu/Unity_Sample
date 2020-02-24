using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    //float jumpForce = 680.0f;
    float jumpForce = 500.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
 
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // jump
        if (Input.GetKeyDown(KeyCode.Space) &&
            this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // move right / left
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // player's speed
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // speed limit
        if(speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // turn
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // change animation speed by player's walk speed
        this.animator.speed = speedx / 2.0f;

        // restart because of out of game screen
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    // goal
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");
    }
}
