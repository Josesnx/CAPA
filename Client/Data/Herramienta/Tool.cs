using System.Text;

namespace Client.Data.Herramienta;

public class Tool
{
    public static string GenerateQueryString(IDictionary<string, object> query, string url)
    {
        var builder = new StringBuilder();
        foreach (var q in query)
        {
            if (builder.Length > 0)
            {
                builder.Append('&');
            }
            builder.AppendFormat("{0}={1}", q.Key, q.Value);
        }
        return url + string.Format("?{0}", builder);
    }
}
