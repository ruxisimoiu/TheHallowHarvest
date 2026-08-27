using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

public class GenereazaMascaLumina : MonoBehaviour
{
    [MenuItem("Tools/Genereaza Textura Intuneric")]
    public static void CreeazaTextura()
    {
        int size = 1024;
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Vector2 center = new Vector2(size / 2f, size / 2f);
        float radius = size * 0.02f; // Raza clara devine mult mai mica
        float fade = size * 0.20f;   // Tranzitie scurta si stransa spre beznă

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center);
                float alpha = Mathf.Clamp01((dist - radius) / fade);
                tex.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
            }
        }

        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        string path = Application.dataPath + "/Sprites/MascaIntuneric.png";
        
        if (!Directory.Exists(Application.dataPath + "/Sprites"))
            Directory.CreateDirectory(Application.dataPath + "/Sprites");

        File.WriteAllBytes(path, bytes);
        AssetDatabase.Refresh();

        // Configuram textura automat ca Sprite
        string relativePath = "Assets/Sprites/MascaIntuneric.png";
        TextureImporter importer = AssetImporter.GetAtPath(relativePath) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.SaveAndReimport();
        }

        Debug.Log("Textura de intuneric a fost generata cu succes in Assets/Sprites/MascaIntuneric.png!");
    }
}
#endif