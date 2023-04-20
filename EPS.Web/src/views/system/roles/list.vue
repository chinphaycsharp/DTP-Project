<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Nhóm người dùng</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="Tên nhóm người dùng:"
                                              label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right">
                                    <b-form-input type="text" id="name"
                                                  placeholder="Nhập tên nhóm người dùng ..."
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="" :label-cols="4">
                                    <b-button type="submit" variant="primary">Tìm kiếm</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <br />
                    <b-grid :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            selectable
                            dataUrl="roles"
                            gridName="Danh sách nhóm người dùng"
                            ref="roleTable">
                        <template v-slot:actions>
                            <router-link to="/system/roles/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageRole'])">
                                Thêm mới
                            </router-link>
                            <b-button size="md" @click.stop="removeSelected" variant="danger" v-if="authorize(['ManageRole'])">
                                Xóa
                            </b-button>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/system/roles/detail/${row.item.id}` }" title="xem">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" :to="{ path: `/system/roles/privileges/${row.item.id}` }" title="phân quyền" v-if="authorize(['ManageRole'])">
                                    <i class="fa fa-cogs" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="remove(row.item, row.index, $event.target)" class="mr-1" title="xóa" v-if="authorize(['ManageRole'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'

    export default {
        name: 'ListRole',
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    filterText: null
                },
                gridOptions: {
                    fields: [
                        { key: 'name', label: 'Tên', sortable: true, sortDirection: 'asc' },
                        { key: 'description', label: 'Mô tả', sortable: false },
                    ],
                    sortBy: 'name',
                },
            }
        },
        created() {
        },
        methods: {
            search() {
                this.$refs.roleTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm('Bạn có chắc chắn muốn xóa nhóm người dùng này?')) {
                    this.$services.delete(`roles/${item.id}`).done(() => {
                        vm.$toastr.s('Xóa nhóm người dùng thành công');
                        vm.search();
                    });
                }
            },
            removeSelected() {
                var vm = this;
                var selectedRows = this.$refs.roleTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm('Bạn có chắc chắn muốn xóa các nhóm người dùng này?')) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`roles?ids=${data}`).done(() => {
                            vm.$toastr.s('Xóa nhóm người dùng thành công');
                            vm.search();
                        });
                    }
                }
                else {
                    vm.$toastr.e('Vui lòng chọn nhóm người dùng cần xóa');
                }
            }
        }
    }
</script>
<style scoped>
</style>
