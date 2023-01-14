using core.Extensions;
using core.Models.DefaultResponses;
using MediatR;
using multi_tenant.Models.Enums;
using multi_tenant.Models.Login;
using multi_tenant.Repositories.User;

namespace multi_tenant.Managers.Login;

/// <summary>
/// 
/// </summary>
public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="userRepository"></param>
    public LoginHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handle
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = new LoginResponse();

        var user = await _userRepository.GetUserByUserNameAsync(request.Username);
        if (user is null)
        {
            response.Errors.Add(new ErrorInfo((int)ErrorCode.UserNotFound, ErrorCode.UserNotFound.GetDescription()));
            return response;
        }

        if (!user.VerifyPassword(request.Password))
        {
            response.Errors.Add(new ErrorInfo((int)ErrorCode.IncorrectPassword, ErrorCode.IncorrectPassword.GetDescription()));
            return response;
        }

        return response;
    }
}
