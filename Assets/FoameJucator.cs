using UnityEngine;
using UnityEngine.UI;

public class FoameJucator : MonoBehaviour
{
    [Header("Setari Foame")]
    public float foameMaxima = 100f;
    public float foameCurenta;
    public float consumInTimp = 0.5f;

    [Header("Interfata UI")]
    public Image baraFoameUI;

    [Header("Legaturi")]
    public EnergieJucator scriptEnergie; // Trage scriptul EnergieJucator aici in Inspector

    void Start()
    {
        foameCurenta = foameMaxima;
    }

    void Update()
    {
        // Scadere constanta
        foameCurenta -= consumInTimp * Time.deltaTime;
        if (foameCurenta < 0f) foameCurenta = 0f;

        // Actualizare UI
        if (baraFoameUI != null)
        {
            baraFoameUI.fillAmount = foameCurenta / foameMaxima;
        }

        // Penalizare: Daca foamea e 0, energia se regenereaza mai greu
        if (foameCurenta <= 0f && scriptEnergie != null)
        {
            // Nota: Scriptul EnergieJucator va trebui actualizat ulterior 
            // pentru a suporta "rata de regenerare". Acum doar pregatim terenul.
            Debug.Log("Foame critica! Energia ar trebui sa stagneze.");
        }
    }

    public void Mananca(float cantitate)
    {
        foameCurenta += cantitate;
        if (foameCurenta > foameMaxima) foameCurenta = foameMaxima;
    }
}