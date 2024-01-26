namespace Client.Data.Herramienta;

public class ApiResponseViewModel
{
    public List<object> Items { get; set; }
    public bool HasMore { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Count { get; set; }
}
