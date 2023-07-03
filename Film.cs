namespace MyApp;

class Film : ArtObject
{
    public int Length { get; set; }
    public IEnumerable<Actor> Actors { get; set; }
}
