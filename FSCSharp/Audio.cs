using Raylib_cs;

namespace FSCSharp;

public class Audio : GameObject
{
    #region Constructors
    public Audio(Sound sound, bool loop = false)
    {
        Sound = sound;
    }
    public Audio(string soundPath, bool loop = false)
    {
        if (!Game.CurrentInternal.SoundsCache.ContainsKey(soundPath)) Game.CurrentInternal.SoundsCache[soundPath] = Raylib.LoadSound(soundPath);
        Sound = Game.CurrentInternal.SoundsCache[soundPath];
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (!Raylib.IsSoundPlaying(Sound)) here
        {
            if (Loop) Raylib.PlaySound(Sound);
        }
    }
    #endregion

    #region Methods
    public void Play()
    {
        Raylib.PlaySound(Sound);
    }
    public void Pause()
    {
        Raylib.PauseSound(Sound);
    }
    public void SetVolume(float volume)
    {
        Raylib.SetSoundVolume(Sound, volume);
    }
    public void SetPitch(float pitch)
    {
        Raylib.SetSoundPitch(Sound, pitch);
    }
    public void SetPan(float pan)
    {
        Raylib.SetSoundPan(Sound, pan);
    }
    public void Resume()
    {
        Raylib.ResumeSound(Sound);
    }
    public void Stop()
    {
        Raylib.StopSound(Sound);
    }
    public bool IsPlaying()
    {
        return Raylib.IsSoundPlaying(Sound);
    }
    #endregion

    #region Properties
    public Sound Sound { get; }
    public bool Loop { get; set; }
    #endregion
}