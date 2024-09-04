using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Immutable;
using System.Text.Json;
using TvMazeApiIntegration.Domain.Entities;
using TvMazeApiIntegration.Domain.Interfaces;
using TvMazeApiIntegration.Infrastructure.APIs;

namespace TvMazeApiIntegration.Application.Commands.FetchAndStoreShows;

public class FetchAndStoreShowsHandler(
    ITVMazeApi tvMazeApi,
    IShowRepository showRepository) : IRequestHandler<FetchAndStoreShowsCommand, IResult<FetchAndStoreShowsCommandResponse>>
{
    private readonly ITVMazeApi _tvMazeApi = tvMazeApi;
    private readonly IShowRepository _showRepository = showRepository;

    public async Task<IResult<FetchAndStoreShowsCommandResponse>> Handle(FetchAndStoreShowsCommand request, CancellationToken cancellationToken)
    {
        string showsRawString;
        IReadOnlyCollection<Show> shows;

        try
        {
            showsRawString = await _tvMazeApi.GetShowsAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure<FetchAndStoreShowsCommandResponse>($"Error connecting to TVMaze API: {ex.Message}");
        }

        try
        {
            var showsRaw = JsonDocument.Parse(showsRawString).RootElement.EnumerateArray();
            shows = showsRaw.Select(s =>
                new Show(
                    Id: s.GetProperty("id").GetInt32(),
                    Data: s.ToString()
                ))
                .ToImmutableArray();
        }
        catch (Exception ex)
        {
            return Result.Failure<FetchAndStoreShowsCommandResponse>($"Error parsing TVMaze response: {ex.Message}");
        }

        try
        {
            await _showRepository.SaveShows(shows, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure<FetchAndStoreShowsCommandResponse>($"Error saving to DB: {ex.Message}");
        }

        return Result.Success(new FetchAndStoreShowsCommandResponse());
    }
}
