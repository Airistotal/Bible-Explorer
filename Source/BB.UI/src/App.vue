<template>
    <div id="app">
        <div id="pageContent">
            <template v-for="verse in verses">
                <sup>{{verse.Verse}}</sup>
                <div class="Word" v-for="word in verse.ComparedWords">
                    {{word.MainWord}}
                </div>
            </template>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import { AxiosResponse, AxiosError } from 'axios';

    @Component({})
    export default class App extends Vue {
        verses: ComparedBibleVerse[] = [];

        created() {
            this.axios
                .get("./api/BibleComparer?mainBible=2&book=1&chapter=1&compareBible=3")
                .then((response: AxiosResponse<ComparedBibleVerse[]>) => {
                    try {
                        this.verses = response.data;
                    } catch(e) {
                        alert(e);
                    }
                }).catch((err: AxiosError) => {
                    alert(JSON.stringify(err));
                });
        }
    }
</script>

<style>
    .Word {
        display: inline-block;
        padding-left: 4px;
    }

    .Highlight {

    }

    .HighlightStart {

    }

    .HighlightEnd {

    }

    #pageContent {
        margin: 75px;
        padding: 50px;
        box-shadow: 0px 0px 7px #AEAEAE;
    }
</style>
