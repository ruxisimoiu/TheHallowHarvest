using UnityEngine;

public class InventarJucator : MonoBehaviour
{
    // Aici vom adăuga mai târziu o listă reală și legătura cu butoanele de UI
    
    public void AdaugaObiectInStorage(TipObiect categorie, string nume, int utilizari, float energie)
    {
        Debug.Log("Added to storage: " + nume);
        
        if (categorie == TipObiect.Cutit || categorie == TipObiect.Pusca)
        {
            Debug.Log("It's a weapon with " + utilizari + " uses left.");
        }
        else if (categorie == TipObiect.Mancare)
        {
            Debug.Log("It's food that will provide " + energie + " energy when consumed.");
        }
        
        // Aici va urma codul care să facă să apară iconița în interfața ta vizuală!
    }
}