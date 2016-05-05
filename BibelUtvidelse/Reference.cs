﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BibelUtvidelse
{
    public class Reference
    {
        /// <summary>
        /// This exstension was made by Berin Loritsch, im happy to be able to use it. 
        /// Kurious Iesous 
        /// </summary>

        private static readonly Regex RemoveHtml = new Regex("<[^>]*>", RegexOptions.Compiled);

        public Book Book { get; set; }
        public int Chapter { get; set; }
        public int[] Verses { get; set; }



        public static bool TryParse(string text, out Reference reference)
        {
            string errorString;
            reference = InternalParse(text, out errorString);

            if (errorString != null)
            {
                reference = null;
                return false;
            }

            return true;
        }

        private static Reference InternalParse(string text, out string errorString)
        {
            errorString = null;
            int colon = text.LastIndexOf(':');
            int chapter = -1;
            string chapterSection = "1";
            string verseSection = "";

            if (colon > 0)
            {
                verseSection = text.Substring(colon + 1);
                chapter = colon - 3;

                chapterSection = text.Substring(chapter, colon - chapter);
                while (!string.IsNullOrEmpty(chapterSection) && !Char.IsDigit(chapterSection[0]))
                {
                    chapter++;
                    chapterSection = text.Substring(chapter, colon - chapter);
                }
            }
            else
            {
                chapter = 2;  // skip initial numbers for books
                while (chapter < text.Length && !Char.IsDigit(text[chapter]))
                {
                    chapter++;
                }

                if (chapter == text.Length)
                {
                    errorString = "There are no chapter or verses, can't be a reference.";
                    return null;
                }

                verseSection = text.Substring(chapter);
            }

            Book book;
            if (!Book.TryParse(text.Substring(0, chapter), out book))
            {
                errorString = "There is no book, can't be a reference.";
                return null;
            }

            if (!int.TryParse(chapterSection, out chapter))
            {
                errorString = "Bad chapter format";
                return null;
            }

            Reference reference = new Reference
            {
                Book = book,
                Chapter = chapter
            };

            if (colon < 0 && reference.Book.ChapterCount > 1)
            {
                if (!int.TryParse(verseSection, out chapter))
                {
                    errorString = "Bad chapter format.";
                    return null;
                }

                reference.Chapter = chapter;
                reference.Verses = new int[0];
                return reference;
            }

            if (reference.Chapter > reference.Book.ChapterCount)
            {
                errorString = "Chapter found was too high";
                return null;
            }

            reference.Verses = ParseRanges(verseSection, out errorString);

            return reference;
        }

        private static int[] ParseRanges(string section, out string errorString)
        {
            errorString = null;
            List<int> numbers = new List<int>();
            string[] items = section.Split(',');

            foreach (string verse in items)
            {
                string[] ranges = verse.Split('-');

                if (ranges.Length > 2 || ranges.Length == 0)
                {
                    errorString = "Invalid range specification";
                    return new int[0];
                }

                int start;
                if (!int.TryParse(ranges[0], out start))
                {
                    errorString = "Invalid range specification";
                    return new int[0];
                }

                int end = start;
                if (ranges.Length > 1 && !int.TryParse(ranges[1], out end))
                {
                    errorString = "Invalid range specification";
                    return new int[0];
                }

                if (end < start)
                {
                    errorString = "invalid range specification";
                    return new int[0];
                }

                for (int i = start; i <= end; i++)
                {
                    numbers.Add(i);
                }
            }

            return numbers.ToArray();
        }

        public ICollection<Reference> scantext(string text)
        {
            return Scan(text);

        }


        public static ICollection<Reference> Scan(string text)
        {
            List<Reference> references = new List<Reference>();

            if (text == null)
            {
                return references;
            }

            string[] words = RemoveHtml.Replace(text, "").Split(' ', '(', ')', ';', '\r', '\n', '\t');

            for (int i = 0; i < words.Length; i++)
            {
                string one = words[i];

                // If we are starting with a blank entry, just skip this cycle
                if (string.IsNullOrWhiteSpace(one))
                {
                    continue;
                }

                string two = i + 1 < words.Length ? string.Join(" ", one, words[i + 1]) : one;
                string three = i + 2 < words.Length ? string.Join(" ", two, words[i + 2]) : two;

                Book book;
                bool match = Book.TryParse(one, out book);
                match = match || Book.TryParse(two, out book);
                match = match || Book.TryParse(three, out book);

                if (match)
                {
                    string four = i + 3 < words.Length ? string.Join(" ", three, words[i + 3]) : three;
                    string five = i + 4 < words.Length ? string.Join(" ", four, words[i + 4]) : four;

                    // Keep the most inclusive version of the reference
                    Reference found = null;
                    foreach (string test in new[] { two, three, four, five })
                    {
                        Reference check;
                        if (TryParse(test, out check))
                        {
                            found = check;
                        }
                    }

                    if (found != null && !references.Contains(found))
                    {
                        references.Add(found);
                    }
                }
            }

            return references;
        }
    }
}