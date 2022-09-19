python 执行 C#的代码文件
使用C#的动态编辑功能编辑运行C#代码，通过python.net库做C#和python的通信
需要安装pip install pythonnet==3.0.0rc4
PythonRunCSharpScript编辑成dll文件，在python项目中通过pythonnet引用这个dll文件，然后通过dll文件留出的函数传递C#脚本和执行结果。
PythonRunCSharpScriptCore是.net core版本的，但是.net core平台不支持CodeDomProvider动态编译，所以无法使用。
也可以用过可以用Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript运行C#脚本。
