namespace Application.DTOs;

public record AssignmentResponse(
    string Title,
    string Description,
    string Status,
    string AssignedByName,
    string AssignedToName
);