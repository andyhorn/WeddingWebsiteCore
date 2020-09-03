export default (data) => {
    if (data) {
        const { userId, firstName, lastName, email, token } = data;
        const userData = Object.assign({}, { userId, firstName, lastName, email });

        return { userData, token };
    }

    return null;
}