using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // pelihahmon nopeus X-akselin suunnassa
    public float moveSpeed;

    // hyppyvoima Y-akselin suunnassa
    public float jumpForce;

    private bool attack = false;

    // Näppäinmuuttujat
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode attackKey;
    

    // Yhteys pelihahmon fysiikkamoottoriin (muuttuja)
    private Rigidbody2D rb2d;

    // Hyppyyn liittyvät muuttujat
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround; // kokonaisluku 0-31
    public bool isGround; // Jos ollaan maassa, niin isGround = true
    

    // Yhteys animaattoriin
    private Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        // Yhteys pelihahmon fysiikkamoottoriin
        rb2d = GetComponent<Rigidbody2D>();

        // Yhteys animaattoriin
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        // Tarkistetaan ollaanko maassa vai ilmassa
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        
        //Testi
        print("Maassa: " + isGround);
        
        // Pelihahmon liike
        if (Input.GetKey(left))
        {
            // Jos true, niin liikutaan vasemmalle. moveSpeed arvo on negatiivinen koska liikutaan vasemmalle
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            
            
        }
        else if (Input.GetKey(right))
        {
            // jos painettiin näppäintä joka on oikea näppäin, liikutaan positiivisella moveSpeedillä oikealle.
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            
        }
        else
        {
            // Pelihahmo ei liiku
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        // Pelihahmon hyppy
        if (Input.GetKeyDown(jump) && isGround)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(attackKey) && isGround)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

        // Pelihahmon suunnan muutos
        if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb2d.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Kävelyanimaatio kutsut
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x)); //Math.Abs varmistaa että nopeusarvo on aina positiivinen.

        HandleAttacks();

        
    }

    private void HandleAttacks()
    {
        if (attack)
        {
            anim.SetTrigger("attack");

            
        }
        
    }

    

}
