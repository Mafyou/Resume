namespace Resume.Data.DTO;

public record PersonDTO(string Name, DateTime BirthDay, IReadOnlyList<JobDTO> Jobs);