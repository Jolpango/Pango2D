using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Core.Audio
{
    public class SoundEffectRegistry : AssetRegistry<SoundEffect>
    {
        public SoundEffectRegistry(ContentManager content) : base(content)
        {
        }
    }
}
