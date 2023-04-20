<template>
    <div>
        <b-row>
            <b-col md="7">
                <h2 v-if="gridName != ''">{{gridName}}</h2>
                <slot name="gridName" v-if="gridName == ''"></slot>
            </b-col>
            <b-col md="5" class="text-right">
                <slot name="actions"></slot>
            </b-col>
        </b-row>
        <b-table show-empty
                 stacked="md"
                 :items="getItems"
                 :fields="options.fields"
                 :current-page="options.page"
                 :per-page="options.itemsPerPage"
                 :sort-by.sync="options.sortBy"
                 :sort-desc.sync="options.sortDesc"
                 :sort-direction="options.sortDirection"
                 striped
                 table-variant="light"
                 hover
                 bordered
                 :selectable="selectable"
                 :select-mode="selectMode"
                 @row-selected="onRowSelected"
                 :busy="isBusy"
                 ref="bTable">
            <template v-slot:table-busy>
                <div class="text-center text-danger my-2">
                    <b-spinner class="align-middle"></b-spinner>
                    <strong>Loading...</strong>
                </div>
            </template>
            <template v-slot:head(_selected)="row">
                <!--<input type="checkbox" @change="selectAllRows" v-if="selectMode=='multi'" />-->
                <b-form-checkbox v-model="isCheckAll" @change="selectAllRows" v-if="selectMode=='multi'"></b-form-checkbox>
            </template>
            <template v-slot:cell(_selected)="row">
                <b-form-checkbox v-model="row.rowSelected" @change="selectRow(row)"></b-form-checkbox>
            </template>
            <template v-slot:cell(_index)="row">
                {{ (options.page - 1) * options.itemsPerPage + row.index + 1 }}
            </template>
            <template v-for="(_, slot) of $scopedSlots" v-slot:[slot]="scope">
                <slot :name="slot" v-bind="scope" />
            </template>
        </b-table>
        <b-row v-if="options.totalRows > 0">
            <b-form inline class="col-md-12">
                <b-col md="6">
                    Hiển thị bản ghi từ <b>{{firstItem}}</b> đến <b>{{lastItem}}</b> trong tổng số <b>{{options.totalRows}}</b> bản ghi
                </b-col>
                <b-col md="6" class="my-1 pull-right input-group">
                    <label class="control-label ml-auto">Kích thước trang</label>
                    <b-form-select v-model="options.itemsPerPage" :options="options.pageOptions" style="flex:none; margin: 0px 5px;"></b-form-select>
                    <b-pagination align="right" :total-rows="options.totalRows" :per-page="options.itemsPerPage" v-model="options.page" class="my-0" />
                </b-col>
            </b-form>
        </b-row>
    </div>
</template>
<script>
    export default {
        name: 'BGrid',
        props: {
            gridOptions: {
                required: true,
            },
            dataUrl: {
                type: String,
                required: true
            },
            searchForm: {
            },
            selectable: {
                type: Boolean,
                default() {
                    return false;
                }
            },
            selectMode: {
                type: String,
                default() {
                    return 'multi';
                }
            },
            gridName: {
                type: String,
                required: true
            }
        },
        data() {
            return {
                isBusy: false,
                selectedRows: [],
                isCheckAll: false,
                options: {
                    fields: [],
                    page: 1,
                    itemsPerPage: 10,
                    totalRows: 0,
                    pageOptions: [10, 20, 50],
                    sortBy: '',
                    sortDesc: false,
                    sortDirection: 'asc'
                }
            }
        },
        computed: {
            firstItem() {
                return (this.options.page - 1) * this.options.itemsPerPage + 1;
            },
            lastItem() {
                return Math.min(this.options.totalRows, this.options.page * this.options.itemsPerPage);
            }
        },
        mounted: function () {
            Object.assign(this.options, this.gridOptions);

            if (this.options.fields.find(x => x.key == '_index') == null) {
                this.options.fields.unshift({ key: '_index', label: 'STT', sortable: false, thAttr: { width: 50 } });
            }

            if (this.options.fields.find(x => x.key == '_actions') == null) {
                this.options.fields.unshift({ key: '_actions', label: '', sortable: false, thAttr: { width: 70 } });
            }

            var checkboxField = this.options.fields.find(x => x.key == '_selected');
            if (this.selectable && checkboxField == null) {
                this.options.fields.unshift({ key: '_selected', label: '', sortable: false, thAttr: { width: 35 } });
            } else if (!this.selectable && checkboxField != null) {
                this.options.fields.remove(checkboxField);
            }
        },
        methods: {
            selectAllRows(e) {
                if (!this.isCheckAll) {
                    this.$refs.bTable.selectAllRows();
                } else {
                    this.$refs.bTable.clearSelected();
                }
            },
            onRowSelected(items) {
                this.selectedRows = items;
            },
            selectRow(row) {
                if (row.rowSelected) {
                    this.$refs.bTable.unselectRow(row.index);
                } else {
                    this.$refs.bTable.selectRow(row.index);
                }
            },
            getFormData() {
                var pagination = {
                    page: this.options.page,
                    itemsPerPage: this.options.itemsPerPage,
                    sortBy: this.options.sortBy,
                    sortDesc: this.options.sortDesc
                };

                var formData = $.extend({}, pagination, this.searchForm);

                return formData;
            },
            getItems(ctx) {
                var vm = this;
                var formData = this.getFormData();
                this.isBusy = true;

                return this.$services.get(this.dataUrl, formData).then((response) => {
                    vm.options.totalRows = response.totalRows;
                    return response.data;
                }, () => []).always(function () {
                    vm.isBusy = false;
                });
            },
            refresh: function () {
                this.options.page = 1;
                this.$refs.bTable.refresh();
            }
        }
    }
</script>

