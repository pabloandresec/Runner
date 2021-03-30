using UnityEngine;
using UnityEngine.Animations;
using UnityEditor;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpriteCutterSettings" , menuName = "SpriteCutter/SpriteCutterSettings")]
public class SpriteCutterWindowData : ScriptableObject
{
    [SerializeField] private SpriteCutterSettings settings;
    AnimationClipOverrides clipOverrides;
    [SerializeField] private string path = "Assets/Prefabs/Clothing";

    public SpriteCutterSettings Settings { get => settings; }
    public string Path { get => path; }

    public void ProcessSprite()
    {
        Sprite[] sprites = ExtractSprites(settings.SpriteSheet, settings.Columns, settings.Rows);
        AnimationClip[] animations = CreateAnimations(sprites);
        PopulateAnimator(animations);
    }

    private void PopulateAnimator(AnimationClip[] clips)
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(settings.BaseAnimatorController);
        aoc.name = settings.PackName + "_AOC";
        clipOverrides = new AnimationClipOverrides(aoc.overridesCount);
        aoc.GetOverrides(clipOverrides);

        for (int i = 0; i < clips.Length; i++)
        {
            Debug.Log("Override " + i + " name = " + clips[i].name);
            clipOverrides[clips[i].name] = clips[i];
        }

        aoc.ApplyOverrides(clipOverrides);
        AssetDatabase.CreateAsset(aoc, path + "/" + settings.PackName + "/Animations/" + settings.PackName + ".overrideController");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void SaveSpritesToDisk(Sprite[] sprites)
    {
        if (string.IsNullOrEmpty(settings.PackName))
        {
            Debug.LogError("Error no se especifico el nombre del pack");
            return;
        }
        if(!AssetDatabase.IsValidFolder(path))
        {
            Debug.LogError("Path doesnt exist");
            return;
        }
        AssetDatabase.CreateFolder(path, settings.PackName);
        AssetDatabase.CreateFolder(path + "/" + settings.PackName, "Animations");
    }

    public Sprite[] ExtractSprites(Texture2D texture, int cols, int rows)
    {
        List<Sprite> s = new List<Sprite>();

        string path = AssetDatabase.GetAssetPath(texture);

        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter; //Obtiene el path de la texture
        TextureImporterSettings tISet = new TextureImporterSettings();
        ti.ReadTextureSettings(tISet);
        tISet.spriteAlignment = (int)SpriteAlignment.Custom;


        ti.isReadable = true;
        ti.filterMode = FilterMode.Point;
        ti.spritePixelsPerUnit = 32;
        ti.spriteImportMode = SpriteImportMode.Multiple;

        List<SpriteMetaData> metasList = new List<SpriteMetaData>();

        int sIndex = 0;
        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
            {
                SpriteMetaData meta = new SpriteMetaData();
                Rect loc = GetSpriteLocation(texture, new Vector2Int(x, y), new Vector2Int(cols, rows));
                meta.rect = loc;
                meta.alignment = 7;
                meta.pivot = new Vector2(0.5f, 0f);
                meta.name = sIndex.ToString();
                metasList.Add(meta);
                sIndex++;
            }

        ti.spritesheet = metasList.ToArray();
        ti.SetTextureSettings(tISet);

        Debug.Log("Se Guardaron " + ti.spritesheet.Length + " sprites");
        ti.SaveAndReimport();
        UnityEngine.Object[] objects = AssetDatabase.LoadAllAssetsAtPath(path);

        foreach (UnityEngine.Object o in objects)
        {
            if (o.GetType() == typeof(Sprite))
            {
                //Debug.Log("Sprite name = " + o.name);
                s.Add((Sprite)o);
            }
        }

        AssetDatabase.SaveAssets();
        return s.ToArray();
    }

    public AnimationClip[] CreateAnimations(Sprite[] sprites)
    {
        if (string.IsNullOrEmpty(settings.PackName))
        {
            Debug.LogError("Error no se especifico el nombre del pack");
            return null;
        }
        if (!AssetDatabase.IsValidFolder(path))
        {
            Debug.LogError("Path doesnt exist");
            return null;
        }
        AssetDatabase.CreateFolder(path, settings.PackName);
        string rootFolder = path + "/" + settings.PackName;
        AssetDatabase.CreateFolder(rootFolder, "Animations");
        string animFolder = rootFolder + "/Animations";

        Debug.Log("Creando " + settings.Animations.Length + " animaciones con " + sprites.Length + " sprites");
        AnimationClip[] clips = new AnimationClip[settings.Animations.Length];

        for (int i = 0; i < settings.Animations.Length; i++)
        {
            Debug.LogWarning("PROCCESING " + settings.Animations[i].name);
            int[] spritesIndexes = GetAnimationIndexes(i);
            Debug.Log(settings.Animations[i].name + " has " + spritesIndexes.Length + " frames");

            AnimationClip ac = new AnimationClip();
            ac.name = settings.Animations[i].name;
            ac.frameRate = settings.Animations[i].fps;

            EditorCurveBinding spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = "";
            spriteBinding.propertyName = "m_Sprite";

            float baseTime = 0;
            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[spritesIndexes.Length];

            for (int s = 0; s < spritesIndexes.Length; s++)
            {
                spriteKeyFrames[s] = new ObjectReferenceKeyframe();
                spriteKeyFrames[s].time = baseTime;
                spriteKeyFrames[s].value = sprites[spritesIndexes[s]];
                baseTime += 0.0833333333333333333f;  //.250f default
            }
            AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(ac);
            setting.loopTime = settings.Animations[i].loop;


            AnimationUtility.SetAnimationClipSettings(ac, setting);
            AnimationUtility.SetObjectReferenceCurve(ac, spriteBinding, spriteKeyFrames);
            AssetDatabase.CreateAsset(ac, animFolder + "/" + ac.name + ".anim");
            Debug.Log("Asset "+ ac.name + " Created!");
            clips[i] = (AnimationClip)AssetDatabase.LoadAssetAtPath(animFolder + "/" + ac.name + ".anim", typeof(AnimationClip));
            AssetDatabase.SaveAssets();
        }
        AssetDatabase.Refresh();

        Debug.Log("Getting all clips");
        return clips;
    }

    private int[] GetAnimationIndexes(int animationIndex)
    {
        List<int> indexes = new List<int>();
        int currentIndex = settings.Animations[animationIndex].from;
        Debug.Log(settings.Animations[animationIndex].name + " indices are:");
        while (currentIndex <= settings.Animations[animationIndex].to)
        {
            Debug.Log("i: " + currentIndex);
            indexes.Add(currentIndex);
            currentIndex++;
        }
        return indexes.ToArray();
    }

    private Rect GetSpriteLocation(Texture2D tex, Vector2Int index, Vector2Int colsRows)
    {
        int spriteWidth = tex.width / colsRows.x;
        int spriteHeight = tex.height / colsRows.y;

        Rect temp = new Rect(index.x * spriteWidth, index.y * spriteHeight, spriteWidth, spriteHeight);

        return temp;
    }

    /*
    private Sprite[] CutSheet(Texture2D tex)
    {
        int amountOfSprites = settings.Columns * settings.Rows;
        int spriteWidth = settings.SpriteWidth;
        int spriteHeight = settings.SpriteHeight;

        Sprite[] sprites = new Sprite[amountOfSprites];
        for (int i = 0; i < amountOfSprites; i++)
        {
            Rect calcRect = GetSpriteRectByIndex(i, tex.width, spriteWidth, spriteHeight);
            sprites[i] = Sprite.Create(tex, calcRect, settings.Pivot);
        }
        return sprites;
    }

    private Rect GetSpriteRectByIndex(int i, int textureWidth, int spriteWidth, int spriteHeight)
    {
        Vector2Int pos = (Vector2Int)IndexToGridPos(i, textureWidth);
        Rect rect = new Rect(pos, new Vector2(spriteWidth, spriteHeight));
        return rect;
    }
    */
    public static bool IsInMapBounds(Vector2Int mapSize, Vector2Int pos)
    {
        return pos.x < mapSize.x && pos.x >= 0 && pos.y < mapSize.y && pos.y >= 0;
    }

    public static int GridPosToIndex(int x, int y, int width)
    {
        int index = x + y * width;
        return index;
    }

    public static Vector3Int IndexToGridPos(int indx, int width)
    {
        int y = (int)(indx / width);
        int x = indx - (y * width);
        return new Vector3Int(x, y, 0);
    }
}
