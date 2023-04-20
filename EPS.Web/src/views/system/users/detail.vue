<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Thông tin người dùng</strong>
                    </div>
                    <b-form @submit.prevent="save" autocomplete="off">
                        <!-- disable autocomplete -->
                        <input autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="text" name="username" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="email" name="email" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="password" name="password" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <!-- disable autocomplete -->
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Tên tài khoản:"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="">
                                    <label class="col-form-label">{{ user.username }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Đơn vị:"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.user.unitId.$model"
                                                  :state="$v.user.unitId.$error ? false : null"></b-form-input>
                                    <unit-tree v-model="$v.user.unitId.$model" v-if="editing"></unit-tree>
                                    <b-form-invalid-feedback>
                                        Vui lòng nhập đơn vị
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ user.unitName }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Họ tên:"
                                              label-for="fullName"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="fullName" v-if="editing"
                                                  v-model="$v.user.fullName.$model"
                                                  @input="$v.user.fullName.$touch"
                                                  :state="$v.user.fullName.$dirty? !$v.user.fullName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        Vui lòng nhập họ tên
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ user.fullName }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row v-if="editing">
                            <b-col md="6">
                                <b-form-group label="Mật khẩu mới:"
                                              label-for="newPassword"
                                              :label-cols="4"
                                              label-align-md="right"
                                              :horizontal="true">
                                    <b-form-input type="password" id="newPassword"
                                                  v-model="$v.user.newPassword.$model"
                                                  @input="$v.user.newPassword.$touch"
                                                  :state="$v.user.newPassword.$dirty? !$v.user.newPassword.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.newPassword.minLength">
                                        Mật khẩu phải có ít nhất 8 ký tự
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="$v.user.newPassword.minLength && !$v.user.newPassword.passwordPattern">
                                        Mật khẩu phải chứa chữ hoa, chữ thường, số và ký tự đặc biệt
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Nhập lại mật khẩu mới:"
                                              label-for="newPasswordConfirmation"
                                              :label-cols="4"
                                              label-align-md="right"
                                              :horizontal="true">
                                    <b-form-input type="password" id="newPasswordConfirmation"
                                                  v-model="$v.user.newPasswordConfirmation.$model"
                                                  @input="$v.user.newPasswordConfirmation.$touch"
                                                  :state="$v.user.newPasswordConfirmation.$dirty? !$v.user.newPasswordConfirmation.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.newPasswordConfirmation.sameAsPassword">
                                        Mật khẩu không khớp
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Email:"
                                              label-for="email"
                                              :label-cols="4"
                                              label-align-md="right"
                                              :horizontal="true">
                                    <b-form-input type="email" id="email" v-if="editing"
                                                  v-model="user.email">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ user.email }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Số điện thoại:"
                                              label-for="phoneNumber"
                                              :label-cols="4"
                                              label-align-md="right"
                                              :horizontal="true">
                                    <b-form-input type="tel" id="phoneNumber" maxlength="20" v-if="editing"
                                                  v-model="user.phoneNumber">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ user.phoneNumber }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <b-form-group label="Nhóm quyền:"
                                              label-for="roles"
                                              :label-cols="2"
                                              label-align-md="right"
                                              :horizontal="true">
                                    <b-form-checkbox-group v-model="user.roleIds" class="checkbox-group" v-if="editing">
                                        <b-form-checkbox v-for="role in listRoles" :key="role.id" :value="role.id" class="col-md-6">{{ role.text }}</b-form-checkbox>
                                    </b-form-checkbox-group>
                                    <label class="col-form-label" v-if="!editing && user.roleNames">{{ user.roleNames.join(', ') }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageUser'])">Sửa</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing">Quay lại</b-button>
                            <b-button type="submit" variant="primary" v-if="editing">Ghi lại</b-button>
                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing">Hủy</b-button>
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
    import { required, sameAs, minLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/)

    export default {
        name: 'UserDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                user: {
                    id: null,
                    username: null,
                    newPassword: null,
                    newPasswordConfirmation: null,
                    unitId: null,
                    fullName: null,
                    email: null,
                    phoneNumber: null,
                    roleIds: []
                },
                editing: false,
                listRoles: []
            }
        },
        computed: {
            userId() {
                return this.$route.params.userId;
            },
        },
        validations: {
            user: {
                unitId: { required },
                fullName: { required },
                newPassword: { minLength: minLength(8), passwordPattern },
                newPasswordConfirmation: { sameAsPassword: sameAs('newPassword') },
            }
        },
        created() {
            var vm = this;
            this.$services.get('lookup/roles').done((response) => vm.listRoles = response);
            this.loadUserDetail();
        },
        methods: {
            loadUserDetail() {
                var vm = this;
                return this.$services.get(`users/${this.userId}`).done(user => {
                    user.newPassword = "";
                    user.newPasswordConfirmation = "";
                    vm.user = user;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/system/users' });
            },
            cancel() {
                var vm = this;
                this.loadUserDetail().done(() => vm.editing = false);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`users/${this.userId}`, this.user).done(() => {
                        vm.$toastr.s('Cập nhật người dùng thành công');
                        vm.cancel();
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
