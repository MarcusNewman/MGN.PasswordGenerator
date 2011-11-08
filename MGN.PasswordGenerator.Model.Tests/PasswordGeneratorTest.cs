using MGN.PasswordGenerator.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MGN.PasswordGenerator.Model.Tests
{


    /// <summary>
    ///This is a test class for PasswordGeneratorTest and is intended
    ///to contain all PasswordGeneratorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PasswordGeneratorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GeneratePassword
        ///</summary>
        [TestMethod()]
        public void GeneratePasswordTests()
        {
            var generatedPassword = PasswordGenerator.GeneratePassword();

            Assert.IsFalse(String.IsNullOrEmpty(generatedPassword), "Generated password should not be null or empty.");
            Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.Special.ToCharArray()) >= 0, "Generated password should have at least one special charachter.");
            Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.Numbers.ToCharArray()) >= 0, "Generated password should have at least one digit.");
            Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.UpperCase.ToCharArray()) >= 0, "Generated password should have at least one uppercase letter.");
            Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.LowerCase.ToCharArray()) >= 0, "Generated password should have at least one lowercase letter.");
            Assert.IsTrue(generatedPassword.Length == 15, "Generated password should be 15 characters in length.");
            //TODO: Filler should be disctint from the other characters. This will result in 5 unique characters being selected for the password.
        }

        private static bool UsesOneCharIn(string generatedPassword, string alphabet)
        {
            var firstalphabetChar = generatedPassword[generatedPassword.IndexOfAny(alphabet.ToCharArray())];
            var alphabetWithoutFirst = alphabet.Remove(alphabet.IndexOf(firstalphabetChar), 1);
            return generatedPassword.IndexOfAny(alphabetWithoutFirst.ToCharArray()) < 0;
        }
        [TestMethod]
        public void PasswordGenerator_AlphabetTests()
        {
            Assert.IsTrue(PasswordGenerator.LowerCase == "abcdefghijklmnopqrstuvwxyz");
            Assert.IsTrue(PasswordGenerator.UpperCase == "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Assert.IsTrue(PasswordGenerator.Numbers == "0123456789");
            Assert.IsTrue(PasswordGenerator.Special == " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
            Assert.IsTrue(PasswordGenerator.All == (PasswordGenerator.LowerCase + PasswordGenerator.UpperCase + PasswordGenerator.Numbers + PasswordGenerator.Special));
        }
    }
}
