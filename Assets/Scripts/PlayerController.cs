using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f; // Velocidad de movimiento del personaje
    public LayerMask groundLayer; // Capa de las plataformas
    private Animator anim;
    private Rigidbody2D rb;
    private bool isGrounded; //Rayo que detecta si está en ground
    private bool isMoving; //Detecta si el ninja ya se está moviendo
    private int direction; // 1 para derecha, -1 para izquierda
    private int plataforma; //para saber en que plataforma se encuentra

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        plataforma = 1; //Inicia en la plataforma 1
        direction = 1; // Inicialmente, el personaje se mueve hacia la derecha
        
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
        Move();
        SwitchPlatform();
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            isMoving = true;
            anim.SetBool("moving", true);
            direction = (int)Mathf.Sign(horizontalInput); // Obtener la dirección del movimiento
            
        }

        if (isMoving)
        {
            Vector2 movement = new(direction * speed, rb.velocity.y);
            rb.velocity = movement;

            if (direction == 1)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else { transform.localScale = new Vector3(-1f, 1f, 1f); }

            Debug.Log(direction);
        }
    }
        
    void SwitchPlatform()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput > 0 && isGrounded)
            {
                // Rayo hacia arriba para detectar colisiones
                Debug.Log("Arriba");
                int layerMask = 1 << groundLayer; // Solo interactúa con la capa ground
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, layerMask);
                
                //if (hit.collider != null)
                //{
                //    Debug.Log("Colisión con: " + hit.collider.name);
                //}
                //else
                //{
                //    Debug.Log("No hay colisión hacia arriba.");
                //}


                if (!hit && plataforma < 4)
                {
                    
                    plataforma++;
                    Debug.Log("Subiste a la plataforma: " + plataforma);
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
                }
            }
            else if (verticalInput < 0 && isGrounded)
            {
                Debug.Log("Abajo");
                // Rayo hacia abajo para detectar colisiones
                int layerMask = 1 << groundLayer; // Solo interactúa con la capa ground
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask);

                

                if (!hit && plataforma > 1)
                {
                    plataforma--;
                    Debug.Log("Bajaste a la plataforma: "+plataforma);
                    // Cambiar a la plataforma inferior solo si no hay colisión
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
                }
            }
        }
    }
        
}


