using UnityEngine;

public enum TipObiect
{
    Cutit,
    Pusca,
    Mancare
}

[RequireComponent(typeof(Collider2D))]
public class ObiectColectabil : MonoBehaviour
{
    [Header("Date Obiect")]
    public TipObiect categorie;
    public string nume = "Obiect";
    public int lovituri = 3;
    public float energie = 25f;

    [Header("Vizual")]
    public SpriteRenderer spriteRenderer;

    private bool jucatorInZona = false;
    private InventarJucator inventarJucator;

    void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.GetComponent<InventarJucator>() != null)
        {
            jucatorInZona = true;
            inventarJucator = other.GetComponent<InventarJucator>();
            Debug.Log($"Apasa 'E' pentru a lua: {nume}");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.GetComponent<InventarJucator>() != null)
        {
            jucatorInZona = false;
            inventarJucator = null;
        }
    }

    void Update()
    {
        // Ridicare de pe jos cu tasta E
        if (jucatorInZona && Input.GetKeyDown(KeyCode.E) && inventarJucator != null)
        {
            bool adaugat = inventarJucator.AdaugaObiectInStorage(categorie, nume, lovituri, energie);
            if (adaugat)
            {
                Destroy(gameObject);
            }
        }
    }
}