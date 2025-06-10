using Pango2D.ECS.Components.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class PlayerComponent : IComponent
    {
        public int Health { get; set; }
    }
}
