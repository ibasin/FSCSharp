using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public abstract class Game : IDisposable
{
    #region Constructors
    protected Game(string name, bool is3D)
    {
        Name = name;
        Is3D = is3D;
        CurrentInternal = this;
    }
    #endregion

    #region IDisposable
    public virtual void Dispose()
    {
        //do nothing
    }
    #endregion

    #region Properties
    public List<GameObject> GameObjects { get; set; } = new();

    public string Name { get; }
    public bool Is3D { get; }

    public abstract int WindowWidth { get; }
    public abstract int WindowHeight { get; }

    public static Game CurrentInternal { get; private set; } = null!;

    public static KeyboardManager KeyboardManager { get; } = new();

    public readonly Dictionary<string, Sound> SoundsCache = new();

    public Camera3DGameObject Camera3DGameObject
    {
        get
        {
            if (!Is3D) throw new Exception("Camera3DGameObject property can only br called for 3D games");
            var cameraGameObject = (Camera3DGameObject?)GameObjects.SingleOrDefault(x => x is Camera3DGameObject);
            if (cameraGameObject == null) throw new Exception("Exactly one game object must be of type Camera3DGameObject for 3D games!");
            return cameraGameObject;
        }
    }
    #endregion
}

public abstract class Game<TGame> : Game where TGame : Game<TGame>
{
    #region Constructors
    protected Game(string name, Color backgroundColor, bool is3D = false) : base(name, is3D)
    {
        BackgroundColor = backgroundColor;
        Raylib.InitWindow(0, 0, name);

        int display = Raylib.GetCurrentMonitor();
        WindowWidth = Raylib.GetMonitorWidth(display);
        WindowHeight = Raylib.GetMonitorHeight(display);
        Raylib.SetWindowSize(WindowWidth, WindowHeight);
        Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);

        Raylib.InitAudioDevice();
    }
    protected Game(string name, int width, int height, Color backgroundColor, bool is3D = false) : base(name, is3D)
    {
        WindowWidth = width;
        WindowHeight = height;
        BackgroundColor = backgroundColor;

        Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow);
        Raylib.InitWindow(width, height, name);
        Raylib.SetTargetFPS(60);

        Raylib.InitAudioDevice();

        Raylib.SetExitKey(KeyboardKey.Null);
    }
    #endregion

    #region Overrides
    public override void Dispose()
    {
        foreach (var gameObject in GameObjects)
        {
            gameObject.Dispose();
        }

        foreach (var soundKvp in SoundsCache)
        {
            Raylib.UnloadSound(soundKvp.Value);
        }

        Raylib.CloseWindow();
    }
    public sealed override int WindowWidth { get; }
    public sealed override int WindowHeight { get; }
    #endregion

    #region Methods
    public virtual IDisposable Run()
    {
        var stopwatch = new Stopwatch();
        var gameObjectPriorityComparer = new GameObjectPriorityComparer();

        //Initial show all objects
        //foreach (var gameObject in GameObjects)
        //{
        //    if (gameObject is TangibleGameObject tangibleGameObject) tangibleGameObject.Draw();
        //}

        try
        {
            while (!Raylib.WindowShouldClose())
            {
                var delta = (float)stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();
                KeyboardManager.Update(stopwatch);
                RunSingleIterationWithoutKeyboard(delta);
                GameObjects.Sort(gameObjectPriorityComparer);
                stopwatch.Stop();
            }
        }
        catch (GameOverException ex)
        {
            //Delete kills
            foreach (var gameObject in GameObjects.Where(x => x.ToDelete).ToArray())
            {
                gameObject.Dispose();
                GameObjects.Remove(gameObject);
            }

            var timeElapsed = 0f;
            while(timeElapsed <= ex.Delay)
            {
                try
                {
                    var delta = (float)stopwatch.Elapsed.TotalSeconds;
                    timeElapsed += delta;
                    stopwatch.Restart();
                    RunSingleIterationWithoutKeyboard(delta);
                    stopwatch.Stop();
                }
                catch (Exception)
                {
                    //Ignore
                }
            }

            if (ex.Message != "")
            {
                ShowMessage(ex.Message, Color.White);

                while (!Raylib.WindowShouldClose())
                {
                    Raylib.PollInputEvents();
                }
            }
        }
        return this;
    }

    protected virtual void RunSingleIterationWithoutKeyboard(float delta)
    {
        if (!Raylib.IsWindowFullscreen() && !Raylib.IsWindowFocused()) Raylib.SetWindowFocused();

        var gameObjectsArr = GameObjects.ToArray();

        //Round-robin pre-update all game objects
        foreach (var gameObject in gameObjectsArr) gameObject.PreUpdate(delta);

        //Round-robin update all game objects
        foreach (var gameObject in gameObjectsArr) gameObject.Update(delta);

        //Round-robin post-update all game objects
        foreach (var gameObject in gameObjectsArr) gameObject.PostUpdate(delta);

        //Delete kills
        foreach (var gameObject in GameObjects.Where(x => x.ToDelete).ToArray())
        {
            gameObject.Dispose();
            GameObjects.Remove(gameObject);
        }

        //Draw on the screen
        DrawGameObjects(delta);
    }
    public virtual void DrawGameObjects(float delta)
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(BackgroundColor);

        //resort new objects
        var gameObjectsArr = GameObjects.ToArray();

        //Round-robin draw tangible 3D game objects
        if (Is3D)
        {
            BeginMode3D();
            try
            {
                foreach (var gameObject in gameObjectsArr)
                {
                    var tangibleGameObject = gameObject as Tangible3DGameObject;
                    tangibleGameObject?.Draw();
                }
            }
            finally
            {
                EndMode3D();
            }
        }

        //Round-robin draw 2D tangible game objects
        foreach (var gameObject in gameObjectsArr)
        {
            if (gameObject is Tangible3DGameObject) continue;

            var tangibleGameObject = gameObject as TangibleGameObject;
            tangibleGameObject?.Draw(delta);
        }

        Raylib.EndDrawing();
    }

    public virtual void ShowMessage(string text, Color color)
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(BackgroundColor);

        const int fontSize = 40;
        int textWidth = Raylib.MeasureText(text, fontSize);
        var textLocation = Vector2.WindowCenter.Move(Go.Left, textWidth / 2).Move(Go.Up, fontSize / 2);
        Raylib.DrawText(text, textLocation.IntX, textLocation.IntY, fontSize, color);

        Raylib.EndDrawing();
    }

    public virtual void ShowSplashScreen(string imagePath, int milliseconds)
    {
        var texture = Raylib.LoadTexture(imagePath);
        try
        {
            //wait for x milliseconds
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds <= milliseconds)
            {
                //draw background
                Raylib.BeginDrawing();

                var hScale = (float)Current.WindowWidth / texture.Width;
                var vScale = (float)Current.WindowHeight / texture.Height;

                if (hScale <= vScale)
                {
                    var vOffset = (Current.WindowHeight - texture.Height * hScale) / 2;
                    Raylib.DrawTextureEx(texture, new Vector2(0, vOffset), 0, hScale, Color.White);
                }
                else
                {
                    var hOffset = (Current.WindowWidth - texture.Width * vScale) / 2;
                    Raylib.DrawTextureEx(texture, new Vector2(hOffset, 0), 0, vScale, Color.White);
                }

                Raylib.EndDrawing();

                if (Raylib.WindowShouldClose()) Environment.Exit(0);
            }
        }
        finally
        {
            Raylib.UnloadTexture(texture);
        }
    }

    public virtual void PlaySound(Sound sound)
    {
        Raylib.PlaySound(sound);
    }
    public virtual void PlaySound(string soundPath)
    {
        if (!SoundsCache.ContainsKey(soundPath)) SoundsCache[soundPath] = Raylib.LoadSound(soundPath);
        PlaySound(SoundsCache[soundPath]);
    }

    public virtual void BeginMode3D()
    {
        if (Is3D) Raylib.BeginMode3D(Camera3DGameObject.Camera);
    }
    public virtual void EndMode3D()
    {
        if (Is3D) Raylib.EndMode3D();
    }
    #endregion

    #region Properties
    public static TGame Current => (TGame)CurrentInternal;
    public Color BackgroundColor { get; set; }
    #endregion
}