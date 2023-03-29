using System.ComponentModel;

namespace multi_tenant.Models.Enums;

/// <summary>
/// ErrorCode
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// Tenant key unavailable
    /// </summary>
    [Description("Tenant key unavailable")]
    TenantKeyUnavailable = 1001,

    /// <summary>
    /// Invalid Tenant
    /// </summary>
    [Description("Invalid Tenant")]
    InvalidTenant = 1002,

    /// <summary>
    /// User name cannot be left blank
    /// </summary>
    [Description("User name cannot be left blank")]
    UserNameCannotBeLeftBlank = 1101,

    /// <summary>
    /// Password cannot be left blank
    /// </summary>
    [Description("Password cannot be left blank")]
    PasswordCannotBeLeftBlank = 1102,

    /// <summary>
    /// User Not Found
    /// </summary>
    [Description("User Not Found")]
    UserNotFound = 1103,

    /// <summary>
    /// Incorrect Password
    /// </summary>
    [Description("Incorrect Password")]
    IncorrectPassword = 1104
}