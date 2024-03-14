using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dashDuration = 0.25f;
    public bool canMove = true;
    private float input;
    public float moveSpeed;
    public float dashForce;
    public float jumpForce;
    private Rigidbody2D _therB;
    public LayerMask whatIsGround;
    private bool _isGrounded;
    public Transform GroundCheckPoint;
    public float knockBackeForce = 5f;
    public float knockbackDuration = .25f;
    public GameObject bullet;
    public GameObject Trampa;
    public Transform firePoint;
    public Transform Trampapoint;
    // Start is called before the first frame update
    void Start()
    {
        _therB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            putTramp();
        }
        //Disparar bala
        if (Input.GetKeyDown(KeyCode.M))
        {
            bulletDisparo();
        }
        //Otra forma de hacer que se mueva el personaje
        input = Input.GetAxisRaw("Horizontal");
        //Esto hace que solo se gire cuando se esté moviendo
        if (input != 0f)
        {
            if (input > 0f)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            //_therB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, _therB.velocity.y);
            _isGrounded = Physics2D.OverlapCircle(GroundCheckPoint.position, .2f, whatIsGround);
        }
       

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                _therB.velocity = new Vector2(_therB.velocity.x, jumpForce);

            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Dash();
        }
    }
    private void putTramp()
    {
        Instantiate(Trampa, Trampapoint.transform.position, Trampapoint.transform.rotation);
    }
    private void bulletDisparo()
    {
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        
    }
    private void FixedUpdate()
    {
        if(canMove == true)
        {
            _therB.velocity = new Vector2(input * moveSpeed, _therB.velocity.y);

        }
    }
    void Dash()
    {
        
        canMove = false;
        _therB.velocity = new Vector2(0, _therB.velocity.y);
        //El transform.right hace q vaya a la derecha o izquierda dependiendo de la rotación del personaje
        _therB.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
        //Activamos la corrutina
        StartCoroutine(CRT_CancelDash());
    }

    IEnumerator CRT_CancelDash()
    {
        //Esto cancela el movimiento mientras estamos haciendo el dash
        yield return new WaitForSeconds(dashDuration);
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.collider.CompareTag("Enemy") == true)
        {
            //Se puede acceder a la posicion en x del objeto contra el que ha chocado usando el collision.collider
            ApplyKnockback(collision.collider.transform.position.x);
        }

        
    }

    //Variable _cPosition representa la posicion en X del objeto contra el que ha hecho
    void ApplyKnockback(float _xPosition)
    {
        canMove = false;
        //Hacemos que la velocidad en X sea 
        _therB.velocity = new Vector2(0, _therB.velocity.y);
        //Dependiendo de si el objeto esta en la derecha o izquierda añadira un empujon en otra direccion
        if(transform.position.x < _xPosition)
        {
            _therB.AddForce(new Vector2(-1, 0.75f) * knockBackeForce, ForceMode2D.Impulse);
        }
        else
        {
            _therB.AddForce(new Vector2(1, 0.75f) * knockBackeForce, ForceMode2D.Impulse);
        }
        
        StartCoroutine(CRT_CancelKnockbak());
    }

    IEnumerator CRT_CancelKnockbak()
    {
        yield return new WaitForSeconds(knockbackDuration);
        canMove = true;
    }
}
