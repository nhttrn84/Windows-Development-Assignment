using System;

namespace MediaPlayer
{
    public class MediaItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public TimeSpan LastPlayedPosition { get; set; }
    }
}
