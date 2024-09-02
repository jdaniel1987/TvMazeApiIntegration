namespace TvMazeApiIntegration.Application.Queries.GetAllShows;

public record GetAllShowsResponse(IReadOnlyCollection<GetAllShowsResponseItem> Shows);
