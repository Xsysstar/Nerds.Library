using Nerds.Library.Books;
using Nerds.Library.Business;
using System;
using System.Globalization;
using System.Linq;

namespace Nerds.Library
{
    /// <summary>
    /// A factory to create dummy instances of domain entities.
    /// </summary>
    public class DummyFactory
    {
        private readonly Random random;

        private readonly Publisher[] fakePublishers;
        private readonly Title[] fakeTitles;
        private readonly Author[] fakeAuthors;
        private readonly Genre[] fakeGenres;

        public DummyFactory(int seed = 1337)
        {
            random = new Random(seed);

            fakePublishers = new string[] { "Nerds & co", "Springer", "Scholastic", "Wiley", "Oxford University Press", "Pearson Education" }.Select(name => new Publisher { Id = Guid.NewGuid(), Name = name }).ToArray();
            fakeTitles = new string[] { "Harry Potter", "Lord of the Rings", "The Pragmatic Programmer", "Clean Code", "The Art of War" }.Select(caption => new Title { Caption = caption }).ToArray();
            fakeAuthors = new string[] { "J.K. Rowling", "Tolkien", "Kevlin Henney", "Hans Kazan", "Anonymous" }.Select(name => new Author { Id = Guid.NewGuid(), FullName = name }).ToArray();
            fakeGenres = new string[] { "Action", "Romance", "Drama", "Documentary", "Fake news" }.Select(caption => new Genre { Id = Guid.NewGuid(), Caption = caption }).ToArray();
        }

        public Organization FakeOrganization()
        {
            var organization = new Organization();
            for (var i = 0; i <= 5; i++)
                organization.AddBook(FakeBook());

            organization.AddCustomer(FakeCustomer());
            organization.AddCustomer(FakeCustomer());

            return organization;
        }

        public Book FakeBook()
        {
            var id = Guid.NewGuid();
            return new Book
            {
                Id = id,
                UniqueBarcode = id.ToString("N", CultureInfo.InvariantCulture).Substring(0, 13),
                Template = FakeBookTemplate()
            };
        }

        public BookTemplate FakeBookTemplate()
        {
            return new BookTemplate
            {
                Id = Guid.NewGuid(),
                Publication = FakePublication(),
                Title = FakeTitle(),
                Authors = Enumerable.Range(1, random.Next(1, 4)).Select(_ => FakeAuthor()).Distinct().AsQueryable(),
                Genres = Enumerable.Range(1, random.Next(1, 3)).Select(_ => FakeGenre()).Distinct().AsQueryable(),
            };
        }

        public Publication FakePublication()
        {
            return new Publication
            {
                // Published in the last twenty years on some day.
                PublicationDate = DateTime.Now.AddDays(0 - random.Next(20 * 365) * random.NextDouble()),
                Publisher = FakePublisher(),
                ISBN = "978-3-16-148410-" + random.Next(10),
            };
        }

        public Publisher FakePublisher()
        {
            return fakePublishers[random.Next(fakePublishers.Length)];
        }

        public Title FakeTitle()
        {
            return fakeTitles[random.Next(fakeTitles.Length)];
        }

        public Author FakeAuthor()
        {
            return fakeAuthors[random.Next(fakeAuthors.Length)];
        }

        public Genre FakeGenre()
        {
            return fakeGenres[random.Next(fakeGenres.Length)];
        }

        public Customer FakeCustomer()
        {
            var id = Guid.NewGuid();
            return new Customer
            {
                Id = id,
                Name = "Customer " + id,
            };
        }
    }
}
