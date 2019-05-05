using System;

namespace Nerds.Library.Books
{
    public sealed class Author
    {
        /// <summary>
        /// The globally unique identifier of this instance.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The author's full name. You may type a name in one of three forms: "First von Last" "von
        /// Last, First" "von Last, Jr, First".
        /// </summary>
        /// <remarks>
        /// Check the <a href="https://nwalsh.com/tex/texhelp/bibtx-23.html">bibtex format</a>.
        /// </remarks>
        public string FullName { get; set; }

        // TODO: DayOfBirth, DayOfDeath.
    }
}
