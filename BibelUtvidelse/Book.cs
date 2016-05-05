using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibelUtvidelse
{
    public class Book
    {

        /// <summary>
        /// This exstension was made by Berin Loritsch, im happy to be able to use it. 
        /// Kurious Iesous 
        /// </summary>

        // The set of books we recognize
        private static readonly List<Book> books;
        private static readonly Dictionary<string, Book> commonMisspellings;

        static Book()
        {
            // Initialize the set
            books = new List<Book>{
            // Old Testament
            new Book("1 Mosebok", "1 Mos.", "1 Mo", 50), // Gen
            new Book("2 Mosebok", "2 Mos.", "2 Mo", 40),  // Exod
            new Book("3 Mosebok", "3 Mos.", "3 Mo", 27), // Lev
            new Book("4 Mosebok", "4 Mos.", "3 Mo", 36), // Num
            new Book("5 Mosebok", "5 Mos.", "5 Mo", 34), // Deut
            new Book("Josva", "Josv.", "Jos", 24), // Josh
            new Book("Dommerne", "Domr.", "Dom", 21), // Judg
            new Book("Ruts", "Rut", "Ru.", 4), // Ruth
            new Book("1 Samuel", "1 Sam.", "1 S", 31), // 1Sam
            new Book("2 Samuel", "2 Sam.", "2 S", 24), // 2Sam
            new Book("1 Kongebok", "1 Konge.", "1 K", 22), // 1Kgs
            new Book("2 Kongebok", "2 Konge.", "2 K", 25), // 2Kgs
            new Book("1 Krønikebok", "1 Krøn.", "1 Krø", 29), // 1Chr
            new Book("2 Krønikebok", "2 Krøn.", "2 Krø", 36), // 2Chr
            new Book("Esra", "Esra.", "Esr", 10), // Ezra
            new Book("Nehemja", "Neh.", "Ne", 13), // Neh
            new Book("Ester", "Est.", "Est", 10), // Esth
            new Book("Job", "Job", "Jb", 42), // Job
            new Book("Salmene", "Sal.", "Sl", 150), // Ps
            new Book("Ordspråkene", "Ordsp.", "Or", 31), // Prov
            new Book("Forkynneren", "Fork.", "Fk", 12), // Eccl
            new Book("Høysangen", "Høys.", "Høys", 8), // Song
            new Book("Jesaia", "Jes.", "Js", 66), // Isa
            new Book("Jeremia", "Jer.", "Je", 52), // Jer
            new Book("Klagesangene", "Klag.", "Kla", 5), // Lam
            new Book("Esekiel", "Esek.", "Es", 48), // Ezek
            new Book("Daniel", "Dan.", "Da", 12), // Dan
            new Book("Hosea", "Hos.", "Ho", 14), // Hos
            new Book("Joel", "Joel", "Joel", 3), // Joel
            new Book("Amos", "Amos", "Am", 9), // Amos
            new Book("Obdaja", "Ob.", "Ob", 1), // Obad
            new Book("Jona", "Jona", "Jona", 4), // Jonah
            new Book("Mika", "Mik.", "Mi", 7), // Mic
            new Book("Nahum", "Nah.", "Na", 3), // Nah
            new Book("Habakkuk", "Hab.", "Hab", 3), // Hab
            new Book("Sefanja", "Sef.", "Sef", 3), // Zeph
            new Book("Haggai", "Hag.", "Hag", 2), // Hag
            new Book("Sakarja", "Sak.", "Zec", 14), // Zech
            new Book("Malaki", "Mal.", "Mal", 4), // Mal

            // New Testament
            new Book("Matteus", "Matt.", "Mt", 28), // Matt
            new Book("Markus", "Mark.", "Mk", 16), // Mark
            new Book("Lukas", "Luke.", "Lu", 24), // Luke
            new Book("Johannes", "Joh.", "Jn", 21), // John
            new Book("Apostelgjerningene", "Apg.", "Apg", 28), // Acts
            new Book("Romerene", "Rom.", "Ro", 16), // Rom
            new Book("1 Korinterene", "1 Kor.", "1 Kor", 16), // 1Cor
            new Book("2 Korinterene", "2 Kor.", "2 Kor", 13), // 2Cor
            new Book("Galaterne", "Gal.", "Ga", 6), // Gal
            new Book("Efeserene", "Efe.", "Ef", 6), // Eph
            new Book("Filipperne", "Fil.", "Fi", 4), // Phil
            new Book("Kolosserne", "Kol.", "Col", 4), // Col
            new Book("1 Tessalonikerne", "1 Tess.", "1 Te", 5), // 1Thess
            new Book("2 Tessalonikerne", "2 Tess.", "2 Te", 3), // 2Thess
            new Book("1 Timoteus", "1 Tim.", "1 Ti", 6), // 1Tim
            new Book("2 Timoteus", "2 Tim.", "2 Ti", 4), // 2Tim
            new Book("Titus", "Titus.", "Tit", 3), // Titus
            new Book("Filemon", "Filem.", "File", 1), // Phlm
            new Book("Hebreerne", "Heb.", "He", 13), // Heb
            new Book("Jakob", "Jakob.", "Ja", 5), // Jas
            new Book("1 Peter", "1 Peter.", "1 Pe", 5), // 1Pet
            new Book("2 Peter", "2 Peter.", "2 Pe", 3), // 2Pet
            new Book("1 Johannes", "1 Joh.", "1 Jn", 5), // 1John
            new Book("2 Johannes", "2 Joh.", "2 Jn", 1), // 2John
            new Book("3 Johannes", "3 Joh.", "3 Jn", 1), // 3John
            new Book("Judas", "Judas", "Judas", 1), // Jude
            new Book("Åpenbaringen", "Åp.", "Åp", 22) // Rev
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

        private Book(string fullName, string abbrev, string thompsan, int chapters)
        {
            order = numCreated;
            Name = fullName;
            StandardAbreviation = abbrev;
            ThompsonAbreviation = thompsan;
            ChapterCount = chapters;
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
        /// The number of chapters in the book.
        /// </summary>
        public int ChapterCount { get; private set; }

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

        public static IEnumerable<Book> List()
        {
            return books.ToArray();
        }
    }
}
