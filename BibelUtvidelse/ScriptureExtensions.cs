using System.Collections.Generic;
using System.Linq;

namespace BibelUtvidelse
{
    /// <summary>
    /// Convenience functions to reduce the list of scripture references that were found
    /// during scanning to the smallest set possible.
    /// </summary>
    public static class ScriptureExtensions
    {
        /// <summary>
        /// Reduces a set of string representations of scripture references to the smallest
        /// selection, resulting in another string representation.  For example, if you have
        /// 1 Mosebok 1:1 and 1 Mosebok 1:3,4-6 the function would yield 1 Mosebok 1:1,3-6.
        /// 
        /// This is an extention function available on any IEnumerable&lt;string&gt;
        /// </summary>
        /// <param name="source">the source list of references</param>
        /// <returns>a reduced list of references</returns>
        public static HashSet<string> ReduceScriptures(this IEnumerable<string> source)
        {
            ICollection<Reference> references = source.Select(r => Reference.Parse(r)).Reduce();
            return new HashSet<string>(references.Select(r => r.ToString()));
        }

        /// <summary>
        /// Reduces a set of Reference objects to the smallest set of References, merging verse
        /// listings, etc.
        /// </summary>
        /// <param name="source">the source list of references</param>
        /// <returns>a reduced set of references</returns>
        public static ICollection<Reference> Reduce(this IEnumerable<Reference> source)
        {
            Reference lastReference = null;
            List<Reference> destination = new List<Reference>();

            foreach (Reference check in source.OrderBy(r => r))
            {
                if (lastReference != null)
                {
                    if (lastReference.Book.Equals(check.Book) && lastReference.Chapter == check.Chapter)
                    {
                        HashSet<int> uniqueVerses = new HashSet<int>(lastReference.Verses.Union(check.Verses));
                        lastReference.Verses = uniqueVerses.OrderBy(v => v).ToArray();
                    }
                    else
                    {
                        destination.Add(lastReference);
                        lastReference = null;
                    }
                }

                lastReference = check;
            }

            if (lastReference != null)
            {
                destination.Add(lastReference);
            }

            return destination;
        }
    }
}
