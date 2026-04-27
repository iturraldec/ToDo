namespace Application.DTOs;

public record AssignmentResponse(
    string AssignmentName,
    string Title,
    string Description,
    string Status,
    DateTime CreatedAt,
    DateOnly DueAt
);