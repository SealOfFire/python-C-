using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;
using Python.Runtime;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace PythonRunCSharpScript
{
    /// <summary>
    /// 和python交换数据的变量结构
    /// </summary>
    public class Variables : Dictionary<string, object>
    {
    }

    /// <summary>
    /// 和python交换数据的变量结构
    /// </summary>
    public class Variable
    {
        public string Name;
        public string CSharpType;
        public string PythonType;
        public object Value;
        public override string ToString()
        {
            return $"{this.Name} , ${this.CSharpType}, ${this.PythonType}, ${this.Value}";
        }
    }

    /// <summary>
    /// 处理C#脚本
    /// </summary>
    public class CSharpScript
    {
        /// <summary>
        /// 在内存中编译的方式运行C#代码
        /// .net core版本会报错平台不支持
        /// .net framework版本可以正常运行
        /// </summary>
        /// <param name="importDlls">引用的dll列表</param>
        /// <param name="code"></param>
        /// <param name="typeName"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object CompileScript(string[] importDlls, string code, string typeName, string methodName, object[] parameters = null)
        {
            //
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters compilerParameters = new CompilerParameters();
            foreach (string dll in importDlls)
            {
                compilerParameters.ReferencedAssemblies.Add(dll);
            }
            compilerParameters.GenerateExecutable=false;
            compilerParameters.GenerateInMemory=true;
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParameters, code);
            if (results.Errors.HasErrors)
            {
                // 编译出错
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError err in results.Errors)
                {
                    sb.AppendLine(err.ToString());
                }
                throw new Exception(sb.ToString());
            }
            else
            {
                // 执行
                Assembly assembly = results.CompiledAssembly;
                object objHelloWord = assembly.CreateInstance(typeName);
                MethodInfo methodInfo = objHelloWord?.GetType().GetMethod(methodName);
                object val = methodInfo?.Invoke(objHelloWord, parameters);
                //int debug = 0;
                //debug++;
                // 返回值是基本数据类型
                return val;
            }
        }

        public class Student
        {
            public Decimal Height;
            public Decimal Weight;
            public Decimal BMI;

            public string Status = string.Empty;
        }

        /// <summary>
        /// 直接运行python脚本
        /// C:\Users\SealO\AppData\Roaming\Python\Python38\site-packages\pythonnet\runtime\Python.Runtime.dll
        /// </summary>
        /// <param name="code"></param>
        public object RunScript(string code, Variable[] input)
        {
            Console.WriteLine($"RunScript 1: {code}");
            //dynamic a = 10;
            //object val = new object();
            // Python.Runtime.PyObject object2;

            Script script = Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript.Create(code);
            //script.Options.AddImports("System");
            //script.Options.AddImports("System.Linq");
            if (input !=null)
            {
                foreach (Variable val in input)
                {
                    Console.WriteLine($"RunScript type: {val}");
                    script =script.ContinueWith(test(val));
                }
            }
            script = script.ContinueWith(code);

            Console.WriteLine($"RunScript 2: {script.Code}");

            ImmutableArray<Diagnostic> diagnostics = script.Compile();

            ScriptState result = script.RunAsync().Result;

            foreach (ScriptVariable val in result.Variables)
            {
                Console.WriteLine($"RunScript 3: {val.Name},{val.Type},{val.Value}");
            }
            Console.WriteLine($"RunScript 4: {result.ReturnValue}");

            return result;
        }

        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string test(Variable val)
        {
            string result = string.Empty;
            switch (val.PythonType.ToLower())
            {
                case "number":
                    result=$"decimal {val.Name}={val.Value};";
                    break;
                case "int":
                    result=$"int {val.Name}={val.Value};";
                    break;
                case "float":
                    result=$"float {val.Name}={val.Value};";
                    break;
                case "str":
                    result=$"string {val.Name}={val.Value};";
                    break;
                case "bool":
                    result=$"bool {val.Name}={val.Value};";
                    break;
                case "list":
                    //result=$"object[] {val.Name}=new object[];";
                    break;
                case "dict":
                    //
                    break;
                default:
                    throw new Exception($"不支持{val.CSharpType}类型的参数");
            }
            return result;
        }

    }
}
