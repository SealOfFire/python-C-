// node ./javascript_test3.js 1 2
console.log("aaa");
function add(a, b) {
    return a + b;
};

console.log("argv:", process.argv[2]);
console.log("argv:", process.argv[3]);

const a = process.argv[2];
const b = process.argv[3];
console.log(add(a, b));

