using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollowPlayer : MonoBehaviour
{
    public Transform jugador; // Referencia al objeto del jugador.
    public float distanciaYSigueAlJugador = 5.0f; // La distancia en el eje Y a la que el objeto "spawn" seguirá al jugador.

    void Update()
    {
        if (jugador == null)
        {
            // No hay objeto del jugador asignado, así que no hacemos nada.
            return;
        }

        // Calcula la posición Y a la que debe seguir el objeto "spawn" usando la posición Y del jugador.
        Vector3 posicionObjetoSpawn = new Vector3(transform.position.x, jugador.position.y + distanciaYSigueAlJugador, transform.position.z);

        // Actualiza la posición del objeto "spawn" para que siga al jugador en el eje Y.
        transform.position = posicionObjetoSpawn;
    }
}
