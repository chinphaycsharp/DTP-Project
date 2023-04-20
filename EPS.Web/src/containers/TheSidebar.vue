<template>
    <CSidebar fixed
              :minimize="minimize"
              :show="show"
              @update:show="(value) => $store.commit('set', ['sidebarShow', value])">
        <CSidebarBrand class="d-md-down-none" to="/">
            <img class="navbar-brand-full" src="img/logo.png" height="35" alt="Logo" style="padding-right: 5px">
            <span v-if="!minimize" style="font-style:italic">
                AT Innovations
            </span>
        </CSidebarBrand>

        <CRenderFunction flat :content-to-render="nav" />
        <CSidebarMinimizer class="d-md-down-none"
                           @click.native="$store.commit('set', ['sidebarMinimize', !minimize])" />
    </CSidebar>
</template>

<script>
    import nav from './_nav'

    export default {
        name: 'TheSidebar',
        computed: {
            nav() {
                var result = JSON.parse(JSON.stringify(nav));
                var navItems = result[0]._children;
                var privileges = this.$store.state.identity.privileges;

                RemoveUnauthorizedItems(navItems);

                function RemoveUnauthorizedItems(items) {
                    if (items && items.length > 0) {
                        for (var i = items.length - 1; i >= 0; i--) {
                            var item = items[i];

                            var requiresPrivileges = item.requiresPrivileges || [];
                            if (item.items && item.items.length > 0) {
                                requiresPrivileges = requiresPrivileges.concat(item.items.map(x => x.requiresPrivileges).reduce((x, y) => (x || []).concat(y || [])));
                            }

                            if (requiresPrivileges && requiresPrivileges.length > 0
                                && intersect(privileges, requiresPrivileges).length == 0) {
                                items.remove(item);
                            } else {
                                RemoveUnauthorizedItems(item.items);
                            }
                        }
                    }
                }

                return result;
            },
            show() {
                return this.$store.state.common.sidebarShow
            },
            minimize() {
                return this.$store.state.common.sidebarMinimize
            }
        }
    }
</script>
