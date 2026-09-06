using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace WizardOfTerror.StaticObjects;

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
        
        //Board = new int[WidthInTiles, HeightInTiles];
        //for (var x = 0; x < Board.GetLength(0); x++)
        //{
        //    for (var y = 0; y < Board.GetLength(1); y++)
        //    {
        //        //Board[x, y] = Random.Shared.Next(64);
        //        Board[x, y] = 0;
        //    }
        //}

        // ReSharper disable RedundantExplicitArraySize
        Board = new int[30, 20]
            {
            { 37, 37, 37, 37, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 37, 37, 37, 37, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 37, 37, 37, 37, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 37, 37, 37, 37, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 12, 12, 12, 61, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26,  2, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26,  2, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26,  2, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26,  2, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26,  2, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26,  2, 37, 37, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 12, 61, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 02, 37, 37, 37, 37,  37, 37, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 12, 12, 12, 12,  12, 61, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26, 26, 43, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
            { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,  26,  3, 37, 37, 37, 37, 37, 37, 37, 37 },
        };
        // ReSharper restore RedundantExplicitArraySize

        HFence = new Sprite(Raylib.LoadTexture("Resources/2 Objects/2 Fence/1.png"));
        VFence = new Sprite(Raylib.LoadTexture("Resources/2 Objects/2 Fence/7.png"));
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
                TileSetBody.Draw(location, tileType);
            }
        }

        for (var x = HFence.Size.X / 2; x < WOTGame.Current.WindowWidth - HFence.Size.X / 2; x += HFence.Size.X)
        {
            HFence.Draw(new Vector2(x, HFence.Size.Y / 2));
            HFence.Draw(new Vector2(x, WOTGame.Current.WindowHeight - HFence.Size.Y));
        }

        for (var y = VFence.Size.Y / 2; y < WOTGame.Current.WindowHeight - VFence.Size.Y / 2; y += VFence.Size.Y)
        {
            VFence.Draw(new Vector2(VFence.Size.X / 2, y));
            VFence.Draw(new Vector2(WOTGame.Current.WindowWidth - VFence.Size.X, y));
        }
    }
    #endregion

    #region Properties
    public const int WidthInTiles = 30;
    public const int HeightInTiles = 20;

    public int[,] Board { get; }
    public AnimatedTilesSprite TileSetBody { get; set; }

    public Sprite HFence { get; }
    public Sprite VFence { get; }
    #endregion
}