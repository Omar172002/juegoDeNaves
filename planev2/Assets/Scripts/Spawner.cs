using UnityEngine;

using System.Collections;


public class Spawner : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform jugador; // Referencia al objeto del jugador.
    public float distanciaYSigueAlJugador = 5.0f; // Distancia arriba (en el eje Y) del jugador para spawnear al enemigo.
    public float tiempoEntreSpawns = 2.0f;

    private float tiempoSiguienteSpawn = 0.0f;

    void Update()
    {
        if (Time.time >= tiempoSiguienteSpawn)
        {
            SpawnEnemigo();
            tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    void SpawnEnemigo()
    {
        

        // Calcula la posición de spawn arriba del jugador en el eje Y.
        Vector3 posicionDeSpawn = new Vector3(jugador.position.x, jugador.position.y + distanciaYSigueAlJugador, jugador.position.z);

        // Crea una instancia del enemigo en la posición calculada.
        GameObject enemigoInstance = Instantiate(enemigoPrefab, posicionDeSpawn, Quaternion.identity);

        Destroy(enemigoInstance, 20.0f);
    }
}


