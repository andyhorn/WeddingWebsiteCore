<template>
  <div class="box" :class="{ fullwidth: fullwidth, centered: centered }">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: "Box",
  props: ["fullwidth", "centered"]
};
</script>

<style scoped>
.box {
  border: 1px solid rgba(155, 155, 155, 0.5);
  border-radius: 5px;
  padding: 8px;
  margin-top: 3px;
  margin-bottom: 3px;
  width: auto;
}
.centered {
  margin: 0 auto;
  margin-top: 3px;
  margin-bottom: 3px;
}
.fullwidth {
  width: 100%;
}
</style>