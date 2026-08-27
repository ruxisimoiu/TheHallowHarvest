using UnityEngine;
using UnityEngine.UI;

public class InventarUI : MonoBehaviour
{
    [Header("Sloturile din Storage (1 - 5)")]
    public Image[] sloturi = new Image[5];

    [Header("Indicator Selectie Slot")]
    public RectTransform chenarSelectie;

    [Header("Sprite-uri Obiecte")]
    public Sprite spriteCutit;
    public Sprite spritePusca;
    public Sprite spriteMancare;

    public int slotSelectat = 0;

    void Start()
    {
        ActualizeazaPozitieSelectie();
    }

    void Update()
    {
        // 1. Schimbare slot cu rotita de la mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0f)
        {
            slotSelectat++;
            if (slotSelectat >= sloturi.Length) slotSelectat = 0;
            ActualizeazaPozitieSelectie();
        }
        else if (scroll > 0f)
        {
            slotSelectat--;
            if (slotSelectat < 0) slotSelectat = sloturi.Length - 1;
            ActualizeazaPozitieSelectie();
        }

        // 2. Schimbare rapida prin tastele 1 - 5
        if (Input.GetKeyDown(KeyCode.Alpha1)) SeteazaSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SeteazaSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SeteazaSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SeteazaSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SeteazaSlot(4);
    }

    public void SeteazaSlot(int index)
    {
        if (index >= 0 && index < sloturi.Length)
        {
            slotSelectat = index;
            ActualizeazaPozitieSelectie();
        }
    }

    private void ActualizeazaPozitieSelectie()
    {
        if (chenarSelectie != null && sloturi[slotSelectat] != null)
        {
            chenarSelectie.position = sloturi[slotSelectat].transform.position;
        }
    }

    public void AdaugaObiectVizual(TipObiect categorie, int indexSlot)
    {
        if (indexSlot < 0 || indexSlot >= sloturi.Length) return;

        Image slotCurent = sloturi[indexSlot];
        if (slotCurent == null) return;

        Sprite spriteDeAfisat = null;
        switch (categorie)
        {
            case TipObiect.Cutit: spriteDeAfisat = spriteCutit; break;
            case TipObiect.Pusca: spriteDeAfisat = spritePusca; break;
            case TipObiect.Mancare: spriteDeAfisat = spriteMancare; break;
        }

        if (spriteDeAfisat != null)
        {
            slotCurent.sprite = spriteDeAfisat;
            Color c = slotCurent.color;
            c.a = 1f;
            slotCurent.color = c;
        }
    }

    public void GolesteToateSloturile()
    {
        foreach (Image slot in sloturi)
        {
            if (slot != null)
            {
                slot.sprite = null;
                Color c = slot.color;
                c.a = 0f; // Il facem invizibil
                slot.color = c;
            }
        }
    }
}