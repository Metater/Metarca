namespace Metarca.Server.Ecs;

public abstract class Entity
{
    /*
    // Events for added, updated, removed component may be useful in the future

    private readonly List<IComp> components = new();

    #region Management
    public void Add<T>(T comp) where T : IComp
    {
        #if DEBUG
        Type type = typeof(T);

        if (components.Any(c => c.GetType() == type))
        {
            throw new Exception($"Attempted to add component \"{type}\" to entity \"{GetType()}\" more than once!");
        }
        #endif

        components.Add(comp);
    }
    public T Get<T>() where T : IComp
    {
        Type type = typeof(T);

        #if DEBUG
        return (T)(components.Find(c => c.GetType() == type)
            ?? throw new Exception($"Component \"{type}\" not found in entity \"{GetType()}\"!"));
        #else
        return (T)(components.Find(c => c.GetType() == type)!);
        #endif
    }
    public bool Remove<T>() where T : IComp
    {
        Type type = typeof(T);
        return components.RemoveAll(c => c.GetType() == type) > 0;
    }
    #endregion
    */
}