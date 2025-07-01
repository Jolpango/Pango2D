using System;
public class AnimationInstance
{
    public string Name { get; }
    public bool Loop { get; }
    public Action OnEnd { get; }
    public Action<int> OnFrameChanged { get; }

    public AnimationInstance(string name, bool loop = false, Action onEnd = null, Action<int> onFrameChanged = null)
    {
        Name = name;
        Loop = loop;
        OnEnd = onEnd;
        OnFrameChanged = onFrameChanged;
    }
}
