using NUnit.Framework;

namespace RomanNumerals.Core.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private MainViewModel mainViewModel;

        private static readonly object[] ArabicToRomanNumeralsSource = new object[]
        {
            new object[] { 1, "I" },
            new object[] { 3, "III" },
            new object[] { 4, "IV" },
            new object[] { 5, "V" },
            new object[] { 10, "X" },
            new object[] { 50, "L" },
            new object[] { 100, "C" },
            new object[] { 500, "D" },
            new object[] { 1000, "M" },
            new object[] { 8, "VIII" },
            new object[] { 9, "IX" },
            new object[] { 11, "XI" },
            new object[] { 19, "XIX" },
            new object[] { 20, "XX" },
            new object[] { 34, "XXXIV" },
            new object[] { 39, "XXXIX" },
            new object[] { 49, "XLIX" },
            new object[] { 99, "XCIX" },
            new object[] { 999, "CMXCIX" },
            new object[] { 1999, "MCMXCIX" },
            new object[] { 3999, "MMMCMXCIX" }
        };

        [SetUp]
        public void Setup()
        {
            mainViewModel = new MainViewModel();
        }

        [Test]
        [TestCaseSource(nameof(ArabicToRomanNumeralsSource))]
        public void ArabicToRomanNumerals(int arabic, string roman)
        {
            mainViewModel.ArabicNumeral = arabic;
            mainViewModel.ArabicToRoman();

            Assert.That(mainViewModel.RomanNumeral, Is.EqualTo(roman));
            Assert.That(mainViewModel.CurrentError, Is.EqualTo(""));
        }

        [Test]
        [TestCaseSource(nameof(ArabicToRomanNumeralsSource))]
        public void RomanToArabicNumerals(int arabic, string roman)
        {
            mainViewModel.RomanNumeral = roman;
            mainViewModel.RomanToArabic();

            Assert.That(mainViewModel.ArabicNumeral, Is.EqualTo(arabic));
            Assert.That(mainViewModel.CurrentError, Is.EqualTo(""));
        }

        [Test]
        public void ArabicZeroToRoman()
        {
            mainViewModel.ArabicNumeral = 0;
            mainViewModel.ArabicToRoman();

            Assert.That(mainViewModel.RomanNumeral, Is.EqualTo("nulla"));
            Assert.That(mainViewModel.CurrentError, Is.EqualTo(""));
        }

        [Test]
        public void FourCharactersInARow()
        {
            mainViewModel.RomanNumeral = "XXXX";
            mainViewModel.RomanToArabic();

            Assert.That(mainViewModel.ArabicNumeral, Is.EqualTo(0));
            Assert.That(mainViewModel.CurrentError, Is.EqualTo("Cannot have more than 4 X characters in a row."));
        }

        [Test]
        public void NegativeNumber()
        {
            mainViewModel.ArabicNumeral = -5;
            mainViewModel.ArabicToRoman();

            Assert.That(mainViewModel.RomanNumeral, Is.EqualTo(""));
            Assert.That(mainViewModel.CurrentError, Is.EqualTo("Arabic Number must be a non-negative integer."));
        }
    }
}