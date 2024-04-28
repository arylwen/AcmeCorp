namespace AcmeCorpAPI.Domain;
public class OrderDTO {
    public int Id { get; set; }
    public string Details { get; set; }
    public DateTime OrderDate { get; set; }

    public int CustomerId { get; set; }
}