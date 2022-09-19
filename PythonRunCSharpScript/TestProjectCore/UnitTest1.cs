using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace TestProjectCore
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PythonRunCSharpScript.CSharpScript script = new PythonRunCSharpScript.CSharpScript();

            string code = string.Empty;
            string fileName = "D:\\MyProgram\\PythonRunCSharpScript\\PythonApplication\\codes_ample\\CodeTest2.cs";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    code = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                fileStream.Close();
            }

            object result = script.RunScript(code, null);

        }

        [TestMethod]
        public void TestMethod2()
        {
            PythonRunCSharpScript.CSharpScript script = new PythonRunCSharpScript.CSharpScript();

            string code = string.Empty;
            string fileName = "D:\\MyProgram\\PythonRunCSharpScript\\PythonApplication\\codes_ample\\CodeTest2.cs";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    code = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                fileStream.Close();
            }

            string[] importDlls = { "System" };
            string typeName = "Calculator";
            string methodName = "add";
            object[] parameters = { 5d, 3.1d };
            object result = script.CompileScript(importDlls, code, typeName, methodName, parameters);

        }
    }
}