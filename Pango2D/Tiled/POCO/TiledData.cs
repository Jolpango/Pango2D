using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pango2D.Tiled
{
    public partial class TiledData
    {
        [JsonProperty("compressionlevel")]
        public int Compressionlevel { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("infinite")]
        public bool Infinite { get; set; }

        [JsonProperty("layers")]
        public Layer[] Layers { get; set; }

        [JsonProperty("nextlayerid")]
        public int Nextlayerid { get; set; }

        [JsonProperty("nextobjectid")]
        public int Nextobjectid { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("renderorder")]
        public string Renderorder { get; set; }

        [JsonProperty("tiledversion")]
        public string Tiledversion { get; set; }

        [JsonProperty("tileheight")]
        public int Tileheight { get; set; }

        [JsonProperty("tilesets")]
        public TilesetDataRef[] Tilesets { get; set; }

        [JsonProperty("tilewidth")]
        public int Tilewidth { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public partial class Layer
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public int[] Data { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public int? Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opacity")]
        public float Opacity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public int? Width { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("draworder", NullValueHandling = NullValueHandling.Ignore)]
        public string Draworder { get; set; }

        [JsonProperty("objects", NullValueHandling = NullValueHandling.Ignore)]
        public TiledObject[] Objects { get; set; }
    }

    public partial class TiledObject
    {
        [JsonProperty("height")]
        public float Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rotation")]
        public int Rotation { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("width")]
        public float Width { get; set; }

        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }
    }

    public partial class TilesetDataRef
    {
        [JsonProperty("firstgid")]
        public int Firstgid { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public partial class TiledData
    {
        public static TiledData FromJson(string json) => JsonConvert.DeserializeObject<TiledData>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TiledData self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
