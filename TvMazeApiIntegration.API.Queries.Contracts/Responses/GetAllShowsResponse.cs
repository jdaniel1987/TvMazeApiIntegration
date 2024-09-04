namespace TvMazeApiIntegration.API.Queries.Contracts.Responses;

public record GetAllShowsResponse(IReadOnlyCollection<GetAllShowsResponseItem> Shows);
