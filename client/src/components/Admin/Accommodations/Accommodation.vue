<template>
    <b-container class="my-2 p-1 border-bottom border-dark">
        <h3>{{ accommodation.name }}</h3>
        <p>{{ accommodation.description }}</p>
        <div v-if="address != null">
            <p>
                {{ `${address.streetNumber} ${address.streetName}`}}<br />
                <span v-if="address.streetDetail != null">
                    {{ address.streetDetail }}<br />
                </span>
                {{ `${address.city}, ${address.state} ${address.postalCode}`}}
            </p>
        </div>
    </b-container>
</template>

<script>
export default {
    name: "AdminAccommodation",
    props: ["accommodation"],
    data() {
        return {
            address: null
        }
    },
    created() {
        if (this.accommodation.addressId != null) {
            const addr = this.$store.getters.findAddress(this.accommodation.addressId);
            this.address = addr;
        }
    }
}
</script>