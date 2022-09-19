using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;
using System.CodeDom.Compiler;
using System.Collections.Immutable;
using System.Reflection;

namespace PythonRunCSharpScriptCore
{
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
        /// <param name="code"></param>
        public void CompileScript(string code)
        {
            //
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.GenerateExecutable=false;
            parameters.GenerateInMemory=true;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
            if (results.Errors.HasErrors)
            {
                // 编译出错
            }
            else
            {
                // 执行
                Assembly assembly = results.CompiledAssembly;
                object? objHelloWord = assembly.CreateInstance("MyCalculator.Calculator");
                MethodInfo? methodInfo = objHelloWord?.GetType().GetMethod("add");
                object? val = methodInfo?.Invoke(objHelloWord, new object[] { 1.1, 2.2 });
                int debug = 0;
                debug++;
            }

            //CodeEntryPointMethod start = new CodeEntryPointMethod();
            //CodeMethodInvokeExpression cs1 = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("System.Console"), "WriteLine", new CodePrimitiveExpression("Hello World!!"));
            //start.Statements.Add(cs1);
        }

        /// <summary>
        /// 直接运行python脚本
        /// </summary>
        /// <param name="code"></param>
        public object RunScript(string code, Dictionary<string, object> variables = null)
        {
            Console.WriteLine($"RunScript 1{code}");

            Script script = Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript.Create("");
            //script.Options.AddImports("System");
            //script.Options.AddImports("System.Linq");
            if (variables !=null)
            {
                foreach (KeyValuePair<string, object> kvp in variables)
                {
                    script.ContinueWith($"object {kvp.Key}={kvp.Value}");
                }
            }
            script.ContinueWith(code);

            Console.WriteLine($"RunScript 2{script.Code}");

            ImmutableArray<Diagnostic> diagnostics = script.Compile();

            ScriptState result = script.RunAsync().Result;

            foreach (ScriptVariable val in result.Variables)
            {
                Console.WriteLine($"RunScript 3{val.Name},{val.Type},{val.Value}");
            }
            Console.WriteLine($"RunScript 4{result.ReturnValue}");

            return result.ReturnValue;
            //return 20;
        }
    }
}
