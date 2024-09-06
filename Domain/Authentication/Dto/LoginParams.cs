using System.ComponentModel.DataAnnotations;

namespace Domain.Authentication.Dto;

public record LoginParams([property: Required, EmailAddress] string Email, [property: Required] string Password);