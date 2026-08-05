using UnityEngine;
using System.Collections.Generic;

public class InventarJucator : MonoBehaviour
{
    // o structură în care să avem evidența obiectelor colectate
    [System.Serializable]
    public struct ObiectStocat
    {
        public TipObiect categorie;
        public string nume;
        public int lovituri;
        public float energie;
    }

    public List<ObiectStocat> inventar = new List<ObiectStocat>();

    public void AdaugaObiectInStorage(TipObiect categorie, string nume, int lovituri, float energie)
    {
        ObiectStocat obiectNou = new ObiectStocat
        {
            categorie = categorie,
            nume = nume,
            lovituri = lovituri,
            energie = energie
        };

        inventar.Add(obiectNou);

        Debug.Log("Item collected into inventory: " + nume + " | Category: " + categorie);
    }
}