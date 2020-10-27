export default function (arr, key) {
    return arr.slice().sort((a, b) => a[key] < b[key] ? -1 : a[key] > b[key] ? 1 : 0);
}