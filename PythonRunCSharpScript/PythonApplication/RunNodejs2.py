# python 运行nodejs
import os
import tempfile
import subprocess
import json
#https://nodejs.dev/en/learn/run-nodejs-scripts-from-the-command-line/
# node -e "console.log(123)"
# node -e "require('.\javascript_test1.js')"

code = """
function add(a, b) {
    console.log(a)
    console.log(b)
    return a + b;
};

console.log('add');
let c=add(value1,value2);
console.log(c);
"""

inputParameters = {'value1': 10, 'value2': 1.4, 'value3': 'value3', 'value4': [
	1, 2, 3], 'value4': {'key1': 11}, 'a': 1, 'b': 2}
outputParameters = {'value1': 'c'}

parameters = []
paramentsName = []
debug = []
# 输入参数
for item in inputParameters.items():
	paramentsName.append(item[0])
	debug.append("console.log('%s:',%s)" % (item[0], item[0]))
	if isinstance(item[1], int):
		parameters.append("let %s = parseInt(%s);" % item)
	elif isinstance(item[1], float):
		parameters.append("let %s = parseFloat(%s);" % item)
	elif isinstance(item[1], str):
		parameters.append("let %s = '%s';" % item)
	elif isinstance(item[1], list):
		parameters.append("let %s = %s;" % item)
	elif isinstance(item[1], dict):
		parameters.append("let %s = %s;" % item)

# 输出的参数
# 给python中的value1赋值js脚本中的变量c
output = []
for item in outputParameters.items():
	if isinstance(item[1], str):
		output.append(f"\"{item[1]}\":{item[1]}")
	else:
		output.append(f"\"{item[1]}\":\"错误:js脚本中的变量名称只能使用字符串\"")

outputJson = "console.log(JSON.stringify({%s}));" % (','.join(output))
parametersDeclare = "\r\n".join(parameters)
parametersList = ",".join(paramentsName)
debugLog = "\r\n".join(debug)
main = """const main=(%s)=>{%s; %s;};""" % (parametersList, code, outputJson)

source = f"""
{parametersDeclare}
{debugLog}
{main}
//console.log('start');
main({parametersList});
//console.log('end');
"""

# 创建一个保存代码的临时文件
tempFile = tempfile.TemporaryFile(mode="w+")
# 脚本代码写入到临时文件中
tempFile.write(source)
tempFile.flush()

try:
	"""
	command = "node %s 1 2" % (file.name)
	with os.popen(command) as nodejs:
		result = nodejs.read()
		print(result)
	"""
	p = subprocess.Popen(
		["node", tempFile.name], stdout=subprocess.PIPE)
	out = p.stdout.read()
	print(out)
except Exception as e:
	raise e
finally:
	#关闭，删除临时文件
	tempFile.close()

# 从out中获取返回值
out1 = out.decode(encoding="utf-8")
outList = out1.split("\n")
result = json.loads(outList[len(outList)-2])
for key in outputParameters.keys():
	outputParameters[key] = result[outputParameters[key]]
	#item[0] = result[item[1]]
#for item in result.items():
pass
				