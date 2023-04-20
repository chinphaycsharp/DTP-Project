<template>
    <CContainer class="d-flex content-center min-vh-100">
        <CRow>
            <CCol>
                <CCardGroup>
                    <CCard class="p-4">
                        <CCardBody>
                            <CForm @submit.prevent="login">
                                <h1>Login</h1>
                                <p class="text-muted">Sign In to your account</p>
                                <CInput placeholder="Username" v-model="username"
                                        autocomplete="username email">
                                    <template #prepend-content>
                                        <CIcon name="cil-user" />
                                    </template>
                                </CInput>
                                <CInput placeholder="Password" v-model="password"
                                        type="password"
                                        autocomplete="curent-password">
                                    <template #prepend-content>
                                        <CIcon name="cil-lock-locked" />
                                    </template>
                                </CInput>
                                <CRow>
                                    <CCol col="6" class="text-left">
                                        <CButton color="primary" class="px-4" type="submit">Login</CButton>
                                    </CCol>
                                    <CCol col="6" class="text-right">
                                        <CButton color="link" class="px-0">Forgot password?</CButton>
                                        <CButton color="link" class="d-md-none">Register now!</CButton>
                                    </CCol>
                                </CRow>
                            </CForm>
                        </CCardBody>
                    </CCard>
                </CCardGroup>
            </CCol>
        </CRow>
    </CContainer>
</template>

<script>
    import { validationMixin } from 'vuelidate'
    import { required } from 'vuelidate/lib/validators'

    export default {
        name: 'Login',
        mixins: [validationMixin],
        data() {
            return {
                username: null,
                password: null,
                errorMsg: null,
            };
        },
        validations: {
            username: {
                required
            },
            password: {
                required,
            }
        },
        methods: {
            login() {
                var vm = this;
                this.errorMsg = null;
                this.$store.dispatch("authenticate", { username: this.username, password: this.password })
                    .done(() => vm.$router.push({ path: '/' }))
                    .fail(
                        function (errors) {
                            if (errors.responseJSON && errors.responseJSON.error_description) {
                                vm.errorMsg = errors.responseJSON.error_description;
                            } else {
                                vm.errorMsg = errors.statusText;
                            }
                        }
                    );
            }
        }
    }
</script>
