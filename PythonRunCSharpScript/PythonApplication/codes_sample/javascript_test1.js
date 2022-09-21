// node -e "require('./javascript_test1.js')()" 1 2

module.exports = () => {
    const a = process.argv[1];
    const b = process.argv[2];
    console.log(add(a, b));
}

function add(a, b) {
    return a + b;
};
