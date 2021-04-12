using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float movementSpeed = 7;
    public bool canMove = false;
    private bool movementDirection = false; //false is down, true is up
    private Rigidbody2D myRB;
    public float detectionRadius = 3;
    public bool isFollowing = false;

    private Vector2 zero;
    private CircleCollider2D detectionZone;
    public Transform playerTarget;
    private Vector2 up;
    private Vector2 down;

    // Start is called before the first frame update
    void Start()
    {
        up = new Vector2(0, movementSpeed);
        down = new Vector2(0, -movementSpeed);
        zero = new Vector2(0, 0);

        myRB = GetComponent<Rigidbody2D>();
        detectionZone = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        detectionZone.radius = detectionRadius;

        if (isFollowing == false) 
        { 
            myRB.velocity = zero;
        }

        else if (isFollowing == true)
        {
            Vector3 lookPos = playerTarget.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            myRB.rotation = angle;
            lookPos.Normalize();

            myRB.MovePosition(transform.position + (lookPos * movementSpeed * (Time.deltaTime)));
        }

        if (canMove == true)
        {
            myRB.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            if (movementDirection == false)
                myRB.velocity = down;

            else if (movementDirection == true)
                myRB.velocity = up;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bullet"))
        {
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameManeger>().playerKillCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            isFollowing = true;
        }
        if(collision.gameObject.name == "trigger1")
        {
            movementDirection = false;
        }
        else if(collision.gameObject.name == "trigger2")
        {
            movementDirection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            isFollowing = false;
        }
    }
}
