using UnityEngine;
using System.Collections.Generic;

public class InventarJucator : MonoBehaviour
{
    [System.Serializable]
    public struct ObiectStocat
    {
        public TipObiect categorie;
        public string nume;
        public int lovituri; // Durabilitate
        public float energie;
    }

    [Header("Inventar")]
    public List<ObiectStocat> inventar = new List<ObiectStocat>();

    [Header("Setari Lupta")]
    public Transform punctAtac;
    public float razaAtacMelee = 1.5f;
    public float daunePumn = 10f;
    public float dauneCutit = 30f;
    public float daunePusca = 60f;

    [Header("Prefab-uri pentru Drop pe Jos (Q)")]
    public GameObject prefabCutit;
    public GameObject prefabPusca;
    public GameObject prefabMancare;

    [Header("Conexiuni")]
    public InventarUI inventarUI;
    private EnergieJucator energieComponent;

    void Start()
    {
        energieComponent = GetComponent<EnergieJucator>();

        if (inventarUI == null)
        {
            inventarUI = Object.FindFirstObjectByType<InventarUI>();
        }

        if (punctAtac == null)
        {
            punctAtac = transform;
        }
    }

    void Update()
    {
        // 1. DROP (Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AruncaObiectSelectat();
        }

        // 2. ATAC (Click Stanga)
        if (Input.GetMouseButtonDown(0))
        {
            ExecutaAtac();
        }

        // 3. CONSUMA MANCARE (Click Dreapta)
        if (Input.GetMouseButtonDown(1))
        {
            ConsumaMancareSelectata();
        }
    }

    public bool AdaugaObiectInStorage(TipObiect categorie, string nume, int lovituri, float energie)
    {
        if (inventar.Count >= 5)
        {
            Debug.Log("Storage plin!");
            return false;
        }

        ObiectStocat obiectNou = new ObiectStocat
        {
            categorie = categorie,
            nume = nume,
            lovituri = lovituri,
            energie = energie
        };

        inventar.Add(obiectNou);

        if (inventarUI != null)
        {
            inventarUI.AdaugaObiectVizual(categorie, inventar.Count - 1);
        }

        Debug.Log($"<color=green>[COLECTAT]</color> {nume} (Durabilitate: {lovituri}) in slotul #{inventar.Count}");
        return true;
    }

    private void ExecutaAtac()
    {
        int slot = (inventarUI != null) ? inventarUI.slotSelectat : 0;

        // Daca slotul curent are un item in inventar
        if (slot >= 0 && slot < inventar.Count)
        {
            ObiectStocat arma = inventar[slot];

            if (arma.categorie == TipObiect.Cutit)
            {
                AtacCutit(slot, arma);
                return;
            }
            else if (arma.categorie == TipObiect.Pusca)
            {
                AtacPusca(slot, arma);
                return;
            }
            else if (arma.categorie == TipObiect.Mancare)
            {
                Debug.Log("<color=yellow>[INFO]</color> Ai mancare in mana! Foloseste Click Dreapta ca sa o consumi.");
                return;
            }
        }

        // Daca nu avem arma in slotul selectat -> Dam cu PUMNUL
        AtacPumn();
    }

    private void AtacPumn()
    {
        Debug.Log("<color=yellow>[ATAC PUMN]</color> Ai dat un pumn!");
        VerificaLovituraInamici(daunePumn);
    }

    private void AtacCutit(int slotIndex, ObiectStocat arma)
    {
        // 1. Verificam mai intai daca am lovit un inamic
        bool aLovit = VerificaLovituraInamici(dauneCutit);

        if (aLovit)
        {
            // Scadem durabilitatea DOAR daca lovitura a atins o tinta valida
            arma.lovituri -= 1;
            inventar[slotIndex] = arma; // Salvam noua durabilitate in lista

            Debug.Log($"<color=orange>[HIT REUSIT - CUTIT]</color> Inamic atins! Durabilitate ramasa: {arma.lovituri}");

            // Verificam daca arma s-a rupt
            if (arma.lovituri <= 0)
            {
                Debug.Log("<color=red>[CUTIT DISTRUS]</color> Cutitul s-a rupt dupa lovitura!");
                inventar.RemoveAt(slotIndex);
                ReincarcaToateSloturileUI();
            }
        }
        else
        {
            Debug.Log("<color=grey>[SWING IN GOL]</color> Ai dat cu cutitul in aer. Nicio durabilitate pierduta.");
        }
    }

    private void AtacPusca(int slotIndex, ObiectStocat arma)
    {
        // Daca vrei ca si la pusca sa scada doar cand nimeresti un inamic:
        bool aLovit = VerificaLovituraInamici(daunePusca);

        if (aLovit)
        {
            arma.lovituri -= 1;
            inventar[slotIndex] = arma;

            Debug.Log($"<color=cyan>[HIT REUSIT - PUSCA]</color> Tinta lovita! Gloante/durabilitate ramasa: {arma.lovituri}");

            if (arma.lovituri <= 0)
            {
                Debug.Log("<color=red>[PUSCA DESCARCATA]</color> Arma a ramas fara munitie!");
                inventar.RemoveAt(slotIndex);
                ReincarcaToateSloturileUI();
            }
        }
        else
        {
            Debug.Log("<color=grey>[TRAGERE RATATA]</color> Foc tras in gol. Nu a consumat durabilitate.");
        }
    }


    private bool VerificaLovituraInamici(float daune)
    {
        Vector3 origine = (punctAtac != null) ? punctAtac.position : transform.position;
        Collider2D[] tinteAtinge = Physics2D.OverlapCircleAll(origine, razaAtacMelee);
        bool gasitInamic = false;

        foreach (Collider2D tinta in tinteAtinge)
        {
            if (tinta.gameObject == gameObject) continue;

            Inamic inamic = tinta.GetComponent<Inamic>();
            if (inamic != null)
            {
                inamic.PrimesteLovitura(daune);
                gasitInamic = true;
            }
        }

        return gasitInamic;
    }

    private void ConsumaMancareSelectata()
    {
        if (inventarUI == null) return;
        int slot = inventarUI.slotSelectat;

        if (slot >= 0 && slot < inventar.Count)
        {
            ObiectStocat obiect = inventar[slot];

            if (obiect.categorie == TipObiect.Mancare)
            {
                if (energieComponent != null)
                {
                    energieComponent.CresteEnergie(obiect.energie);
                }

                Debug.Log($"<color=green>[CONSUMAT]</color> Ai mancat {obiect.nume}! +{obiect.energie} Energie.");
                inventar.RemoveAt(slot);
                ReincarcaToateSloturileUI();
            }
        }
    }

    private void AruncaObiectSelectat()
    {
        if (inventarUI == null) return;
        int slot = inventarUI.slotSelectat;

        if (slot >= 0 && slot < inventar.Count)
        {
            ObiectStocat obiect = inventar[slot];
            GameObject prefabDeCreat = null;

            switch (obiect.categorie)
            {
                case TipObiect.Cutit: prefabDeCreat = prefabCutit; break;
                case TipObiect.Pusca: prefabDeCreat = prefabPusca; break;
                case TipObiect.Mancare: prefabDeCreat = prefabMancare; break;
            }

            if (prefabDeCreat != null)
            {
                Vector3 pozitieDrop = transform.position + new Vector3(0.8f, 0, 0);
                GameObject obiectNou = Instantiate(prefabDeCreat, pozitieDrop, Quaternion.identity);
                obiectNou.transform.localScale = new Vector3(0.3f, 0.3f, 1f);

                ObiectColectabil oc = obiectNou.GetComponent<ObiectColectabil>();
                if (oc != null) oc.lovituri = obiect.lovituri;
            }

            inventar.RemoveAt(slot);
            ReincarcaToateSloturileUI();
        }
    }

    public void ReincarcaToateSloturileUI()
    {
        if (inventarUI == null) return;

        inventarUI.GolesteToateSloturile();
        for (int i = 0; i < inventar.Count; i++)
        {
            inventarUI.AdaugaObiectVizual(inventar[i].categorie, i);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = (punctAtac != null) ? punctAtac.position : transform.position;
        Gizmos.DrawWireSphere(pos, razaAtacMelee);
    }
}