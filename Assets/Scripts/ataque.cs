using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataque : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;

    [SerializeField] private float radioGolpe;

    [SerializeField] private float dañoGolpe;

    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            //if (colisionador.CompareTag("Enemigo"))
            //{
            //    colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            //}

        }

    }

}
