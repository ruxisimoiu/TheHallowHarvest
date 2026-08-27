using UnityEngine;

public class Inamic : MonoBehaviour
{
    [Header("Statistici Inamic")]
    public float viataMaxima = 50f;
    public float viataCurenta;

    void Start()
    {
        viataCurenta = viataMaxima;
    }

    public void PrimesteLovitura(float daune)
    {
        viataCurenta -= daune;
        Debug.Log($"<color=red>[INAMIC]</color> Lovit cu {daune} damage! Viata ramasa: {viataCurenta}/{viataMaxima}");

        if (viataCurenta <= 0f)
        {
            Debug.Log("<color=red>[INAMIC]</color> Inamicul a fost invins!");
            Destroy(gameObject);
        }
    }
}