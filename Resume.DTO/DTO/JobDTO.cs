namespace Resume.Data.DTO;

public record JobDTO(string Position, DateTime StartDate, DateTime? EndDate, CompanyDTO Company);