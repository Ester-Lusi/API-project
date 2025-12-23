using Entities;

namespace Dtos
{
    public record OrderDto
    (
        int OrderId,
        DateOnly? OrderDate,
        int OrderSum,
        IEnumerable<OrderDto> OrderItems,
        int UserId
    );
}
