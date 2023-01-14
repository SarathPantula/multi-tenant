using core.Extensions;
using FluentValidation;
using multi_tenant.Models.Enums;
using multi_tenant.Models.Login;

namespace multi_tenant.Managers.Login;

/// <summary>
/// 
/// </summary>
public class LoginValidator : AbstractValidator<LoginRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public LoginValidator()
    {
        RuleFor(request => request.Username).NotEmpty()
            .WithErrorCode(((int)ErrorCode.UserNameCannotBeLeftBlank).ToString())
            .WithMessage(ErrorCode.UserNameCannotBeLeftBlank.GetDescription());

        RuleFor(request => request.Password).NotEmpty()
            .WithErrorCode(((int)ErrorCode.PasswordCannotBeLeftBlank).ToString())
            .WithMessage(ErrorCode.PasswordCannotBeLeftBlank.GetDescription());
    }
}
