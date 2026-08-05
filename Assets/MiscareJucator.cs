using UnityEngine;

public class MiscareJucator : MonoBehaviour
{
    public float viteza = 5f;

    [Header("Imagini Verticale (Sus/Jos)")]
    public Sprite imagineFata;
    public Sprite imagineSpate;

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

        // 1. Verificăm mișcarea spre DREAPTA
        if (miscareOrizontala > 0) 
        {
            sr.flipX = false; // Imaginea rămâne normală
            timerAnimatie += Time.deltaTime; 

            if (timerAnimatie >= vitezaAnimatie)
            {
                timerAnimatie = 0f;
                arataCadrul1 = !arataCadrul1;
            }

            if (arataCadrul1) sr.sprite = cadruDreapta1;
            else sr.sprite = cadruDreapta2;
        }
        // 2. Verificăm mișcarea spre STÂNGA
        else if (miscareOrizontala < 0) 
        {
            sr.flipX = true; // OGLINDIM imaginea pentru stânga!
            timerAnimatie += Time.deltaTime; 

            if (timerAnimatie >= vitezaAnimatie)
            {
                timerAnimatie = 0f;
                arataCadrul1 = !arataCadrul1;
            }

            if (arataCadrul1) sr.sprite = cadruStanga1;
            else sr.sprite = cadruStanga2;
        }
        // 3. Sus/Jos (Imagini statice)
        else if (miscareVerticala > 0) 
        {
            sr.flipX = false; // Resetăm oglindirea
            sr.sprite = imagineSpate;
            timerAnimatie = 0f; 
        }
        else if (miscareVerticala < 0) 
        {
            sr.flipX = false; // Resetăm oglindirea
            sr.sprite = imagineFata;
            timerAnimatie = 0f; 
        }
        // 4. Stă pe loc
        else
        {
            sr.flipX = false;
            sr.sprite = imagineFata;
            timerAnimatie = 0f;
            arataCadrul1 = true;
        }
    }

    void FixedUpdate()
    {
        Vector2 directie = new Vector2(miscareOrizontala, miscareVerticala);
        rb.linearVelocity = directie.normalized * viteza;
    }
}