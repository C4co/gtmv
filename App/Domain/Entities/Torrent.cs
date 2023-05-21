namespace Domain.Entities;

public class Torrent
{
    public string hash { get; set; } = null!;

    public string quality { get; set; } = null!;

    public int seeds { get; set; }

    public int peers { get; set; }

    public string size { get; set; } = null!;

    public string url { get; set; } = null!;

    public string type { get; set; } = null!;

    public String getMagnetLink()
    {
        List<String> magnetLink = new List<String>();

        magnetLink.Add($"magnet:?xt=urn:btih:{this.hash}&dn=Url+Encoded+Movie+Name&tr=http://track.one:1234/announce&tr=udp://track.two:80");
        magnetLink.Add("udp://glotorrents.pw:6969/announce");
        magnetLink.Add("udp://tracker.opentrackr.org:1337/announce");
        magnetLink.Add("udp://torrent.gresille.org:80/announce");
        magnetLink.Add("udp://tracker.openbittorrent.com:80");
        magnetLink.Add("udp://tracker.coppersurfer.tk:6969");
        magnetLink.Add("udp://tracker.leechers-paradise.org:6969");
        magnetLink.Add("udp://p4p.arenabg.ch:1337");
        magnetLink.Add("udp://tracker.internetwarriors.net:1337");

        return String.Join("&", magnetLink);
    }
}
