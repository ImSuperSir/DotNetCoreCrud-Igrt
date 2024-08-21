using FluentValidation;
using TaskApi.Core.Entities;

namespace TaskApi.Api.Validators
{
    public class TaskValidator : AbstractValidator<MyTask>
    {
        public TaskValidator()
        {

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required")
                    .MaximumLength(50).WithMessage("Title must not exceed 50 characters");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                    .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
            // RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required.");
            // RuleFor(x => x.UpdatedAt).NotEmpty().WithMessage("UpdatedAt is required.");
        }
    }
}