using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record UserEmail
{
  private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

  public string Value { get; init; }

  public UserEmail(string value)
  {
    if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("El email no puede estar vacío.");

    if (!EmailRegex.IsMatch(value)) throw new ArgumentException("El formato del email no es válido.");

    Value = value.ToLowerInvariant();
  }
}