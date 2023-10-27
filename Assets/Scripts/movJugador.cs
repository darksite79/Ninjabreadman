using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movJugador : MonoBehaviour
{

    private Rigidbody2D rb2D;

    [Header("Movimiento")]

    private float movHorizontal = 0f;

    [SerializeField] private float velocidadMov;

    [Range(0, 0.3f)][SerializeField] private float suavizadoMov;

    private Vector3 velocidad = Vector3.zero;

    private bool mirarDer = true;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        movHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMov;
    }

    private void FixedUpdate()
    {
        //Mover

        Mover(movHorizontal * Time.fixedDeltaTime);
    }

    private void Mover(float mover)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoMov);

        if (mover > 0 && !mirarDer)
        {
            //Girar
            Girar();
        }
        
        else if ( mover < 0 && mirarDer)
        {
            //Girar
            Girar();
        }

    }

    private void Girar()
    {
        mirarDer = !mirarDer;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

}
