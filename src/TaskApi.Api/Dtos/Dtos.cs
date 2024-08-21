using System.ComponentModel.DataAnnotations;

namespace TaskApi.Api.ServiceDtos
{
    public record CreateTaskDto([Required] string Title, [Required]string Description);
    public record UpdateTaskDto([Required]Guid Id,[Required]string Title, [Required]string Description);
    public record TaskDto(Guid Id, string Title, string Description, DateTime CreatedDate, DateTime UpdatedDate);



}