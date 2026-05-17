namespace FSCSharp;

public abstract class GameObject : IDisposable
{
    #region IDisposable
    public virtual void Dispose()
    {
        //do nothing
    }
    #endregion

    #region Methods
    public virtual void PreUpdate(float delta) { }
    public abstract void Update(float delta);
    public virtual void PostUpdate(float delta) { }
    #endregion

    #region Properties
    public virtual bool ToDelete { get; set; }
    public int Priority { get; set; } = 100;
    #endregion
}