# python 运行nodejs
import os
import tempfile
import subprocess
#https://nodejs.dev/en/learn/run-nodejs-scripts-from-the-command-line/
# node -e "console.log(123)"
# node -e "require('.\javascript_test1.js')"

code = """
%s

const a = process.argv[2];
const b = process.argv[3];
console.log(%s(a,b));

"""

functionName = "add"
parameters = [1, 2]

# 脚本代码路径
path = os.path.dirname(os.path.abspath(__file__))
path = os.path.join(path, "codes_sample")
path = os.path.join(path, "javascript_test2.js")

# 读取脚本代码
with open(path) as f:
	lines = f.read()
code = code % (lines, functionName)

#code = """console.log(""aaa"");"""
# 代码存储为临时文件
tmp = tempfile.gettempdir()
file = tempfile.TemporaryFile(mode="w+")
# 脚本代码写入到临时文件中
file.write(code);
file.flush();
savePath = os.path.dirname(os.path.abspath(__file__))
savePath = os.path.join(path, "codes_sample.js")

# 执行脚本代码
command = """node -e "%s" """ % (code)
command = "node %s 1 2" % (file.name)
with os.popen(command) as nodejs:
	result = nodejs.read()
	print(result)

#
p = subprocess.Popen(["node",file.name, "1", "2"], stdout=subprocess.PIPE)
out = p.stdout.read()
print(out)

# 关闭文件，文件被删除
file.close();
