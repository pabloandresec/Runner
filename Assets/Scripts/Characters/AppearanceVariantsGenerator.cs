using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AppearanceVariantsGenerator : MonoBehaviour
{
    [SerializeField] private UnlockeableData unlocks;
    [SerializeField] private GameObject diplayerPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private string spriteName;
    [SerializeField] private Texture2D[] baseTextures;
    [SerializeField] private Texture2D[] hairTextures;

    public void ShowVariations()
    {
        AppearanceDisplayer[] displayers = GetComponentsInChildren<AppearanceDisplayer>();

        if(unlocks.Data.UnlockedVariations.Count != 0)
        {
            foreach (string combination in unlocks.Data.UnlockedVariations)
            {
                AppearanceDisplayer a = displayers.FirstOrDefault(c => c.transform.name == combination);
                if(a != null)
                {
                    a.ShowGraphic();
                }
            }
        }
    }

    public void GenerateVariants()
    {
        foreach (Texture2D clothText in baseTextures)
        {
            Sprite baseCloth = GetSpecificSpriteFromTexture(spriteName, clothText);
            foreach (Texture2D hairT in hairTextures)
            {
                Sprite hairLayer = GetSpecificSpriteFromTexture(spriteName, hairT);
                GameObject go = Instantiate(diplayerPrefab, contentParent, false);
                go.GetComponent<AppearanceDisplayer>().SetSprite(0, baseCloth);
                go.GetComponent<AppearanceDisplayer>().SetSprite(1, hairLayer);
                go.transform.name = baseCloth.texture.name + "_" + hairLayer.texture.name;
            }
        }
    }

    private Sprite GetSpecificSpriteFromTexture(string name, Texture2D text)
    {
        Sprite selected = null;
        string s = AssetDatabase.GetAssetPath(text);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(s).OfType<Sprite>().ToArray();
        foreach (Sprite selectedSprite in sprites)
        {
            if (selectedSprite.name == spriteName)
            {
                selected = selectedSprite;
                break;
            }
        }
        return selected;
    }
}
