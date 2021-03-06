﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibelUtvidelse.Test
{
    [TestFixture]
    public class ReferenceTest
    {
        [Test]
        public void ParseSimpleReference()
        {
            string text = "1 Johannes 1:9";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("1 Jn"));
            Assert.That(reference.Chapter, Is.EqualTo(1));
            Assert.That(reference.Verses, Has.Member(9));
            Assert.That(reference.Verses.Length, Is.EqualTo(1));
        }

        [Test]
        public void ParseCommaDelimitedReference()
        {
            string text = "1 Mo 2:4,6,8";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("1 Mo"));
            Assert.That(reference.Chapter, Is.EqualTo(2));
            Assert.That(reference.Verses.Length, Is.EqualTo(3));
            Assert.That(reference.Verses, Has.Member(4));
            Assert.That(reference.Verses, Has.Member(6));
            Assert.That(reference.Verses, Has.Member(8));
        }

        [Test]
        public void ParseRangeReference()
        {
            string text = "Heb. 12:1-4";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("He"));
            Assert.That(reference.Chapter, Is.EqualTo(12));
            Assert.That(reference.Verses.Length, Is.EqualTo(4));

            foreach(int i in Enumerable.Range(1, 4))
            {
                Assert.That(reference.Verses, Has.Member(i));
            }
        }

        [Test]
        public void ParseCombinedCommaAndRange()
        {
            string text = "Galaterne 6:2,4-5,8,16-18";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("Ga"));
            Assert.That(reference.Chapter, Is.EqualTo(6));
            Assert.That(reference.Verses.Length, Is.EqualTo(7));

            Assert.That(reference.Verses, Has.Member(2));

            foreach(int i in Enumerable.Range(4,2))
            {
                Assert.That(reference.Verses, Has.Member(i));
            }

            Assert.That(reference.Verses, Has.Member(8));

            foreach(int i in Enumerable.Range(16, 3))
            {
                Assert.That(reference.Verses, Has.Member(i));
            }
        }

        [Test]
        public void ParseOnlyVerseReferences()
        {
            string text = "II Johannes 10";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("2 Jn"));
            Assert.That(reference.Book.ChapterCount, Is.EqualTo(1));
            Assert.That(reference.Chapter, Is.EqualTo(1));
            Assert.That(reference.Verses.Length, Is.EqualTo(1));
            Assert.That(reference.Verses, Has.Member(10));
        }

        [Test]
        public void ParseOnlyChapterReference()
        {
            string text = "Gal. 3";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("Ga"));
            Assert.That(reference.Chapter, Is.EqualTo(3));
            Assert.That(reference.Verses.Length, Is.EqualTo(0));
        }

        [Test]
        public void ParseOnlyVerseWithRange()
        {
            string text = "3 Jn 5-7";

            Reference reference = Reference.Parse(text);

            Assert.That(reference.Book.ToString(), Is.EqualTo("3 Jn"));
            Assert.That(reference.Book.ChapterCount, Is.EqualTo(1));
            Assert.That(reference.Chapter, Is.EqualTo(1));
            Assert.That(reference.Verses.Length, Is.EqualTo(3));
            
            foreach(int i in Enumerable.Range(5,3))
            {
                Assert.That(reference.Verses, Has.Member(i));
            }
        }

        [Test]
        public void ParseChapterRangeNotSupported()
        {
            string text = "2 Kor. 5-7";
             
            Assert.Throws<FormatException>(() => Reference.Parse(text));
        }

        [Test]
        public void ParseInvalidRange()
        {
            Assert.Throws<FormatException>(() => Reference.Parse("1 Mos. 3:4-5-9"));
        }

        [Test]
        public void ParseRangeInReverse()
        {
            Assert.Throws<FormatException>(() => Reference.Parse("3 Mos. 6:12-8"));
        }

        [Test]
        public void ParseChapterTooHigh()
        {
            Assert.Throws<FormatException>(() => Reference.Parse("Sl 256:128"));
        }

        [Test]
        public void ScanForVersesFindsThemInsideParantheses()
        {
            ICollection<Reference> references = Reference.Scan("This is random text with a reference (1 Kor 13:3)");

            Assert.That(references.Count, Is.EqualTo(1));
            Assert.That(references.ElementAt(0), Is.EqualTo(Reference.Parse("1 Kor. 13:3")));
        }

        [Test]
        public void ScanCanFindSemicolonSeparatedScriptures()
        {
            ICollection<Reference> references = Reference.Scan("Lorem ipsum dolor -- 2 Kongebok 5:23;Or 3:13");

            Assert.That(references.Count, Is.EqualTo(2));

            Assert.That(references.ElementAt(0), Is.EqualTo(Reference.Parse("2 Konge. 5:23")));
            Assert.That(references.ElementAt(1), Is.EqualTo(Reference.Parse("Ordsp. 3:13")));
        }
    }
}
