using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct SpriteCutterSettings
{
    [SerializeField] private string packName;
    [SerializeField] private Texture2D spriteSheet;
    [Min(1)]
    [SerializeField] private int columns;
    [Min(1)]
    [SerializeField] private int rows;
    [SerializeField] private int ppu;
    [SerializeField] private Vector2 pivot;
    [SerializeField] private RuntimeAnimatorController baseAnimatorController;
    [SerializeField] private CustomAnimationRange[] animations;

    public int SpriteWidth { get => spriteSheet.width / columns; }
    public int SpriteHeight { get => spriteSheet.height / rows; }

    public string PackName { get => packName; set => packName = value; }
    public Texture2D SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
    public int Columns { get => columns; set => columns = value; }
    public int Rows { get => rows; set => rows = value; }
    public int Ppu { get => ppu; set => ppu = value; }
    public Vector2 Pivot { get => pivot; set => pivot = value; }
    public RuntimeAnimatorController BaseAnimatorController { get => baseAnimatorController; set => baseAnimatorController = value; }
    public CustomAnimationRange[] Animations { get => animations; set => animations = value; }

    public string GetSpriteAnimName(int index)
    {
        CustomAnimationRange car = animations.FirstOrDefault(a => (index >= a.from && index <= a.to));
        return car.name;
    }
}