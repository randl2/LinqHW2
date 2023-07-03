using System.Text;

namespace MyApp;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var data = new List<object>()
        {
            "Hello",
            new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
            new List<int>() {4, 6, 8, 2},
            new string[] {"Hello inside array"},
            new Film()
            { 
                Author = "Martin Scorsese", 
                Name = "The Departed",
                Actors = new List<Actor>()
                {
                    new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22) },
                    new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11) },
                    new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10) }
                }
            },

            new Film()
            { 
                Author = "Gus Van Sant", 
                Name = "Good Will Hunting", 
                Actors = new List<Actor>() 
                {
                    new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10) },
                    new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11) },
                }
            },

            new Book() { Author = "Stephen King", Name = "Finders Keepers", Pages = 200 }, 
            "Leonardo DiCaprio"
        };

        //Завдання 1

        Console.WriteLine("Завдання 1: " + string.Join(", ", data.Where(obj => !(obj is ArtObject))));

        //Завдання 2

        Console.WriteLine("Завдання 2: " + string.Join(", ", data
                                                           .OfType<Film>()
                                                           .SelectMany(x => x.Actors)
                                                           .Select(x => x.Name)
                                                           .Distinct()));

        //Завдання 3

        Console.WriteLine("Завдання 3: " + string.Join(", ", data
                                                         .OfType<Film>()
                                                         .SelectMany(x => x.Actors)
                                                         .Where(x => x.Birthdate.Month == 8)
                                                         .Select(x => x.Name)
                                                         .Distinct()));

        //Завдання 4

        Console.WriteLine("Завдання 4: " + string.Join(", ", data.OfType<Film>().SelectMany(x => x.Actors)
                                        .Select(actor => new { Name = actor.Name, Age = DateTime.Now.Year - actor.Birthdate.Year })
                                        .OrderBy(x => x.Age)
                                        .Reverse()
                                        .Distinct()
                                        .Take(2)
                                        .Select(x => x.Name)));

        //Завдання 5

        Console.WriteLine("Завдання 5:\n" + string.Join(", ", data
                                                  .OfType<Book>()            
                                                  .GroupBy(book => book.Author)
                                                  .Select(book => $"\b\bАвтор: {book.Key}\nКількість книг: {book.Count()}\n\n")));

        //Завдання 6

        Console.WriteLine("Завдання 6:\n" + string.Join(", ", data
                                                 .OfType<Film>()
                                                 .GroupBy(x => x.Author)
                                                 .Select(x => $"\b\bРежисер: {x.Key}\nКількість його фільмів: {x.Count()}\n\n")) +
                                            string.Join(", ", data
                                                 .OfType<Book>()
                                                 .GroupBy(x => x.Author)
                                                 .Select(x => $"\b\bАвтор: {x.Key}\nКількість його книг: {x.Count()}\n\n")));



        //Завдання 7

        Console.WriteLine("Завдання 7:\n" + string.Join("\n", data
                                            .OfType<Film>()
                                            .SelectMany(x => x.Actors)
                                            .Select(x => x.Name)
                                            .Distinct()
                                            .SelectMany(x => x.ToLower())
                                            .GroupBy(x => x)
                                            .Select(x => $"{x.Key}: {x.Count()}")));

        //Завдання 8

        Console.WriteLine("Завдання 8:\n" + string.Join(", ", data
                                            .OfType<Book>()
                                            .Select(book => new { Author = book.Author, Book = book.Name, Pages = book.Pages } )
                                            .GroupBy(x => x.Author)
                                            .Select(book => $"\b\bАвтор: {book.Key}\nКниги: { string.Join(", ", book.Select(x => x.Book)) }" +
                                            $"\nК-сть сторінок: { string.Join(", ", book.Select(x => x.Pages)) }\n\n")));

        //Завдання 9

        Console.WriteLine("Завдання 9:\n" + string.Join(", ", data
                              .OfType<Film>()
                              .SelectMany(movie => movie.Actors, (movie, actor) => new { Movie = movie.Name, Actor = actor.Name })
                              .GroupBy(movie => movie.Actor)
                              .Select(actor => $"\b\bАктор: {actor.Key}. Фільмографія: { string.Join(", ", actor.Select(movie => movie.Movie))}\n")));

        //Завдання 10

        Console.WriteLine("Завдання 10: " + (data.OfType<Book>()
                                       .Select(x => x.Pages)
                                       .Sum() + data
                                       .OfType<List<int>>()
                                       .SelectMany(x => x)
                                       .Sum() + data
                                       .OfType<Film>()
                                       .SelectMany(x => x.Actors)
                                       .Select(x => x.Birthdate.Year)
                                       .Sum() + data
                                       .OfType<Film>()
                                       .SelectMany(x => x.Actors)
                                       .Select(x => x.Birthdate.Month)
                                       .Sum() + data
                                       .OfType<Film>()
                                       .SelectMany(x => x.Actors)
                                       .Select(x => x.Birthdate.Day)
                                       .Sum()));

        //Завдання 11

        Console.WriteLine("Завдання 11: " + string.Join(", ", data
                                                  .OfType<Book>()
                                                  .ToDictionary(x => x.Author, x => x.Name)));

        //Завдання 12

        Console.WriteLine("Завдання 12:\n" + string.Join(", ", data
                                    .OfType<Film>()
                                    .Where(movie => movie.Actors.All(actor => !(actor.Name is string) && actor.Name == "Matt Damon"))
                                    .Select(movie => movie.Name)));
    }
}
