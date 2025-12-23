namespace Dtos
{
    public record OrderDto
    (
        int OrderId,
        DateOnly? OrderDate,
        int OrderSum,
        int UserId,
        string UserFirstName,
        string UserLastName
    );
}
