namespace FSCSharp;

public class GameObjectPriorityComparer : IComparer<GameObject>
{
    #region IComparer implementation
    public int Compare(GameObject? x, GameObject? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (y is null) return 1;
        if (x is null) return -1;
        return x.Priority.CompareTo(y.Priority);
    }
    #endregion
}