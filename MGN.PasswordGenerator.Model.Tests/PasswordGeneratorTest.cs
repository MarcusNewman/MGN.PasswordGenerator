using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MGN.PasswordGenerator.Model.Tests
{
    /// <summary>
    ///This is a test class for PasswordGenerator
    ///</summary>
    [TestClass()]
    public class PasswordGeneratorTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides information about and functionality for the current test run.
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

        #region private static string Names
        private static String ModelAssemblyName = "MGN.PasswordGenerator.Model";
        private static String PasswordGeneratorName = "PasswordGenerator";
        #endregion
        
        /// <summary>
        /// Gets the model Assembly.
        /// </summary>
        /// <returns>The model Assembly</returns>
        private static Assembly GetModelAssembly()
        {
            Assembly result = null;
            try
            {
                var relativeAssemblyPath = String.Format("..\\..\\..\\{0}\\bin\\Debug\\{0}.dll", ModelAssemblyName);
                result = Assembly.LoadFrom(relativeAssemblyPath);
            }
            catch (System.IO.FileNotFoundException)
            {
                Assert.Fail(ModelAssemblyName + " Assembly was not found.");
            }
            return result;
        }

        /// <summary>
        /// A test for the existence of the MGN.PasswordGenerator.Model Assembly.
        /// Assumes a relative path of MGN.PasswordGenerator\MGN.PasswordGenerator.Model\bin\Debug
        /// </summary>
        [TestMethod]
        public void ModelAssemblyShouldExist()
        {
            var ModelAssembly = GetModelAssembly();
            Assert.IsNotNull(ModelAssembly, ModelAssemblyName + " Assembly should not be null.");
        }

        /// <summary>
        /// Gets the PasswordGenerator Type.
        /// </summary>
        /// <returns>The Password Generator Type</returns>
        private static Type GetPasswordGeneratorType()
        {
            Type result = null;
            var ModelAssembly = GetModelAssembly();
            var Class1FullName = ModelAssemblyName + "." + PasswordGeneratorName;
            try
            {
                result = ModelAssembly.GetType(Class1FullName, true);
            }
            catch (TypeLoadException)
            {
                Assert.Fail(PasswordGeneratorName + " Type was not found.");
            }
            return result;
        }
        /// <summary>
        /// Test for existence of MGN.PasswordGenerator.Model.PasswordGenerator class
        /// PasswordGenerator must be public
        /// </summary>
        [TestMethod]
        public void ModelShouldContainPasswordGenerator()
        {
            var PasswordGeneratorType = GetPasswordGeneratorType();
            Assert.IsNotNull(PasswordGeneratorType, PasswordGeneratorName + " Type should not be null.");
        }       

        ///// <summary>
        /////A test for GeneratePassword
        /////</summary>
        //[TestMethod()]
        //public void GeneratePasswordTests()
        //{
        //    var generatedPassword = PasswordGenerator.GeneratePassword();
        //    Assert.IsFalse(String.IsNullOrEmpty(generatedPassword), "Generated password should not be null or empty.");
        //    Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.Special.ToCharArray()) >= 0, "Generated password should have at least one special charachter.");
        //    Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.Numbers.ToCharArray()) >= 0, "Generated password should have at least one digit.");
        //    Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.UpperCase.ToCharArray()) >= 0, "Generated password should have at least one uppercase letter.");
        //    Assert.IsTrue(generatedPassword.IndexOfAny(PasswordGenerator.LowerCase.ToCharArray()) >= 0, "Generated password should have at least one lowercase letter.");
        //    Assert.IsTrue(generatedPassword.Length == 15, "Generated password should be 15 characters in length.");
        //    //Filler should be distinct from the other characters. This will result in 5 unique characters being selected for the password.            
        //    var uniqueCharacters = new List<char>();
        //    foreach (var character in generatedPassword)
        //    {
        //        if (uniqueCharacters.Contains(character)) continue;
        //        uniqueCharacters.Add(character);
        //    }
        //    Assert.IsTrue(uniqueCharacters.Count == 5, "Generated password should have 5 unique characters");
        //}

        //private static bool UsesOneCharIn(string generatedPassword, string alphabet)
        //{
        //    var firstalphabetChar = generatedPassword[generatedPassword.IndexOfAny(alphabet.ToCharArray())];
        //    var alphabetWithoutFirst = alphabet.Remove(alphabet.IndexOf(firstalphabetChar), 1);
        //    return generatedPassword.IndexOfAny(alphabetWithoutFirst.ToCharArray()) < 0;
        //}
        //[TestMethod]
        //public void PasswordGenerator_AlphabetTests()
        //{
        //    Assert.IsTrue(PasswordGenerator.LowerCase == "abcdefghijklmnopqrstuvwxyz");
        //    Assert.IsTrue(PasswordGenerator.UpperCase == "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        //    Assert.IsTrue(PasswordGenerator.Numbers == "0123456789");
        //    Assert.IsTrue(PasswordGenerator.Special == " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
        //    Assert.IsTrue(PasswordGenerator.All == (PasswordGenerator.LowerCase + PasswordGenerator.UpperCase + PasswordGenerator.Numbers + PasswordGenerator.Special));
        //}
        //[TestMethod]
        //public void GenerateShouldTakeBooleanUseFiller()
        //{

        //}
    }
}
