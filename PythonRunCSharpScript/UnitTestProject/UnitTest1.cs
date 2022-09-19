using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PythonRunCSharpScript.CSharpScript script = new PythonRunCSharpScript.CSharpScript();

            string code = string.Empty;
            string fileName = "D:\\MyProgram\\PythonRunCSharpScript\\PythonApplication\\codes_ample\\CodeTest.cs";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    code = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                fileStream.Close();
            }

            string[] importDlls = { "System.dll" };
            string typeName = "Calculator";
            string methodName = "add";
            object[] parameters = { 5d, 3.1d };
            object result = script.CompileScript(importDlls, code, typeName, methodName, parameters);
        }

        [TestMethod]
        public void TestMethod2()
        {
            PythonRunCSharpScript.CSharpScript script = new PythonRunCSharpScript.CSharpScript();

            string code = string.Empty;
            string fileName = "D:\\MyProgram\\PythonRunCSharpScript\\PythonApplication\\codes_ample\\CodeTest.cs";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    code = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                fileStream.Close();
            }

            string[] importDlls = { "System.dll" };
            string typeName = "Calculator";
            string methodName = "test1";
            object[] parameters = null;
            object result = script.CompileScript(importDlls, code, typeName, methodName, parameters);
        }

        [TestMethod]
        public void TestMethod3()
        {
            PythonRunCSharpScript.CSharpScript script = new PythonRunCSharpScript.CSharpScript();

            string code = string.Empty;
            string fileName = "D:\\MyProgram\\PythonRunCSharpScript\\PythonApplication\\codes_ample\\CodeTest.cs";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    code = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                fileStream.Close();
            }

            string[] importDlls = { "System.dll" };
            string typeName = "Calculator";
            string methodName = "test2";
            object[] parameters = null;
            object result = script.CompileScript(importDlls, code, typeName, methodName, parameters);
        }
    }
}
