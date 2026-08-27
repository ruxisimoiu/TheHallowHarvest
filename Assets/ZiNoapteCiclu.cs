using UnityEngine;

public class CicluZiNoapteDirect : MonoBehaviour
{
    [Header("Referinta Masca")]
    public SpriteRenderer mascaLumina; // Trage obiectul 'lumina' aici

    [Header("Configurare Timp")]
    public float durataZiInSecunde = 120f;

    [Header("Culoare Noapte")]
    public Color culoareNoapte = new Color(0.03f, 0.05f, 0.1f, 1f); // Bleumarin inchis

    [Range(0f, 1f)]
    public float progresTimp = 0f;

    void Update()
    {
        progresTimp += Time.deltaTime / durataZiInSecunde;
        if (progresTimp >= 1f) progresTimp = 0f;

        ActualizeazaMasca();
    }

    void OnValidate()
    {
        ActualizeazaMasca();
    }

    private void ActualizeazaMasca()
    {
        if (mascaLumina == null) return;

        float alpha = 0f;

        // Ziua (0 - 0.4): Alpha = 0 (fara intuneric)
        // Apus/Seara (0.4 - 0.55): Tranzitie lina spre noapte
        // Noapte completa (0.55 - 0.85): Alpha = 1 (intuneric dens + cercul de lumina)
        // Rasarit (0.85 - 1.0): Revenire la zi
        if (progresTimp >= 0.4f && progresTimp <= 0.95f)
        {
            if (progresTimp < 0.55f)
                alpha = Mathf.InverseLerp(0.4f, 0.55f, progresTimp);
            else if (progresTimp > 0.85f)
                alpha = Mathf.InverseLerp(0.95f, 0.85f, progresTimp);
            else
                alpha = 1f;
        }

        Color c = culoareNoapte;
        c.a = alpha;
        mascaLumina.color = c;
    }
}