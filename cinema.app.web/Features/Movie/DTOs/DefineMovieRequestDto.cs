namespace cinema.app.web.Features.Movie.DTOs
{
    public record DefineMovieRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public int Rows { get; set; }
        public int SeatsPerRow { get; set; }
    }
}
