namespace TerrabitTest.Model;

public class UserSearchReq
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Gender { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDelete { get; set; }
    public DateTime? CreateDateTo { get; set; }
    public DateTime? CreateDateFrom { get; set; }
    public DateTime? UpdateDateTo { get; set; }
    public DateTime? UpdateDateFrom { get; set; }
    public DateTime? DeleteDateTo { get; set; }
    public DateTime? DeleteDateFrom { get; set; }
}

public class UserCreateReq
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Gender { get; set; }
}