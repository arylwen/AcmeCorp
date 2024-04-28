namespace AcmeCorpAPI.Domain;
public class Order {
    public int Id { get; set; }
    public string Details { get; set; }
    public DateTime OrderDate { get; set; }

    public int CustomerId { get; set; }
    public Customer customer { get; private set;}
}