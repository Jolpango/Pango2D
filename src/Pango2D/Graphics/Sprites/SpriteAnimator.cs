using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using System;
using System.Collections.Generic;

namespace Pango2D.Graphics.Sprites
{
    public class SpriteAnimator : IComponent
    {
        private readonly Dictionary<string, SpriteAnimation> animations;
        private AnimationInstance currentInstance;
        private SpriteAnimation currentAnimation;
        private float time;
        private int currentFrameIndex;
        private bool paused;
        private readonly Queue<AnimationInstance> animationQueue = new();
        private bool animationEnded;

        public string CurrentAnimationName => currentInstance?.Name;
        public string DefaultAnimation { get; set; }
        public bool IsPlaying => currentAnimation != null && !paused && !animationEnded;
        public int CurrentFrameIndex => currentFrameIndex;
        public SpriteFrame CurrentFrame => currentAnimation?.Frames[currentFrameIndex];
        public Rectangle CurrentFrameRect => currentAnimation?.Frames[currentFrameIndex].SourceRect ?? Rectangle.Empty;

        public event Action<string> AnimationEnded;

        public SpriteAnimator(Dictionary<string, SpriteAnimation> animations, string defaultAnimation = null)
        {
            this.animations = animations;
            DefaultAnimation = defaultAnimation;
            if (defaultAnimation != null)
                Play(defaultAnimation, true);
        }

        public void Play(string name, bool loop = false, bool forceRestart = false, Action onEnd = null, Action<int> onFrameChanged = null)
        {
            if (!forceRestart && currentInstance != null && currentInstance.Name == name && IsPlaying)
                return;
            if (!animations.TryGetValue(name, out var anim)) return;
            currentInstance = new AnimationInstance(name, loop, onEnd, onFrameChanged);
            currentAnimation = anim;
            time = 0f;
            currentFrameIndex = 0;
            currentAnimation.Looping = loop;
            paused = false;
            animationEnded = false; // Reset flag

        }

        public void Queue(string name, bool loop = false, Action onEnd = null)
        {
            animationQueue.Enqueue(new AnimationInstance(name, loop, onEnd));
        }

        public void SetDefaultAnimation(string name)
        {
            DefaultAnimation = name;
        }

        public bool IsPlayingAnimation(string name)
        {
            return currentInstance != null && currentInstance.Name == name;
        }

        public void PlayIfNotPlaying(string name, bool loop = false)
        {
            if (currentInstance == null || currentInstance.Name != name)
                Play(name, loop);
        }

        public void Stop(bool resetToDefault = true)
        {
            if(currentInstance is null || currentInstance.Name == DefaultAnimation && resetToDefault)
                return;
            currentInstance = null;
            currentAnimation = null;
            currentFrameIndex = 0;
            time = 0f;
            paused = false;
            if (resetToDefault && DefaultAnimation != null)
                Play(DefaultAnimation, true);
        }

        public void Pause() => paused = true;
        public void Resume() => paused = false;

        public void SetFrame(int frameIndex)
        {
            if (currentAnimation != null && frameIndex >= 0 && frameIndex < currentAnimation.Frames.Length)
                currentFrameIndex = frameIndex;
        }

        public void Update(GameTime gameTime)
        {
            if (paused || currentAnimation == null || currentAnimation.Frames.Length == 0) return;

            if (animationEnded) return; // Don't process further if ended

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float currentDuration = currentAnimation.Frames[currentFrameIndex].Duration > 0
                ? currentAnimation.Frames[currentFrameIndex].Duration
                : currentAnimation.DefaultFrameDuration;

            if (time >= currentDuration)
            {
                time = 0f;
                currentFrameIndex++;
                currentInstance.OnFrameChanged?.Invoke(currentFrameIndex);
                if (currentFrameIndex >= currentAnimation.Frames.Length)
                {
                    if (currentAnimation.Looping)
                    {
                        currentFrameIndex = 0;
                    }
                    else
                    {
                        currentFrameIndex = currentAnimation.Frames.Length - 1;
                        if (!animationEnded)
                        {
                            animationEnded = true;
                            AnimationEnded?.Invoke(currentInstance?.Name);
                            currentInstance?.OnEnd?.Invoke();
                        }

                        // Play next in queue if available
                        if (animationQueue.Count > 0)
                        {
                            var next = animationQueue.Dequeue();
                            Play(next.Name, next.Loop, true, next.OnEnd);
                        }
                        else if (DefaultAnimation != null && (currentInstance == null || currentInstance.Name != DefaultAnimation))
                        {
                            Play(DefaultAnimation, true);
                        }
                    }
                }
            }
        }
    }
}
