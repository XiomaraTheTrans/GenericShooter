using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    public float speed = 10;
    public int playerHealth = 3;
    public GameObject bullet;
    public float bulletSpeed = 5;
    public float bulletLife = 1;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0)
        {
            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
            playerHealth = 3;
        }
        Vector2 velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;

        myRB.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

            Destroy(b, bulletLife);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);

            Destroy(b, bulletLife);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

            Destroy(b, bulletLife);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);

            Destroy(b, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Enemy"))
        {
            //This also means playerHealth = playerHealth - 1;
            playerHealth--;
        }

        else if((collision.gameObject.name.Contains("Health")) && (playerHealth < 3))
        {
            //This also means playerHealth = playerHealth +1;
            playerHealth++;
            collision.gameObject.SetActive(false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
   //     GameObject.Find("Enemy").GetComponent<enemyMovement>().canMove = true;
   //     Destroy(collision.gameObject);
    //}
}
