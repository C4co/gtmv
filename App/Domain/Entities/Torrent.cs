namespace Domain.Entities
{
    public class Torrent
    {
        public string Hash { get; set; } = null!;

        public string Quality { get; set; } = null!;

        public int Seeds { get; set; }

        public int Peers { get; set; }

        public string Size { get; set; } = null!;

        public string Url { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string GetMagnetLink()
        {
            List<string> magnetLink = new()
            {
                $"magnet:?xt=urn:btih:{Hash}&dn=Url+Encoded+Movie+Name&tr=http://track.one:1234/announce&tr=udp://track.two:80",
                "udp://glotorrents.pw:6969/announce",
                "udp://tracker.opentrackr.org:1337/announce",
                "udp://torrent.gresille.org:80/announce",
                "udp://tracker.openbittorrent.com:80",
                "udp://tracker.coppersurfer.tk:6969",
                "udp://tracker.leechers-paradise.org:6969",
                "udp://p4p.arenabg.ch:1337",
                "udp://tracker.internetwarriors.net:1337"
            };

            return string.Join("&", magnetLink);
        }
    }
}
