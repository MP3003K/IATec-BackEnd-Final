namespace DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Code { get; set; } = string.Empty;
    public double Balance { get; set; }
    public CustomerDto? Customer { get; set; }
}