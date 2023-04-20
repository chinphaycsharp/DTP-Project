<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Thêm mới nhóm người dùng</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="Tên nhóm người dùng:"
                                              label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="right"
                                              label-class="required">
                                    <b-form-input type="text" id="name"
                                                  v-model="$v.role.name.$model"
                                                  :state="$v.role.name.$dirty? !$v.role.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        Vui lòng nhập tên nhóm người dùng
                                    </b-form-invalid-feedback>
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
                                    <b-form-textarea id="description" v-model="role.description"></b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label-cols="4"
                                              :horizontal="true">
                                    <b-button v-if="authorize(['ManageRole'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> Ghi lại</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> Hủy</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
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
        name: 'CreateRole',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                role: {
                    name: null,
                    description: null
                }
            }
        },
        validations: {
            role: {
                name: { required }
            }
        },
        created() {

        },
        methods: {
            cancel() {
                this.$router.push({ path: '/system/roles/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('roles', this.role).done((id) => {
                        vm.$toastr.s('Tạo mới nhóm người dùng thành công');
                        vm.$router.push({ path: '/system/roles/list' });
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
