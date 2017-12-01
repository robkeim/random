module.exports = {
    assertAreEqual: (expected, actual) => {
        if (expected === actual) {
            console.log('OK ' + expected);
        } else {
            console.log('Expected = ' + expected);
            console.log('Actual   = ' + actual);
            throw Error('Unexpected results');
        }
    }
}
