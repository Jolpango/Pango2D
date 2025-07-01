using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components.Contracts;
using System;

namespace Pango2D.ECS.Components
{
    public class Camera : IComponent
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Zoom { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public float Friction { get; set; } = 0.1f;
        public float MaxSpeed { get; set; } = 100f;
        public int ViewportWidth { get; set; } = 640;
        public int ViewportHeight { get; set; } = 360;

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3((int)-Position.X, (int)-Position.Y, 0f)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom, Zoom, 1f) *
               Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0f));
        }
    }
}
