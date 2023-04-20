<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Thêm mới người dùng</strong>
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
                                              label-for="username"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="required">
                                    <b-form-input type="text" id="username"
                                                  v-model="$v.user.username.$model"
                                                  :state="$v.user.username.$dirty? !$v.user.username.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        Vui lòng nhập tên tài khoản
                                    </b-form-invalid-feedback>
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
                                              label-class="required">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.user.unitId.$model"
                                                  :state="$v.user.unitId.$error ? false : null">
                                    </b-form-input>
                                    <unit-tree v-model="$v.user.unitId.$model"></unit-tree>
                                    <b-form-invalid-feedback>
                                        Vui lòng nhập đơn vị
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Họ tên:"
                                              label-for="fullName"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="required">
                                    <b-form-input type="text" id="fullName"
                                                  v-model="$v.user.fullName.$model"
                                                  @input="$v.user.fullName.$touch"
                                                  :state="$v.user.fullName.$dirty? !$v.user.fullName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback id="fullNameFeedback">
                                        Vui lòng nhập họ tên
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Mật khẩu:"
                                              label-for="password"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="required">
                                    <b-form-input type="password" id="password"
                                                  v-model="$v.user.password.$model"
                                                  @input="$v.user.password.$touch"
                                                  :state="$v.user.password.$dirty? !$v.user.password.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.password.required">
                                        Vùi lòng nhập mật khẩu
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.user.password.minLength">
                                        Mật khẩu phải có ít nhất 8 ký tự
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="$v.user.password.minLength && !$v.user.password.passwordPattern">
                                        Mật khẩu phải chứa chữ hoa, chữ thường, số và ký tự đặc biệt
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Nhập lại mật khẩu:"
                                              label-for="passwordConfirmation"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="required">
                                    <b-form-input type="password" id="passwordConfirmation"
                                                  v-model="$v.user.passwordConfirmation.$model"
                                                  @input="$v.user.passwordConfirmation.$touch"
                                                  :state="$v.user.passwordConfirmation.$dirty? !$v.user.passwordConfirmation.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.passwordConfirmation.required">
                                        Vui lòng nhập lại mật khẩu
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="$v.user.passwordConfirmation.required && !$v.user.passwordConfirmation.sameAsPassword">
                                        Mật khẩu không khớp
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Số điện thoại:"
                                              label-for="phoneNumber"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="">
                                    <b-form-input type="tel" id="phoneNumber" maxlength="20"
                                                  v-model="user.phoneNumber">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Email:"
                                              label-for="email"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="">
                                    <b-form-input type="email" id="email"
                                                  v-model="user.email">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <b-form-group label="Nhóm người dùng:"
                                              label-for="roles"
                                              :label-cols="2"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="">
                                    <b-form-checkbox-group v-model="user.roleIds" class="checkbox-group">
                                        <b-form-checkbox v-for="role in listRoles" :key="role.id" :value="role.id" class="col-md-6">{{ role.text }}</b-form-checkbox>
                                    </b-form-checkbox-group>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="submit" v-if="authorize(['ManageUser'])" variant="primary">Ghi lại</b-button>
                            <b-button type="button" variant="secondary" @click="cancel">Hủy</b-button>
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
        name: 'CreateUser',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                listRoles: [],
                user: {
                    username: null,
                    password: null,
                    passwordConfirmation: null,
                    unitId: null,
                    fullName: null,
                    email: null,
                    phoneNumber: null,
                    roleIds: []
                },
                editing: false
            }
        },
        validations: {
            user: {
                username: { required },
                password: { required, minLength: minLength(8), passwordPattern },
                passwordConfirmation: { required, sameAsPassword: sameAs('password') },
                unitId: { required },
                fullName: { required }
            }
        },
        created() {
            var vm = this;
            this.$services.get('lookup/roles').done((response) => vm.listRoles = response);
        },
        methods: {
            cancel() {
                this.$router.push({ path: '/system/users/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('users', this.user).done((id) => {
                        vm.$toastr.s('Tạo mới người dùng thành công');
                        vm.$router.push({ path: '/system/users/list' });
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
