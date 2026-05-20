using System.Diagnostics;
using Raylib_cs;

namespace FSCSharp;

public class KeyboardManager
{
    #region Methods
    public bool IsKeyDown(KeyboardKey key)
    {
        return Raylib.IsKeyDown(key);
    }
    public bool IsKeyUp(KeyboardKey key)
    {
        return Raylib.IsKeyUp(key);
    }
    public bool IsKeyReleased(KeyboardKey key)
    {
        return Raylib.IsKeyReleased(key);
    }
    public bool IsKeyPressed(KeyboardKey key)
    {
        return Raylib.IsKeyPressed(key);
    }

    public KeyboardKey PeekKey()
    {
        if (!KeyBuffer.Any()) return KeyboardKey.Null;
        return KeyBuffer.First();
    }
    public KeyboardKey ReadKey()
    {
        var key = PeekKey();
        if (key != KeyboardKey.Null) KeyBuffer.RemoveAt(0);
        return key;
    }
    public int ClearBuffer()
    {
        var counter = 0;
        while (Game.KeyboardManager.ReadKey() != KeyboardKey.Null)
        {
            counter++;
        }
        return counter;
    }
    #endregion

    #region Overrides
    public virtual void Update(Stopwatch stopwatch)
    {
        //we can read up to 10 keys per iteration
        for (var i = 0; i < 10; i++)
        {
            var key = (KeyboardKey)Raylib.GetKeyPressed();
            if (key != KeyboardKey.Null)
            {
                if (key == PauseButton)
                {
                    stopwatch.Stop();
                    WaitForUnpause();
                    stopwatch.Start();
                }
                else
                {
                    KeyBuffer.Insert(0, key);
                }
            }
            else
            {
                key = GetKeyDown();
                if (key != KeyboardKey.Null) KeyBuffer.Insert(0, key);
                break;
            }
        }
    }
    protected virtual void WaitForUnpause()
    {
        var key = KeyboardKey.Null;
        while (key != KeyboardKey.Backspace)
        {
            Raylib.PollInputEvents();
            if (Raylib.WindowShouldClose()) throw new GameOverException();
            key = (KeyboardKey)Raylib.GetKeyPressed();
        }
    }

    private KeyboardKey GetKeyDown()
    {
        // Standard keyboard keys range roughly from 32 to 348
        for (var i = 32; i < 349; i++)
        {
            var key = (KeyboardKey)i;
            if (Raylib.IsKeyDown(key)) return key;
        }
        return KeyboardKey.Null;
    }
    #endregion

    #region Properties
    protected List<KeyboardKey> KeyBuffer { get; } = new ();
    public KeyboardKey PauseButton { get; set; } = KeyboardKey.Backspace;
    #endregion
}