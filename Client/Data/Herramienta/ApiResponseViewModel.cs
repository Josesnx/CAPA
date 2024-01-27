namespace Client.Data.Herramienta;

public class ApiResponseViewModel<T>
{
    public List<T> Items { get; set; }
    public bool HasMore { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Count { get; set; }
}
