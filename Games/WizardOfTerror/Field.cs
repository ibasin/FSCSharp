using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror;

public class Field : TangibleGameObject
{
    #region Constructors
    public Field()
    {
        var tile01Texture = Raylib.LoadTexture("Resources/1 Tiles/FieldsTile_01.png");
        FieldsetSprites[(int)FieldsetEnum.Tile01] = new Sprite(tile01Texture, 2f);
        
        for (var x = 0; x < Board.GetLength(0); x++)
        {
            for (var y = 0; y < Board.GetLength(1); y++)
            {
                Board[x, y] = FieldsetEnum.Tile01;
            }
        }

        Priority = 50;
    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    public override void Draw(float delta)
    {
        for (var x = 0; x < Board.GetLength(0); x++)
        {
            for (var y = 0; y < Board.GetLength(1); y++)
            {
                var location = new Vector2(32 + x * 64, 32 + y * 64);
                var tileType = Board[x, y];
                FieldsetSprites[(int)tileType].Draw(location);
            }
        }
    }
    #endregion

    #region Properties
    public const int WidthInTiles = 25;
    public const int HeightInTiles = 20;

    public FieldsetEnum[,] Board { get; set; } = new FieldsetEnum[WidthInTiles, HeightInTiles];
    public Sprite[] FieldsetSprites { get; set; } = new Sprite[1];
    #endregion
}