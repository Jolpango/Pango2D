using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Pango2D.Tiled
{
    public partial class TilesetData
    {
        [JsonProperty("columns")]
        public int Columns { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("imageheight")]
        public int Imageheight { get; set; }

        [JsonProperty("imagewidth")]
        public int Imagewidth { get; set; }

        [JsonProperty("margin")]
        public int Margin { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("spacing")]
        public int Spacing { get; set; }

        [JsonProperty("tilecount")]
        public int Tilecount { get; set; }

        [JsonProperty("tiledversion")]
        public string Tiledversion { get; set; }

        [JsonProperty("tileheight")]
        public int Tileheight { get; set; }

        [JsonProperty("tilewidth")]
        public int Tilewidth { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public partial class TilesetData
    {
        public static TilesetData FromJson(string json) => JsonConvert.DeserializeObject<TilesetData>(json, TilesetDataConverter.Settings);
    }

    public static class SerializeTilesetData
    {
        public static string ToJson(this TilesetData self) => JsonConvert.SerializeObject(self, TilesetDataConverter.Settings);
    }

    internal static class TilesetDataConverter
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