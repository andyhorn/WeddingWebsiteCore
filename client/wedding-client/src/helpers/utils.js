export const deepCopy = (objIn) => {
    let objOut, value, key;

    if (typeof objIn !== 'object' || objIn === null) {
        return objIn;
    }

    objOut = Array.isArray(objIn) ? [] : {};

    for (key in objIn) {
        value = objIn[key];

        objOut[key] = deepCopy(value);
    }

    return objOut;
}