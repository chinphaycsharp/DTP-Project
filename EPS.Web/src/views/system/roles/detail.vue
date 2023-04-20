<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Thông tin nhóm người dùng</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="Tên nhóm người dùng:"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="name" v-if="editing"
                                                  v-model="$v.role.name.$model"
                                                  :state="$v.role.name.$dirty? !$v.role.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        Vui lòng nhập tên nhóm người dùng
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ role.name }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="Mô tả:"
                                              label-align-md="right"
                                              label-for="description"
                                              :label-cols="4"
                                              :horizontal="true">
                                    <b-form-textarea id="description" v-model="role.description" v-if="editing"></b-form-textarea>
                                    <label class="col-form-label" v-if="!editing">{{ role.description }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageRole'])"><i class="fa fa-pencil-square-o"></i> Sửa</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> Quay lại</b-button>
                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> Ghi lại</b-button>
                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> Hủy</b-button>
                        </div>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'

    export default {
        name: 'RoleDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                role: {
                    id: null,
                    name: null,
                    description: null
                },
                editing: false
            }
        },
        computed: {
            roleId() {
                return this.$route.params.roleId;
            },
        },
        validations: {
            role: {
                name: { required }
            }
        },
        created() {
            this.loadRoleDetail();
        },
        methods: {
            loadRoleDetail() {
                var vm = this;
                return this.$services.get(`roles/${this.roleId}`).done(role => {
                    vm.role = role;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/system/roles' });
            },
            cancel() {
                var vm = this;
                this.loadRoleDetail().done(() => vm.editing = false);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`roles/${this.roleId}`, this.role).done(() => {
                        vm.$toastr.s('Cập nhật nhóm người dùng thành công');
                        vm.cancel();
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
