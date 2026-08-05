using UnityEngine;

public class MiscareJucator : MonoBehaviour
{
    public float viteza = 5f;
    private Rigidbody2D rb;
    private float miscareOrizontala;
    private float miscareVerticala;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Preia comenzi de la tastatură pentru toate cele 4 direcții
        miscareOrizontala = Input.GetAxisRaw("Horizontal");
        miscareVerticala = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Creează direcția finală de mișcare
        Vector2 directie = new Vector2(miscareOrizontala, miscareVerticala);
        
        // normalizat (normalized) previne mișcarea mai rapidă pe diagonală
        rb.linearVelocity = directie.normalized * viteza;
    }
}