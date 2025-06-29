using System;
public class AnimationInstance
{
    public string Name { get; }
    public bool Loop { get; }
    public Action OnEnd { get; }

    public AnimationInstance(string name, bool loop = false, Action onEnd = null)
    {
        Name = name;
        Loop = loop;
        OnEnd = onEnd;
    }
}
