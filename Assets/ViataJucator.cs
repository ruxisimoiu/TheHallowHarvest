using UnityEngine;
using UnityEngine.UI;

public class SanatateJucator : MonoBehaviour
{
    [Header("Setari Viata")]
    public float viataMaxima = 100f;
    public float viataCurenta;

    [Header("Interfata UI")]
    public Image baraViataUI; // Trage imaginea 'viata_fill' aici

    void Start()
    {
        // La pornirea jocului, viata este plina
        viataCurenta = viataMaxima;
        ActualizeazaUI();
    }

    void Update()
    {
        // Tasta de test: Apasa 'H' in timpul jocului ca sa simulezi ca iei 15 damage
        if (Input.GetKeyDown(KeyCode.H))
        {
            PrimesteDaune(15f);
        }
    }

    public void PrimesteDaune(float daune)
    {
        viataCurenta -= daune;

        if (viataCurenta < 0f)
        {
            viataCurenta = 0f;
        }

        ActualizeazaUI();

        if (viataCurenta <= 0f)
        {
            Debug.Log("Jucatorul a murit! GAME OVER!");
            // Aici vom putea opri miscarea sau declansa ecranul de Game Over
        }
    }

    public void Vindeca(float cantitate)
    {
        viataCurenta += cantitate;

        if (viataCurenta > viataMaxima)
        {
            viataCurenta = viataMaxima;
        }

        ActualizeazaUI();
        Debug.Log($"Jucatorul s-a vindecat cu {cantitate} puncte de viata.");
    }

    private void ActualizeazaUI()
    {
        if (baraViataUI != null)
        {
            baraViataUI.fillAmount = viataCurenta / viataMaxima;
        }
    }
}