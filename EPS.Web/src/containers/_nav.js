export default [
    {
        _name: 'CSidebarNav',
        _children: [
            {
                _name: 'CSidebarNavItem',
                code: 'home',
                name: 'Trang chủ',
                to: '/home',
                icon: 'cil-home'
            },
            {
                _name: 'CSidebarNavDropdown',
                code: 'system',
                name: 'Quản lý hệ thống',
                to: '/system',
                icon: 'cil-settings',
                items: [
                    {
                        code: 'manage_users',
                        name: 'Người dùng',
                        to: '/system/users',
                        icon: 'cil-user',
                        requiresPrivileges: ['ViewUser', 'ManageUser']
                    },
                    {
                        code: 'manage_roles',
                        name: 'Nhóm người dùng',
                        to: '/system/roles',
                        icon: 'cil-people',
                        requiresPrivileges: ['ViewRole', 'ManageRole']
                    }
                ]
            }
        ]
    }
]