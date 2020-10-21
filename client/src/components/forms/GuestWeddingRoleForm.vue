<template>
    <b-form-checkbox ref="checkbox" switch :value="value" @change="onValueChange($event.target.value)">{{ role.name }}</b-form-checkbox>
</template>

<script>
export default {
    name: "GuestWeddingRoleForm",
    props: [ "guestId", "roleId", "value" ],
    data() {
        return {
            isChecked: false
        }
    },
    computed: {
        role() {
            return this.$store.getters.findWeddingRole(this.roleId);
        }
    },
    methods: {
        onValueChange(e) {
            console.log(e)
            this.$emit("input", e);
        },
    }
}
</script>