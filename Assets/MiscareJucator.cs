using UnityEngine;

public class MiscareJucator : MonoBehaviour
{
    public float viteza = 5f;

    [Header("Imagine Static (Idle)")]
    public Sprite imagineFata;

    [Header("Imagini Alergare Sus (Spate)")]
    public Sprite cadruSus1;
    public Sprite cadruSus2;

    [Header("Imagini Alergare Jos (Fata)")]
    public Sprite cadruJos1;
    public Sprite cadruJos2;

    [Header("Imagini Alergare Dreapta")]
    public Sprite cadruDreapta1;
    public Sprite cadruDreapta2;

    [Header("Imagini Alergare Stanga")]
    public Sprite cadruStanga1;
    public Sprite cadruStanga2;

    [Header("Setari Animatie")]
    public float vitezaAnimatie = 0.15f; 
    
    private float timerAnimatie;
    private bool arataCadrul1 = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr; 
    private float miscareOrizontala;
    private float miscareVerticala;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        
        if (sr.sprite == null) 
        {
            sr.sprite = imagineFata;
        }
    }

    void Update()
    {
        miscareOrizontala = Input.GetAxisRaw("Horizontal");
        miscareVerticala = Input.GetAxisRaw("Vertical"); 

        // 1. Mișcare DREAPTA
        if (miscareOrizontala > 0) 
        {
            sr.flipX = false;
            AplicaAnimatieCadre(cadruDreapta1, cadruDreapta2);
        }
        // 2. Mișcare STÂNGA
        else if (miscareOrizontala < 0) 
        {
            sr.flipX = true;
            AplicaAnimatieCadre(cadruStanga1, cadruStanga2);
        }
        // 3. Mișcare SUS (Spate)
        else if (miscareVerticala > 0) 
        {
            sr.flipX = false;
            AplicaAnimatieCadre(cadruSus1, cadruSus2);
        }
        // 4. Mișcare JOS (Față)
        else if (miscareVerticala < 0) 
        {
            sr.flipX = false;
            AplicaAnimatieCadre(cadruJos1, cadruJos2);
        }
        // 5. Stă pe loc -> Se întoarce la imaginea statică de față
        else
        {
            sr.flipX = false;
            sr.sprite = imagineFata;
            timerAnimatie = 0f;
            arataCadrul1 = true;
        }
    }

    void AplicaAnimatieCadre(Sprite c1, Sprite c2)
    {
        timerAnimatie += Time.deltaTime; 

        if (timerAnimatie >= vitezaAnimatie)
        {
            timerAnimatie = 0f;
            arataCadrul1 = !arataCadrul1;
        }

        sr.sprite = arataCadrul1 ? c1 : c2;
    }

    void FixedUpdate()
    {
        Vector2 directie = new Vector2(miscareOrizontala, miscareVerticala);
        rb.linearVelocity = directie.normalized * viteza;
    }
}