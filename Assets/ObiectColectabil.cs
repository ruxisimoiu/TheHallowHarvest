using UnityEngine;

public enum TipObiect { Mancare, Cutit, Pusca }

public class ObiectColectabil : MonoBehaviour
{
    [Header("Ce fel de obiect este?")]
    public TipObiect categoriaObiectului;
    public string numeObiect = "Nume Obiect";
    
    [Header("Dacă este armă:")]
    public int numarLovituri = 0; 
    
    [Header("Dacă este mâncare:")]
    public float energieOferita = 0f;

    private bool jucatorulEInZona = false;
    private InventarJucator inventarulLui;

    void Update()
    {
        // Jucătorul apasă E când este deasupra obiectului
        if (jucatorulEInZona == true && Input.GetKeyDown(KeyCode.E))
        {
            BagaInStorage();
        }
    }

    void OnTriggerEnter2D(Collider2D altObiect)
    {
        if (altObiect.CompareTag("Player"))
        {
            jucatorulEInZona = true;
            inventarulLui = altObiect.GetComponent<InventarJucator>();
            
            // Mesajul tradus în engleză
            Debug.Log("Press E to pick up: " + numeObiect);
        }
    }

    void OnTriggerExit2D(Collider2D altObiect)
    {
        if (altObiect.CompareTag("Player"))
        {
            jucatorulEInZona = false;
            inventarulLui = null;
        }
    }

    void BagaInStorage()
    {
        if (inventarulLui != null)
        {
            inventarulLui.AdaugaObiectInStorage(categoriaObiectului, numeObiect, numarLovituri, energieOferita);
            
            // Distrugem obiectul de pe jos
            Destroy(gameObject);
        }
    }
}