using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record UserEmail
{
  // Expresión regular compilada para máximo rendimiento en el Dominio
  private static readonly Regex EmailRegex = new Regex(
      @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
      RegexOptions.IgnoreCase | RegexOptions.Compiled);

  public string Value { get; }

  private UserEmail(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("El email no puede estar vacío.");

    if (!EmailRegex.IsMatch(value))
        throw new ArgumentException("El formato del email es inválido.");

    Value = value.ToLowerInvariant(); // Normalización
  }

  public static UserEmail Create(string value)
  {
    return new UserEmail(value);
  }

  // Permite tratar el objeto como un string fácilmente
  public override string ToString() => Value;
}

