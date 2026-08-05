using UnityEngine;
using UnityEngine.UI;

public class EnergieJucator : MonoBehaviour
{
    public float energieMaxima = 100f;
    public float energieCurenta;
    
    [Header("Interfata (UI)")]
    public Image baraEnergieUI; // Aici vom trage imaginea din Unity
    
    [Header("Consum Energie")]
    public float consumInTimp = 1f; // Câtă energie scade pe secundă doar existând

    void Start()
    {
        // La începutul jocului, energia este plină
        energieCurenta = energieMaxima;
    }

    void Update()
    {
        // 1. Scădem energia încet, în mod constant (în timp)
        ScadeEnergie(consumInTimp * Time.deltaTime);

        // 2. Actualizăm imaginea barei (regula de 3 simplă pentru a obține o valoare între 0 și 1)
        if (baraEnergieUI != null)
        {
            baraEnergieUI.fillAmount = energieCurenta / energieMaxima;
        }

        // Verificăm dacă a rămas fără energie (opțional)
        if (energieCurenta <= 0)
        {
            Debug.Log("Player ran out of energy! Game Over!");
            // Aici vom putea pune logica de moarte/restart
        }
    }

    // Funcție pe care o vom apela când se mișcă, atacă sau este lovit
    public void ScadeEnergie(float cantitate)
    {
        energieCurenta -= cantitate;
        
        // Să nu lăsăm energia să scadă sub 0
        if (energieCurenta < 0) 
        {
            energieCurenta = 0;
        }
    }

    // Funcție pe care o vom apela când mănâncă sau găsește un jurnal
    public void CresteEnergie(float cantitate)
    {
        energieCurenta += cantitate;
        
        // Să nu lăsăm energia să treacă de maxim
        if (energieCurenta > energieMaxima) 
        {
            energieCurenta = energieMaxima;
        }
    }
}