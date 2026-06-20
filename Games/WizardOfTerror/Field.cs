using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror;

public class Field : TangibleGameObject
{
    #region Constructors
    public Field()
    {
        var frames = new List<Vector2>();
        for (var y = 16; y <= 256; y += 32)
        {
            for (var x = 16; x <= 256; x += 32)
            {
                frames.Add(new Vector2(x, y));
            }
        }
        var size = new Vector2(32, 32);

        var fieldsTilesetTexture = Raylib.LoadTexture("Resources/1 Tiles/FieldsTileset.png");

        TileSetBody = new AnimatedTilesSprite(fieldsTilesetTexture, frames.ToArray(), size, 0.1f, 2f);

        for (var x = 0; x < Board.GetLength(0); x++)
        {
            for (var y = 0; y < Board.GetLength(1); y++)
            {
                Board[x, y] = Random.Shared.Next(64);
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
                var location = new Vector2(32 + x*64, 32 + y*64);
                var tileType = Board[x, y];
                TileSetBody.Draw(location, (int)tileType);
            }
        }
    }
    #endregion

    #region Properties
    public const int WidthInTiles = 30;
    public const int HeightInTiles = 20;

    public int[,] Board { get; set; } = new int[WidthInTiles, HeightInTiles];
    public AnimatedTilesSprite TileSetBody { get; set; }
    #endregion
}