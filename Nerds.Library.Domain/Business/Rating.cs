using System.Linq;

namespace Nerds.Library.Books
{
    /// <summary>
    /// A rating for some book.
    /// </summary>
    internal sealed class Rating
    {
        /// <summary>
        /// The book being rated.
        /// </summary>
        public BookTemplate Book { get; set; }

        /// <summary>
        /// The number of stars (1..5) given tot a book.
        /// </summary>
        public double Stars { get; set; }

        /// <summary>
        /// Aggregates various ratings into a single composite value.
        /// </summary>
        /// <param name="ratings">The ratings to aggregate.</param>
        /// <returns>An average <see cref="Rating"/>.</returns>
        public static Rating Aggregate(IQueryable<Rating> ratings)
        {
            return new Rating { Stars = ratings.Average(r => r.Stars) };
        }
    }
}
