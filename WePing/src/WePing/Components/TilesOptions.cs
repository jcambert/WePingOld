using System.Collections.Generic;

namespace WePing.Components
{
    public class TilesOptions
    {
        public Dictionary<string, string> Widths { get; set; }
        public const string TILES_OPTIONS = "tiles";
        public const string DEFAULT_TILE_WIDTH = "col-md-2 col-sm-4";

        public string this[int tileCount]
        {
            get
            {
                return Widths?[tileCount.ToString()] ?? DEFAULT_TILE_WIDTH;
            }
        }
    }
}
