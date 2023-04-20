import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/index';

// Containers
const TheContainer = () => import('@/containers/TheContainer')

// Views - Pages
import Page404 from '@/views/pages/Page404'
//import Page500 from '@/views/pages/Page500'
import Login from '@/views/pages/Login'
//import Register from '@/views/pages/Register'



Vue.use(Router)

var router = new Router({
    mode: 'hash', // Demo is living in GitHub.io, so required!
    linkActiveClass: 'open active',
    scrollBehavior: () => ({ y: 0 }),
    routes: [
        {
            path: '/',
            redirect: '/home',
            name: 'default',
            component: TheContainer,
            meta: { requiresAuth: true, label: 'Trang chủ' },
            children: [
                {
                    path: 'home',
                    name: 'home',
                    meta: { label: 'Trang chủ', requiresAuth: true },
                    component: () => import('@/views/home')
                },
                {
                    path: 'system',
                    name: 'system',
                    redirect: '/system/users',
                    meta: { label: 'Hệ thống' },
                    component: {
                        render(c) { return c('router-view') }
                    },
                    children: [
                        {
                            path: 'users',
                            name: 'users',
                            redirect: '/system/users/list',
                            meta: { label: 'Người dùng' },
                            component: {
                                render(c) { return c('router-view') }
                            },
                            children: [
                                {
                                    path: 'list',
                                    name: 'user_list',
                                    meta: { label: 'Danh sách người dùng', requiresAuth: true, requiresPrivileges: ['ViewUser'] },
                                    component: () => import('@/views/system/users/list')
                                },
                                {
                                    path: 'create',
                                    name: 'create_user',
                                    meta: { label: 'Thêm mới người dùng', requiresAuth: true, requiresPrivileges: ['ManageUser'] },
                                    component: () => import('@/views/system/users/create')
                                },
                                {
                                    path: 'detail/:userId',
                                    name: 'user_detail',
                                    meta: { label: 'Thông tin người dùng', requiresAuth: true, requiresPrivileges: ['ViewUser', 'ManageUser'] },
                                    component: () => import('@/views/system/users/detail')
                                },
                                {
                                    path: 'privileges/:userId',
                                    name: 'user_privileges',
                                    meta: { label: 'Phân quyền người dùng', requiresAuth: true, requiresPrivileges: ['ManageUser'] },
                                    component: () => import('@/views/system/users/privileges')
                                }
                            ]
                        },
                        {
                            path: 'roles',
                            name: 'roles',
                            redirect: '/system/roles/list',
                            meta: { label: 'Nhóm người dùng' },
                            component: {
                                render(c) { return c('router-view') }
                            },
                            children: [
                                {
                                    path: 'list',
                                    name: 'role_list',
                                    meta: { label: 'Danh sách nhóm người dùng', requiresAuth: true, requiresPrivileges: ['ViewRole'] },
                                    component: () => import('@/views/system/roles/list')
                                },
                                {
                                    path: 'create',
                                    name: 'create_role',
                                    meta: { label: 'Thêm mới nhóm người dùng', requiresAuth: true, requiresPrivileges: ['ManageRole'] },
                                    component: () => import('@/views/system/roles/create')
                                },
                                {
                                    path: 'detail/:roleId',
                                    name: 'role_detail',
                                    meta: { label: 'Thông tin nhóm người dùng', requiresAuth: true, requiresPrivileges: ['ViewRole', 'ManageRole'] },
                                    component: () => import('@/views/system/roles/detail')
                                },
                                {
                                    path: 'privileges/:roleId',
                                    name: 'role_privileges',
                                    meta: { label: 'Phân quyền nhóm người dùng', requiresAuth: true, requiresPrivileges: ['ManageRole'] },
                                    component: () => import('@/views/system/roles/privileges')
                                }
                            ]
                        }
                    ]
                }
            ]
        },
        {
            path: '/login',
            name: 'login',
            component: Login
        },
        {
            path: '*',
            component: Page404
        }
    ]
})

router.beforeEach((to, from, next) => {
    var isAuthorized = true;
    for (var i = 0; i < to.matched.length; i++) {
        var route = to.matched[i];

        if (route.meta.requiresAuth) {
            // Check if this route require authentication
            if (!store.state.identity.isAuthenticated) {
                isAuthorized = false;
                break;
            }
            // Check if this route require admin permission
            else if (route.meta.adminOnly && !store.state.identity.isAdministrator) {
                isAuthorized = false;
                break;
            }
            // Check if this route require certain privileges
            else if (route.meta.requiresPrivileges && route.meta.requiresPrivileges.length > 0
                && intersect(store.state.identity.privileges, route.meta.requiresPrivileges).length == 0) {
                isAuthorized = false;
                break;
            }
        }
    }

    if (isAuthorized) {
        next();
    } else {
        next('/login');
    }
});

export default router;
