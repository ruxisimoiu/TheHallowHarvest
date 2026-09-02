using UnityEngine;
using UnityEngine.UI;

public class SeteJucator : MonoBehaviour
{
    [Header("Setari Sete")]
    public float seteMaxima = 100f;
    public float seteCurenta;
    public float consumInTimp = 0.8f; // Scade pe secunda
    public float dauneDeshidratare = 2f; // Cat HP pierzi pe secunda cand setea e 0

    [Header("Interfata UI")]
    public Image baraSeteUI;

    [Header("Legaturi")]
    public SanatateJucator scriptSanatate; // Trage scriptul SanatateJucator aici in Inspector

    void Start()
    {
        seteCurenta = seteMaxima;
    }

    void Update()
    {
        // Scadere constanta
        seteCurenta -= consumInTimp * Time.deltaTime;
        if (seteCurenta < 0f) seteCurenta = 0f;

        // Actualizare UI
        if (baraSeteUI != null)
        {
            baraSeteUI.fillAmount = seteCurenta / seteMaxima;
        }

        // Penalizare: Scade viata treptat daca setea e 0
        if (seteCurenta <= 0f && scriptSanatate != null)
        {
            scriptSanatate.PrimesteDaune(dauneDeshidratare * Time.deltaTime);
        }
    }

    public void Bea(float cantitate)
    {
        seteCurenta += cantitate;
        if (seteCurenta > seteMaxima) seteCurenta = seteMaxima;
    }
}