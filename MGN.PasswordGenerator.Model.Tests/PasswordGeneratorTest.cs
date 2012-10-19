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
        private static String GeneratePasswordName = "GeneratePassword";
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

        /// <summary>
        /// Gets PasswordGenerator.GeneratePassword MethodInfo
        /// </summary>
        /// <returns>PasswordGenerator.GeneratePassword MethodInfo</returns>
        private static MethodInfo GetGeneratePasswordMethodInfo()
        {
            var PasswordGeneratorType = GetPasswordGeneratorType();
            return PasswordGeneratorType.GetMethod(GeneratePasswordName);
        }

        /// <summary>
        /// Gets PasswordGenerator.Special FieldInfo
        /// </summary>
        /// <returns>PasswordGenerator.Special FieldInfo</returns>
        private static String GetFieldByName(String name)
        {
            var passwordGeneratorType = GetPasswordGeneratorType();
            var fieldInfo = passwordGeneratorType.GetField(name);
            return (String)fieldInfo.GetValue(null);
        }

        /// <summary>
        /// Test for static method PasswordGenerator.GeneratePassword should return a string.
        /// </summary>
        [TestMethod]
        public void GeneratePasswordShouldBeAStaticMethodReturningAString()
        {
            var GetNameMemberInfo = GetGeneratePasswordMethodInfo();
            Assert.IsNotNull(GetNameMemberInfo, PasswordGeneratorName + "." + GeneratePasswordName + " method should exist.");
            Assert.IsTrue(GetNameMemberInfo.IsStatic, GeneratePasswordName + " should be static.");
            Assert.IsTrue(GetNameMemberInfo.ReturnType == typeof(string), GeneratePasswordName + " should return a string.");
        }

        /// <summary>
        ///A test for GeneratePassword
        ///</summary>
        [TestMethod()]
        public void GeneratePasswordTests()
        {
            var passwordGeneratorType = GetPasswordGeneratorType();
            var generatedPassword = (String)passwordGeneratorType.InvokeMember(GeneratePasswordName,
                                                                               BindingFlags.OptionalParamBinding |
                                                                               BindingFlags.InvokeMethod |
                                                                               BindingFlags.Static |
                                                                               BindingFlags.Public,
                                                                               null,
                                                                               null,
                                                                               new object[] { Type.Missing });
            Assert.IsFalse(String.IsNullOrEmpty(generatedPassword), "Generated password should not be null or empty.");
            Assert.IsTrue(generatedPassword.Length == 15, "Generated password should be 15 characters in length.");
            var special = GetFieldByName("Special");
            var lowerCase = GetFieldByName("LowerCase");
            var upperCase = GetFieldByName("UpperCase");
            var numbers = GetFieldByName("Numbers");
            Assert.IsTrue(generatedPassword.IndexOfAny(special.ToCharArray()) >= 0, "Generated password should have at least one special charachter.");
            Assert.IsTrue(generatedPassword.IndexOfAny(numbers.ToCharArray()) >= 0, "Generated password should have at least one digit.");
            Assert.IsTrue(generatedPassword.IndexOfAny(upperCase.ToCharArray()) >= 0, "Generated password should have at least one uppercase letter.");
            Assert.IsTrue(generatedPassword.IndexOfAny(lowerCase.ToCharArray()) >= 0, "Generated password should have at least one lowercase letter.");
            //Filler should be distinct from the other characters. This will result in 5 unique characters being selected for the password.            
            var uniqueCharacters = new System.Collections.Generic.List<char>();
            foreach (var character in generatedPassword)
            {
                if (uniqueCharacters.Contains(character)) continue;
                uniqueCharacters.Add(character);
            }
            Assert.IsTrue(uniqueCharacters.Count == 5, "Generated password should have 5 unique characters");
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
            var special = GetFieldByName("Special");
            var lowerCase = GetFieldByName("LowerCase");
            var upperCase = GetFieldByName("UpperCase");
            var numbers = GetFieldByName("Numbers");
            var all = GetFieldByName("All");
            Assert.IsTrue(lowerCase == "abcdefghijklmnopqrstuvwxyz");
            Assert.IsTrue(upperCase == "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Assert.IsTrue(numbers == "0123456789");
            Assert.IsTrue(special == " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
            Assert.IsTrue(all == (lowerCase + upperCase + numbers + special));
        }

        [TestMethod]
        public void GenerateShouldTakeBooleanUseFiller()
        {
            //var passwordGeneratorType = GetPasswordGeneratorType();
            //var generatedPassword = (String)passwordGeneratorType.InvokeMember(GeneratePasswordName,
            //                                                                   BindingFlags.OptionalParamBinding |
            //                                                                   BindingFlags.InvokeMethod |
            //                                                                   BindingFlags.Static |
            //                                                                   BindingFlags.Public,
            //                                                                   null,
            //                                                                   null,
            //                                                                   new object[] { Type.Missing, false });
            var passwordGeneratorType = GetPasswordGeneratorType();
            var generatePasswordMember = passwordGeneratorType.GetMethod(GeneratePasswordName,new Type[]{typeof(Int32), typeof(Boolean)});
            Assert.IsNotNull(generatePasswordMember, GeneratePasswordName + " should accept a boolean for it's second parameter.");
        }
    }
}
