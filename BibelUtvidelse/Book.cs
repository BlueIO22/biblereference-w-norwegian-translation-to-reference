using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace BibelUtvidelse
{
    /// <summary>
    /// This exstension was made by Berin Loritsch, im happy to be able to use it. 
    /// Kurious Iesous 
    /// </summary>
    public class Book
    {
        // The set of books we recognize
        private static readonly List<Book> books;
        private static readonly Dictionary<string, Book> commonMisspellings;

        static Book()
        {
            // Initialize the set
            books = new List<Book>{
            // Old Testament
            new Book("1 Mosebok", "Gen", "1 Mos.", 50, true), // Gen
            new Book("2 Mosebok", "Exod", "2 Mos.", 40, true),  // Exod
            new Book("3 Mosebok", "Lev", "3 Mos.", 27, true), // Lev
            new Book("4 Mosebok", "Num", "4 Mos.", 36, true), // Num
            new Book("5 Mosebok", "Deut", "5 Mos.", 34, true), // Deut
            new Book("Josva", "Josh", "Josv.", 24, true), // Josh
            new Book("Dommerne", "Judg", "Domr.", 21, true), // Judg
            new Book("Ruts", "Ruth", "Rut.", 4, true), // Ruth
            new Book("1 Samuel", "1Sam", "1 Sam.", 31, true), // 1Sam
            new Book("2 Samuel", "2Sam", "2 Sam.", 24, true), // 2Sam
            new Book("1 Kongebok", "1Kgs", "1 Konge.", 22, true), // 1Kgs
            new Book("2 Kongebok", "2Kgs", "2 Konge.", 25, true), // 2Kgs
            new Book("1 Krønikebok", "1Chr", "1 Krøn.", 29, true), // 1Chr
            new Book("2 Krønikebok", "2Chr", "2 Krøn.", 36, true), // 2Chr
            new Book("Esra", "Ezra", "Esra.", 10, true), // Ezra
            new Book("Nehemja", "Neh", "Neh.", 13, true), // Neh
            new Book("Ester", "Esth", "Est.", 10, true), // Esth
            new Book("Job", "Job", "Job.", 42, true), // Job
            new Book("Salmene", "Ps", "Sal.", 150, true), // Ps
            new Book("Ordspråkene", "Prov.", "Ordsp.", 31, true), // Prov
            new Book("Forkynneren", "Eccl.", "Fork.", 12, true), // Eccl
            new Book("Høysangen", "Song", "Høys.", 8, true), // Song
            new Book("Jesaia", "Isa", "Jes.", 66, true), // Isa
            new Book("Jeremia", "Jer", "Jer.", 52, true), // Jer
            new Book("Klagesangene", "Lam.", "Klag.", 5, true), // Lam
            new Book("Esekiel", "Ezek", "Esek.", 48, true), // Ezek
            new Book("Daniel", "Dan", "Dan.", 12, true), // Dan
            new Book("Hosea", "Hos", "Hos.", 14, true), // Hos
            new Book("Joel", "Joel", "Joel.", 3, true), // Joel
            new Book("Amos", "Amos", "Amos.", 9, true), // Amos
            new Book("Obdaja", "Obad", "Obad.", 1, true), // Obad
            new Book("Jona", "Jonah", "Jona.", 4, true), // Jonah
            new Book("Mika", "Mic", "Mik.", 7, true), // Mic
            new Book("Nahum", "Nah", "Nah.", 3, true), // Nah
            new Book("Habakkuk", "Hab", "Hab.", 3, true), // Hab
            new Book("Sefanja", "Zeph", "Sef.", 3, true), // Zeph
            new Book("Haggai", "Hag", "Hag.", 2, true), // Hag
            new Book("Sakarja", "Zech", "Sak.", 14, true), // Zech
            new Book("Malaki", "Mal", "Mal.", 4, true), // Mal

            // New Testament
            new Book("Matteus", "Matt", "Matt.", 28, false), // Matt
            new Book("Markus", "Mark", "Mark.", 16, false), // Mark
            new Book("Lukas", "Luke", "Luk.", 24, false), // Luke
            new Book("Johannes", "John", "Joh.", 21, false), // John
            new Book("Apostelgjerningene", "Acts", "Apg.", 28, false), // Acts
            new Book("Romerene", "Rom", "Rom.", 16, false), // Rom
            new Book("1 Korinterene", "1Cor", "1 Kor.", 16, false), // 1Cor
            new Book("2 Korinterene", "2Cor", "2 Kor.", 13, false), // 2Cor
            new Book("Galaterne", "Gal", "Gal.", 6, false), // Gal
            new Book("Efeserene", "Eph", "Efes.", 6, false), // Eph
            new Book("Filipperne", "Phil", "Fil.", 4, false), // Phil
            new Book("Kolosserne", "Col", "Kol.", 4, false), // Col
            new Book("1 Tessalonikerne", "1Thess", "1 Tess.", 5, false), // 1Thess
            new Book("2 Tessalonikerne", "2Thess", "2 Tess.", 3, false), // 2Thess
            new Book("1 Timoteus", "1Tim", "1 Tim.", 6, false), // 1Tim
            new Book("2 Timoteus", "2Tim", "2 Tim.", 4, false), // 2Tim
            new Book("Titus", "Titus", "Titus.", 3, false), // Titus
            new Book("Filemon", "Phlm.", "File.", 1, false), // Phlm
            new Book("Hebreerne", "Heb", "Heb.", 13, false), // Heb
            new Book("Jakob", "Jas", "Jak.", 5, false), // Jas
            new Book("1 Peter", "1Pet", "1 Pet.", 5, false), // 1Pet
            new Book("2 Peter", "2Pet", "2 Pet.", 3, false), // 2Pet
            new Book("1 Johannes", "1John", "1 Joh.", 5, false), // 1John
            new Book("2 Johannes", "2John", "2 Joh.", 1, false), // 2John
            new Book("3 Johannes", "3John", "3 Joh.", 1, false), // 3John
            new Book("Judas", "Jude", "Judas.", 1, false), // Jude
            new Book("Åpenbaringen", "Rev", "Åp.", 22, false) // Rev
        };

            Debug.Assert(books.Count == 66);

            // These are based on what I found in the set of over 6,000
            // transcripts that people typed.
            commonMisspellings = new Dictionary<string, Book>();
            commonMisspellings.Add("Salomos Høysang", books.FirstOrDefault(b => b.ThompsonAbreviation == "Høys"));
            commonMisspellings.Add("psalm", books.FirstOrDefault(b => b.ThompsonAbreviation == "Sl"));
            commonMisspellings.Add("like", books.FirstOrDefault(b => b.ThompsonAbreviation == "Lu"));
            commonMisspellings.Add("jerimiah", books.FirstOrDefault(b => b.ThompsonAbreviation == "Je"));
            commonMisspellings.Add("galations", books.FirstOrDefault(b => b.ThompsonAbreviation == "Ga"));
        }

        private static int numCreated = 0;
        private int order;

        /// <summary>
        /// Create the book internally.
        /// </summary>
        /// <param name="fullName">the full book name</param>
        /// <param name="abbrev">the standard abreviation</param>
        /// <param name="thompsan">the Thompson abreviation</param>
        /// <param name="chapters">the number of chapters in that book</param>
        private Book(string fullName, string abbrev, string thompsan, int chapters, bool old)
        {
            order = numCreated;
            Name = fullName;
            StandardAbreviation = abbrev;
            ThompsonAbreviation = thompsan;
            ChapterCount = chapters;
            OldTestament = old;
            numCreated++;
        }

        /// <summary>
        /// The unabbreviated name of the book.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Standard abbreviations as defined in "The Christian Writer's
        /// Manual of Style", 2004 edition (ISBN: 9780310487715).
        /// </summary>
        public string StandardAbreviation { get; private set; }

        /// <summary>
        /// Thompson Chain references, pulled from the 5th edition.
        /// </summary>
        public string ThompsonAbreviation { get; private set; }

        /// <summary>
        /// Is the book in the Old testament?
        /// </summary>
        public bool OldTestament { get; private set; }

        /// <summary>
        /// The number of chapters in the book.
        /// </summary>
        public int ChapterCount { get; private set; }

        /// <summary>
        /// Display this book as a string, uses the Thompson Chain reference format.
        /// </summary>
        /// <returns>the formatted book</returns>
        public override string ToString()
        {
            return ToString("T", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Format the string with the current culture.
        /// <see cref="ToString(string,IFormatProvider)"/>
        /// </summary>
        /// <param name="format">the format spec</param>
        /// <returns>the formatted book</returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Format the book part of a reference with one of the formats.  The default format is "N".
        /// <list type="table">
        /// <listheader>
        ///   <term>Format</term>
        ///   <description>Description</description>
        /// </listheader>
        /// <item>
        ///   <term>T</term>
        ///   <description>Use the Thompson Chain Reference format.  <example>1 Chr</example></description>
        /// </item>
        /// <item>
        ///   <term>S</term>
        ///   <description>Use the Standard Abbreviation format as defined in "The Christian Writer's Manual of Style" (2004).  <example>1 Chron.</example></description>
        /// </item>
        /// <item>
        ///   <term>s</term>
        ///   <description>Use the Standard Abbreviation format as defined in "The Christian Writer's Manual of Style" (2004), but with Roman numerals.  <example>I Chron.</example></description>
        /// </item>
        /// <item>
        ///   <terms>N</terms>
        ///   <description>Use the full book name.  <example>2 Chronicles</example></description>
        /// </item>
        /// <item>
        ///   <terms>n</terms>
        ///   <description>use the full book name, but with Roman numerals.  <example>II Chronicles</example></description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="format">the format spec</param>
        /// <param name="formatProvider">the culture specific formatter (unused)</param>
        /// <returns>the formatted string</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            // Default to Thompsan references if none provided
            format = format ?? "T";

            switch (format)
            {
                case "T":
                    return ThompsonAbreviation;

                case "S":
                    return StandardAbreviation;

                case "s":
                    return ToRomanNumeral(StandardAbreviation);

                case "N":
                    return Name;

                case "n":
                    return ToRomanNumeral(Name);
            }

            throw new FormatException(string.Format("The {0} format string is not supported.", format));
        }

        /// <summary>
        /// Format the number part of the books that have more than one part as
        /// a Roman numeral rather than the Arabic numeral
        /// </summary>
        /// <param name="book">the book to reformat</param>
        /// <returns></returns>
        private string ToRomanNumeral(string book)
        {
            string[] parts = book.Split(' ');

            // We only have to convert the first part of the book to a roman numeral
            // And the highest we go is 3 (3 Jn).
            switch (parts[0])
            {
                case "1":
                    parts[0] = "I";
                    break;

                case "2":
                    parts[0] = "II";
                    break;

                case "3":
                    parts[0] = "III";
                    break;
            }

            return string.Join(" ", parts);
        }

        /// <summary>
        /// Provides a mechanism to sort books by the order in the Bible.
        /// Relies on the fact that all the books used in the system are
        /// defined statically.
        /// </summary>
        /// <param name="other">the other book to compare</param>
        /// <returns>0 if equal or greater or less than depending on order</returns>
        public int CompareTo(Book other)
        {
            return order.CompareTo(other.order);
        }

        /// <summary>
        /// Parses the string and returns a book instance if it matches.
        /// </summary>
        /// <exception cref="FormatException">If the book format could not be recognized</exception>
        /// <param name="str">the string to parse</param>
        /// <returns>a book</returns>
        public static Book Parse(string str)
        {
            Book book;
            if (TryParse(str, out book))
            {
                return book;
            }

            throw new FormatException(string.Format("Could not recognize the book {0}", str));
        }

        /// <summary>
        /// Tries to parse the string into a Book.  If it can't it will return false instead
        /// of throwing an exception.
        /// </summary>
        /// <param name="inString">The string to parse</param>
        /// <param name="book">the book that was found (or null)</param>
        /// <returns>true if found, false if not</returns>
        public static bool TryParse(string inString, out Book book)
        {
            string potentialBook = StandardizeBookOrdinals(inString);

            // Find the first book where the input string now matches one of the recognized formats.
            book = books.FirstOrDefault(
                b => b.ThompsonAbreviation.Equals(potentialBook, StringComparison.InvariantCultureIgnoreCase)
                    || b.StandardAbreviation.Equals(potentialBook, StringComparison.InvariantCultureIgnoreCase)
                    || b.Name.Equals(potentialBook, StringComparison.InvariantCultureIgnoreCase));

            if (book != null)
            {
                return true;
            }

            // If we didn't find it, check to see if we just missed it because the abbreviation
            // didn't have a period
            book = books.FirstOrDefault((b) =>
            {
                string stdAbrev = b.StandardAbreviation;
                if (stdAbrev.EndsWith("."))
                {
                    stdAbrev = stdAbrev.Substring(0, stdAbrev.Length - 1);
                }

                return potentialBook == stdAbrev;
            });

            if (book != null)
            {
                return true;
            }

            // Special Case: check for common misspellings
            string lowercase = potentialBook.ToLowerInvariant();
            commonMisspellings.TryGetValue(lowercase, out book);

            return book != null;
        }

        private static string StandardizeBookOrdinals(string str)
        {
            // Break up on all remaining white space
            string[] parts = (str ?? "").Trim().Split(' ', '\r', '\n', '\t');

            // If the first part is a roman numeral, or spelled ordinal, convert it to arabic
            var number = parts[0].ToLowerInvariant();
            switch (number)
            {
                case "first":
                case "i":
                    parts[0] = "1";
                    break;

                case "second":
                case "ii":
                    parts[0] = "2";
                    break;

                case "third":
                case "iii":
                    parts[0] = "3";
                    break;
            }

            // Recompile the parts into one string that only has a single space separating elements
            return string.Join(" ", parts);
        }

        /// <summary>
        /// Return a list of all books in the Bible.
        /// </summary>
        /// <returns>a list of all the books, in order</returns>
        public static IEnumerable<Book> List()
        {
            return books.ToArray();
        }
    }
}
