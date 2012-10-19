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
        private static String MGN_PasswordGenerator_ModelAssemblyName = "MGN.PasswordGenerator.Model";
        private static String PasswordGeneratorClassName = "PasswordGenerator";

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

        private static Assembly GetMGN_PasswordGenerator_ModelAssembly()
        {
            Assembly result = null;
            try
            {
                var relativeAssemblyPath = String.Format("..\\..\\..\\{0}\\bin\\Debug\\{0}.dll", MGN_PasswordGenerator_ModelAssemblyName);
                result = Assembly.LoadFrom(relativeAssemblyPath);
            }
            catch (System.IO.FileNotFoundException)
            {
                Assert.Fail(MGN_PasswordGenerator_ModelAssemblyName + " assembly was not found.");
            }
            return result;
        }

        /// <summary>
        /// A test for the existence of the MGN.PasswordGenerator.Model assembly.
        /// Assumes a relative path of MGN.PasswordGenerator\MGN.PasswordGenerator.Model\bin\Debug
        /// </summary>
        [TestMethod]
        public void MGN_PasswordGenerator_ModelAssemblyShouldExist()
        {
            var MGN_PasswordGenerator_ModelAssembly = GetMGN_PasswordGenerator_ModelAssembly();
            Assert.IsNotNull(MGN_PasswordGenerator_ModelAssembly, MGN_PasswordGenerator_ModelAssemblyName + " assembly should not be null.");
        }

        private static Type GetPasswordGeneratorClassType()
        {
            Type result = null;
            var MGN_PasswordGenerator_ModelAssembly = GetMGN_PasswordGenerator_ModelAssembly();
            var Class1FullName = MGN_PasswordGenerator_ModelAssemblyName + "." + PasswordGeneratorClassName;
            try
            {
                result = MGN_PasswordGenerator_ModelAssembly.GetType(Class1FullName, true);
            }
            catch (TypeLoadException)
            {
                Assert.Fail(PasswordGeneratorClassName + " type was not found.");
            }
            return result;
        }
        /// <summary>
        /// Test for existence of MGN.PasswordGenerator.Model.PasswordGenerator class
        /// PasswordGenerator must be public
        /// </summary>
        [TestMethod]
        public void MGN_PasswordGenerator_ModelShouldContainPasswordGeneratorClassTest()
        {
            var PasswordGeneratorClassType = GetPasswordGeneratorClassType();
            Assert.IsNotNull(PasswordGeneratorClassType, PasswordGeneratorClassName + " type should not be null.");
        }

        /// <summary>
        /// Gets an instance of a PasswordGenerator object.
        /// </summary>
        /// <returns></returns>
        private static Object GetPasswordGeneratorClassInstance()
        {
            Object result = null;
            var PasswordGeneratorType = GetPasswordGeneratorClassType();
            result = Activator.CreateInstance(PasswordGeneratorType);
            return result;
        }

        /// <summary>
        /// Test instantiating a PasswordGenerator object.
        /// </summary>
        [TestMethod]
        public void PasswordGeneratorShouldBeInstatiable()
        {
            var PasswordGeneratorInstance = GetPasswordGeneratorClassInstance();
            Assert.IsNotNull(PasswordGeneratorInstance, PasswordGeneratorClassName + " was not instanciable.");
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
