using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;


    [Header("Disparo")]
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float shootingInterval = 6f;
    public float timeDisparoDestroy = 3f;

    private float _shootingTimer;

    public Transform weapon1;
    public Transform weapon2;

    private float shootingTimer;
    private bool isMovingRight = true;

    [Header("Movement Range")]
    public float minX = -200f; // Límite izquierdo
    public float maxX = 200f;  // Límite derecho

    [Header("Vidas")]
    public int vidas = 3; // Cantidad inicial de vidas.


    private List<GameObject> activeProjectiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        shootingTimer = Random.Range(0f, shootingInterval);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

    }

    // Update is called once per frame
    void Update()
    {
        StartFire();

        if (Random.Range(0f, 10f) < 0.03) // Probabilidad baja de cambio de dirección
        {
            isMovingRight = !isMovingRight;
            float newSpeed = isMovingRight ? speed : -speed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(newSpeed, 0f);
        }
       
    }

    public void StartFire()
    {
        _shootingTimer -= Time.deltaTime;
        if (_shootingTimer <= 0f)
        {
            _shootingTimer = shootingInterval;
            GameObject disparoInstance = Instantiate(prefabDisparo);
            disparoInstance.transform.SetParent(transform.parent);
            disparoInstance.transform.position = weapon1.position;

            disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
            Destroy(disparoInstance, timeDisparoDestroy);
            activeProjectiles.Add(disparoInstance);

            GameObject disparoInstance2 = Instantiate(prefabDisparo);
            disparoInstance2.transform.SetParent(transform.parent);
            disparoInstance2.transform.position = weapon2.position;

            disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
            Destroy(disparoInstance2, timeDisparoDestroy);
            activeProjectiles.Add(disparoInstance2);

        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //otherCollider.gameObject.GetComponent<DisparoBehaviour>()
        if (otherCollider.tag == "disparoPlayer" || otherCollider.tag == "Player")
        {

            // Restar una vida al jugador
            vidas--;
            OnDestroy();

            // Verificar si el jugador aún tiene vidas
            if (vidas <= 0)
            {
                //poner pantalla de game over
                gameObject.SetActive(false);
            }

            // Destruir el proyectil enemigo
            Destroy(otherCollider.gameObject);

            //gameObject.SetActive (false);
            //Destroy (otherCollider.gameObject);
        }
    }
    void OnDestroy()
    {
        // Cuando el enemigo sea destruido, destruye todas las balas activas
        foreach (GameObject projectile in activeProjectiles)
        {
            Destroy(projectile);
        }
    }
}
