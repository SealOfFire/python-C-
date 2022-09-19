# pip install pythonnet==3.0.0rc4
import clr

"""
from clr_loader import get_coreclr
from pythonnet import set_runtime

rt = get_coreclr(r"D:\MyProgram\PythonRunCSharpScript\PythonRunCSharpScript\bin\Debug\net6.0\PythonRunCSharpScript.deps.json")
set_runtime(rt)
"""

path = r"D:\\MyProgram\\PythonRunCSharpScript\\PythonRunCSharpScript\\bin\\Debug\\"
dllFilesPath = []
# framework
dllFilesPath.append(path+"Microsoft.CodeAnalysis.CSharp.dll")
dllFilesPath.append(path+"Microsoft.CodeAnalysis.CSharp.Scripting.dll")
dllFilesPath.append(path+"Microsoft.CodeAnalysis.dll")
dllFilesPath.append(path+"Microsoft.CodeAnalysis.Scripting.dll")
dllFilesPath.append(path+"PythonRunCSharpScript.dll")
dllFilesPath.append(path+"System.Buffers.dll")
dllFilesPath.append(path+"System.Collections.Immutable.dll")
dllFilesPath.append(path+"System.Memory.dll")
dllFilesPath.append(path+"System.Numerics.Vectors.dll")
dllFilesPath.append(path+"System.Reflection.Metadata.dll")
dllFilesPath.append(path+"System.Runtime.CompilerServices.Unsafe.dll")
dllFilesPath.append(path+"System.Threading.Tasks.Extensions.dll")
dllFilesPath.append(path+"System.Text.Encoding.CodePages.dll")
dllFilesPath.append(path+"System.Threading.Tasks.Extensions.dll")
#dllFilesPath.append("System.Runtime.CompilerServices.DynamicAttribute");
#dllFilesPath.append(path+"PythonRunCSharpScript.dll")
# core
path = r"D:\\MyProgram\\PythonRunCSharpScript\\PythonRunCSharpScriptCore\\bin\\Debug\\net6.0\\"
#dllFilesPath.append(path+"PythonRunCSharpScriptCore.dll")

# 添加运行环境需要的dll
for value in dllFilesPath:
    clr.AddReference(value)

# from 命名空间 import 类
#from MyCalculator2 import Class
from PythonRunCSharpScript import CSharpScript
from PythonRunCSharpScript import Calculator
from PythonRunCSharpScript import Variables

# 实例化类对象
calc = Calculator()
print("Addition: "+str(calc.add(3, 2)))
print("Subtraction: "+str(calc.subtract(3, 2)))
print("Multiplication: "+str(calc.multiply(3, 2)))
print("Division: "+str(calc.divide(3, 2)))

cshsrpScript = CSharpScript()

with open(r"D:\\MyProgram\\PythonRunCSharpScript\\PythonApplication\\codes_ample\\CodeTest.cs") as f:
    lines = f.read()

#variables = Variables()
#variables.Add("num_one", 5)
#variables.Add("num_two", 10)
#variable = {"num_one": 222, "num_two": 444}
#output = ["num_one", "num_two"]
##variables = {}
##variables["value1"] = "string"
##variables["value2"] = 10
#value = cshsrpScript.RunScript(
#    lines, output=output, variable=variable, variables=variables)
#print("RunScript:")
#print("end")

importDlls = ["System.dll"]
print(cshsrpScript.CompileScript(importDlls,lines, "Calculator","add",[3.1,4.5]))
print(cshsrpScript.CompileScript(importDlls,lines, "Calculator","test1", None))
val = cshsrpScript.CompileScript(importDlls,lines, "Calculator","test2", None)
print(val)
