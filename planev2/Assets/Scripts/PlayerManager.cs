using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;
    public float speedY = 2f;
    [Header("Limites")]
    public float limiteX = 10f;

    [Header("Disparo")] 
    public GameObject prefabDisparo;
    public float disparoSpeed =2f;
    public float timeDisparoDestroy = 2f;

    [Header("Vidas")]
    public int vidas = 3; // Cantidad inicial de vidas.
    public TextMeshProUGUI vidasTextMesh;

    public Transform weapon1;
    public Transform weapon2;
    private bool isFire = false;
    private Rigidbody2D rb;

    private float timeSinceLastShot = 0f;
    public float tiempoEntreDisparos = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        ActualizarTextoDeVidas();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        // Controla el tiempo entre disparos
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetAxis("Fire1") == 1f && timeSinceLastShot >= tiempoEntreDisparos)
        {
            Fire();
            timeSinceLastShot = 0f; // Reinicia el contador de tiempo
        }
    }
    public void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, speedY);
        if (transform.position.x > limiteX)
        {
            transform.position = new Vector2(limiteX, transform.position.y);
        }
        else if(transform.position.x < -limiteX)
        {
            transform.position = new Vector2(-limiteX, transform.position.y);
        }
    }

    public void Fire()
    {
        if (Input.GetAxis("Fire1") == 1f)
        {
            if (!isFire)
            {
                isFire = true;
                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);
                
                disparoInstance.transform.position = weapon1.position;
                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance,timeDisparoDestroy);
                
                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;
                //Move
                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2,timeDisparoDestroy);
            }
            else
            {
                isFire = false;
            }
        }

  
      

    }
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "disparoEnemigo")
        {
            // Restar una vida al jugador
            vidas--;

            // Verificar si el jugador aún tiene vidas
            if (vidas <= 0)
            {
                //poner pantalla de game over
                gameObject.SetActive(false);
            }

            // Destruir el proyectil enemigo
            Destroy(otherCollider.gameObject);
            ActualizarTextoDeVidas();



        }
    }

    void ActualizarTextoDeVidas()
    {
        if (vidasTextMesh != null)
        {
            vidasTextMesh.text = "Vidas: " + vidas.ToString();
        }
    }
}
