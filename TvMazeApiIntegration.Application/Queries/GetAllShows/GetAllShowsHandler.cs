using CSharpFunctionalExtensions;
using MediatR;
using TvMazeApiIntegration.Application.Extensions;
using TvMazeApiIntegration.Domain.Interfaces;

namespace TvMazeApiIntegration.Application.Queries.GetAllShows;

public class GetAllShowsHandler(
    IShowRepository showRepository) : IRequestHandler<GetAllShowsQuery, IResult<GetAllShowsQueryResponse>>
{
    private readonly IShowRepository _showRepository = showRepository;

    public async Task<IResult<GetAllShowsQueryResponse>> Handle(GetAllShowsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var shows = await _showRepository.GetAllShows(cancellationToken)!;

            return Result.Success(shows.ToGetAllShowsQueryResponse());
        }
        catch (Exception ex)
        {
            return Result.Failure<GetAllShowsQueryResponse>($"Error reading from DB: {ex.Message}");
        }
    }
}
