using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
namespace Pango2D.Core
{
    public class AssetRegistry<T> : Registry<string, T>
    {
        protected readonly ContentManager content;

        public AssetRegistry(ContentManager content)
        {
            this.content = content;
        }
        public virtual T this[string key]
        {
            get
            {
                if (!TryGet(key, out var asset))
                    throw new KeyNotFoundException($"Asset with key '{key}' not found.");
                return asset;
            }
        }

        public virtual T Load(string key, string path)
        {
            var asset = content.Load<T>(path);
            Add(key, asset);
            return asset;
        }

        public virtual T GetOrLoad(string key, string path)
        {
            if (!TryGet(key, out var asset))
                asset = Load(key, path);
            return asset;
        }

        public virtual void Unload(string key)
        {
            if (TryGet(key, out var asset) && asset is IDisposable d)
                d.Dispose();
            Remove(key);
        }

        public void Clear(bool dispose)
        {
            if (dispose)
            {
                foreach (var asset in entries.Values)
                    if (asset is IDisposable d) d.Dispose();
            }
            base.Clear();
        }
    }

}
