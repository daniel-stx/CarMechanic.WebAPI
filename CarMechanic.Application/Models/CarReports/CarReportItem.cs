namespace CarMechanic.Application.Models.CarReports;

public sealed record CarReportItem(string Id, string CarId, string Title, DateTime Created);
