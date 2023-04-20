<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Tìm kiếm người dùng</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="Đơn vị:"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right">
                                    <unit-tree v-model="searchForm.unitId" ref="unitTree"></unit-tree>
                                    <b-form-radio-group v-model="searchForm.unitTraversalOption" name="radio-sub-component">
                                        <b-form-radio value="0">Trực thuộc đơn vị</b-form-radio>
                                        <b-form-radio value="1">Cả đơn vị con</b-form-radio>
                                        <b-form-radio value="2">Toàn bộ đơn vị cấp dưới</b-form-radio>
                                    </b-form-radio-group>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="Tên tài khoản:"
                                              label-for="username"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right">
                                    <b-form-input type="text" id="username"
                                                  placeholder="Nhập tài khoản ..."
                                                  v-model="searchForm.username">
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
                            dataUrl="users/list"
                            gridName="Danh sách người dùng"
                            ref="userTable">
                        <template v-slot:actions v-if="authorize(['ManageUser'])">
                            <router-link to="/system/users/create" tag="button" class="btn btn-primary" size="md">
                                Thêm mới
                            </router-link>
                            <b-button size="md" @click.stop="deletes" variant="danger">
                                Xóa
                            </b-button>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" v-if="authorize(['ViewUser'])" :to="{ path: `/system/users/detail/${row.item.id}` }" title="xem">
                                    <i class="fa fa-eye"></i>
                                </b-button>
                                <b-button v-if="authorize(['ManageUser'])" size="sm" :to="{ path: `/system/users/privileges/${row.item.id}` }" title="phân quyền">
                                    <i class="fa fa-cogs" aria-hidden="true"></i>
                                </b-button>
                                <b-button v-if="authorize(['ManageUser'])" size="sm" @click.stop="remove(row.item, row.index, $event.target)" class="mr-1" title="xóa">
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
        name: 'ListUser',
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    username: null,
                    unitId: null,
                    unitTraversalOption: 0
                },
                gridOptions: {
                    fields: [
                        { key: 'username', label: 'Tên tài khoản', sortable: true, sortDirection: 'asc' },
                        { key: 'fullName', label: 'Họ tên', sortable: true },
                        { key: 'unitName', label: 'Đơn vị', sortable: true },
                        { key: 'phoneNumber', label: 'Số điện thoại', sortable: true },
                        //{ key: 'roles', label: 'Vai trò', sortable: false },
                    ],
                    sortBy: 'username',
                }
            }
        },
        created() {
            var vm = this;
        },
        methods: {
            search() {
                this.$refs.userTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm('Bạn có chắc chắn muốn xóa người dùng này?')) {
                    this.$services.delete(`users/${item.id}`).done(() => {
                        vm.$toastr.s('Xóa người dùng thành công');
                        vm.search();
                    });
                }
            },
            deletes() {
                var vm = this;
                var selectedRows = this.$refs.userTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm('Bạn có chắc chắn muốn xóa các người dùng này?')) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`users?ids=${data}`).done(() => {
                            vm.$toastr.s('Xóa người dùng thành công');
                            vm.search();
                        });
                    }
                }
                else {
                    vm.$toastr.e('Vui lòng chọn người dùng cần xóa');
                }
            }
        }
    }
</script>
<style scoped>
</style>
