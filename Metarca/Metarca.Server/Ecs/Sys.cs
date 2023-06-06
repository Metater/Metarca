using System.Text;

namespace Metarca.Server.Ecs;

public abstract class Sys
{
    private readonly List<Sys> children = new();
    private readonly Dictionary<Type, Sys> childrenDict = new();

    public readonly Sys? parent;
    public readonly string lineage;

    public Sys(Sys? parent = null)
    {
        this.parent = parent;

        lineage = GetLineage();
    }

    #region Virtual
    protected virtual void Compose() { }
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Tick() { }
    protected virtual void Stop() { }
    #endregion

    #region Invoking
    public void InvokeCompose()
    {
        Compose();

        foreach (var child in children)
        {
            child.InvokeCompose();
        }
    }
    public void InvokeAwake()
    {
        Awake();

        foreach (var child in children)
        {
            child.InvokeAwake();
        }
    }
    public void InvokeStart()
    {
        Start();

        foreach (var child in children)
        {
            child.InvokeStart();
        }
    }
    public void InvokeTick()
    {
        Tick();

        foreach (var child in children)
        {
            child.InvokeTick();
        }
    }
    public void InvokeStop()
    {
        Stop();

        foreach (var child in children)
        {
            child.InvokeStop();
        }
    }
    #endregion

    #region Management
    protected void AddSub<T>(T sys) where T : Sys
    {
        children.Add(sys);
        Type type = typeof(T);
        childrenDict.Add(type, sys);
        Print("initialized");
    }

    public T Sib<T>() where T : Sys
    {
        #if DEBUG
        if (parent == null)
        {
            throw new Exception($"Parent system is null in system \"{GetType()}\", so sibling system \"{typeof(T)}\" cannot exist!");
        }

        if (GetType() == typeof(T))
        {
            throw new Exception($"System \"{GetType()}\" and requested sibling \"{typeof(T)}\" are the same type!");
        }

        try
        {
            return (T)parent.childrenDict[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            throw new Exception($"Sibling system \"{typeof(T)}\" not found in system \"{GetType()}\"!");
        }
        #else
        return (T)Parent!.subsystemsDict[typeof(T)];
        #endif
    }

    public T Sub<T>() where T : Sys
    {
        #if DEBUG
        try
        {
            return (T)childrenDict[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            throw new Exception($"Child \"{typeof(T)}\" not found in system \"{GetType()}\"!");
        }
        #else
        return (T)subsystemsDict[typeof(T)];
        #endif
    }

    private string GetLineage()
    {
        List<string> names = new();

        Sys? target = this;
        while (target != null)
        {
            names.Add(target.GetType().Name);
            target = target.parent;
        }

        StringBuilder sb = new();

        for (int i = names.Count - 1; i >= 1; i--)
        {
            string name = names[i];
            sb.Append($"[{name}] ");
        }
        sb.Append($"[{names[0]}]");

        return sb.ToString();
    }
    #endregion

    protected void Print(string message)
    {
        Console.WriteLine(
            $"[{DateTime.Now}] {lineage}\n" +
            $"\t{message}"
        );
    }
}