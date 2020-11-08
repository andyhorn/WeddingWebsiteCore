import moduleFactory from "@/store/modules/moduleFactory";

const name = "address";
const idKey = "addressId";
const endpoint = "addresses";
const namePlural = "addresses";

export default moduleFactory(name, idKey, endpoint, namePlural);
