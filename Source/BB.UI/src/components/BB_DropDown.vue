<template>
  <div :id="DropDownId" class="BB-DropDown">
    <select v-model="selectedData">
      <option v-for="option in optionData" :value="option.Value">{{option.Text}}</option>
    </select>
  </div>
</template>

<script lang="ts">
  import { Component, Vue, Prop, Watch } from 'vue-property-decorator';
  import { AxiosResponse, AxiosError } from 'axios';
  import '../assets/bibleMenu.scss';

  @Component({})
  export default class BB_DropDown extends Vue {
    @Prop(String) ApiEndpoint!: string;
    @Prop(Number) InitialValue: any;
    @Prop(String) DropDownId!: string;

    optionData: DropDownData[] = [];
    selectedData: number = 0;

    created() {
      this.loadData();
      this.selectedData = this.InitialValue;
    }

    @Watch('ApiEndpoint')
    onApiEndpointChanged(value: string, oldValue: string) {
      this.loadData();
    }

    @Watch('selectedData')
    onSelectedDataChanged(value: string, oldValue: string) {
      this.$emit("changed", value);
    }

    private loadData() {
      this.axios
        .get(this.ApiEndpoint)
        .then((response: AxiosResponse<DropDownData[]>) => {
          this.optionData = response.data;
        }).catch((err: AxiosError) => {
          alert(JSON.stringify(err));
        });
    }
  }
</script>

<style>
  .BB-DropDown {
    display: inline-block;
  }
</style>
