﻿namespace TvMazeApiIntegration.Application.Queries.GetAllShows;

public record GetAllShowsQueryResponse(IReadOnlyCollection<GetAllShowsQueryResponseItem> Shows);
