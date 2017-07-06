(function () {

    'use strict';
    angular.module('app').constant('Resources', {
        users: 'users',
        languages: 'languages',
        translations: 'translations',
        userRoles: 'userroles',
        userRoleGroups: 'userrolegroups',
        states: 'states',
        lwpSettings: 'lwpsettings',
        toBeTreatedLwpSettings: 'tobetreatedlwpsettings',
        toBeTreatedMobileDevices: 'tobetreatedmobiledevices',
        mobileDevices: 'mobiledevices',
        lwpDevices: 'lwpdevices',
        loginSites: 'loginsites',
        loginLicences: 'loginlicences',
        repairReasons: 'repairreasons',
        platforms: 'platforms',
        flocIds: 'flocids',
        deviceTypes: 'devicetypes',
        productTypes: 'producttypes',
        stateChanges: 'statechanges',
        orderStateChanges: 'orderstatechanges',
        purchaseOrders: 'purchaseorders',
        orderItems: 'orderitems',
        orderItemCancellations: 'orderitemcancellations',
        stateKinds: 'statekinds',
        dashboards: 'dashboard',
        synonyms: 'synonyms',
        deviceReplacements: 'devicereplacements'
    });
    angular.module('app').constant('Roles', {
        settings: 'Settings',
        poOverview: 'PoOverview',
        poNew: 'PoNew',
        poEdit: 'PoEdit',
        poDelete: 'PoDelete',
        poCSV: 'PoCSV',
        poViewItemLineDetail: 'PoViewItemLineDetail',
        itemLineOverview: 'ItemLineOverview',
        itemLineNew: 'ItemLineNew',
        itemLineEditItemLine: 'ItemLineEditItemLine',
        itemLineDelete: 'ItemLineDelete',
        itemLineLinkDevice: 'ItemLineLinkDevice',
        itemLineCSV: 'ItemLineCSV',
        deviceOverview: 'DeviceOverview',
        deviceNew: 'DeviceNew',
        deviceEdit: 'DeviceEdit',
        deviceDelete: 'DeviceDelete',
        deviceCSV: 'DeviceCSV',
        deviceLinkItemLine: 'DeviceLinkItemLine',
        toBeTreatedMobileDeviceOverview: 'ToBeTreatedMobileDeviceOverview',
        toBeTreatedMobileDeviceNew: 'ToBeTreatedMobileDeviceNew',
        toBeTreatedMobileDeviceEdit: 'ToBeTreatedMobileDeviceEdit',
        toBeTreatedMobileDeviceDelete: 'ToBeTreatedMobileDeviceDelete',
        toBeTreatedMobileDeviceCSV: 'ToBeTreatedMobileDeviceCSV',
        dashBoardX: 'DasBoardX',
        usersOverview: 'Usersverview',
        usersNew: 'UsersNew',
        usersEdit: 'UsersEdit',
        usersEditPasswordOnly: 'UsersEditPasswordOnly',
        usersDelete: 'UsersDelete',
        userAddLoginSite: 'UserAddLoginSite',
        usersCSV: 'UsersCSV',
        userRolesView: 'UserRolesView',
        userRolesCSV: 'UserRolesCSV',
        userRolesGroupOverview: 'UserRolesGroupOverview',
        userRoleGroupNew: 'UserRoleGroupNew',
        userRoleGroupEdit: 'UserRoleGroupEdit',
        userRoleGroupDelete: 'UserRoleGroupDelete',
        userRoleGroupAddRole: 'UserRoleGroupAddRole',
        userRoleGroupCSV: 'UserRoleGroupCSV',
        languageOverview: 'LanguageOverview',
        languageNew: 'LanguageNew',
        languageEdit: 'LanguageEdit',
        languageDelete: 'LanguageDelete',
        languageCSV: 'LanguageCSV',
        translationOverview: 'TranslationOverview',
        translationNew: 'TranslationNew',
        translationEdit: 'TranslationEdit',
        translationDelete: 'TranslationDelete',
        translationToggleMode: 'TranslationToggleMode',
        translationCSV: 'TranslationCSV',
        translationAllLanguages: 'TranslationAllLanguages',
        statesOverview: 'StatesOverview',
        statesNew: 'StatesNew',
        statesEdit: 'StatesEdit',
        statesDelete: 'StatesDelete',
        statesCSV: 'StatesCSV',
        deviceStatesFlowOverview: 'DeviceStatesFlowOverview',
        deviceStatesFlowNew: 'DeviceStatesFlowNew',
        deviceStatesFlowEdit: 'DeviceStatesFlowEdit',
        deviceStatesFlowEditRemoveRoleGroups: 'DeviceStatesFlowEditRemoveRoleGroups',
        deviceStatesFlowEditAddRoleGroups: 'DeviceStatesFlowEditAddRoleGroups',
        deviceStatesFlowDelete: 'DeviceStatesFlowDelete',
        deviceStatesFlowCSV: 'DeviceStatesFlowCSV',
        orderStatesFlowOverview: 'OrderStatesFlowOverview',
        orderStatesFlowNew: 'OrderStatesFlowNew',
        orderStatesFlowEdit: 'OrderStatesFlowEdit',
        orderStatesFlowEditRemoveRoleGroups: 'OrderStatesFlowEditRemoveRoleGroups',
        orderStatesFlowEditAddRoleGroups: 'OrderStatesFlowEditAddRoleGroups',
        orderStatesFlowDelete: 'OrderStatesFlowDelete',
        orderStatesFlowCSV: 'OrderStatesFlowCSV',
        loginSiteOverview: 'LoginSiteOverview',
        loginSiteNew: 'LoginSiteNew',
        loginSiteEdit: 'LoginSiteEdit',
        loginSiteDelete: 'LoginSiteDelete',
        loginSiteCSV: 'LoginSiteCSV',
        loginLicenceNew: 'LoginLicenceNew',
        loginLicenceOverview: 'LoginLicenceOverview',
        loginLicenceEdit: 'LoginLicenceEdit',
        loginLicenceDelete: 'LoginLicenceDelete',
        loginLicenceCSV: 'LoginLicenceCSV',
        repairReasonOverview: 'RepairReasonOverview',
        repairReasonNew: 'RepairReasonSiteNew',
        repairReasonEdit: 'RepairReasonEdit',
        repairReasonDelete: 'RepairReasonDelete',
        repairReasonCSV: 'RepairReasonCSV',
        platformOverview: 'PlatformOverview',
        platformNew: 'PlatformSiteNew',
        platformEdit: 'PlatformEdit',
        platformDelete: 'PlatformDelete',
        platformCSV: 'PlatformCSV',
        flocIdOverview: 'FlocIdOverview',
        flocIdNew: 'FlocIdSiteNew',
        flocIdEdit: 'FlocIdEdit',
        flocIdDelete: 'FlocIdDelete',
        flocIdCSV: 'FlocIdCSV',
        deviceTypeOverview: 'DeviceTypeOverview',
        deviceTypeNew: 'DeviceTypeSiteNew',
        deviceTypeEdit: 'DeviceTypeEdit',
        deviceTypeDelete: 'DeviceTypeDelete',
        deviceTypeCSV: 'DeviceTypeCSV',
        productTypeOverview: 'ProductTypeOverview',
        productTypeNew: 'ProductTypeSiteNew',
        productTypeEdit: 'ProductTypeEdit',
        productTypeDelete: 'ProductTypeDelete',
        productTypeCSV: 'ProductTypeCSV',
        cSVImport: 'CSVImport',
        OrderStateCSV: 'OrderStateCSV',
        OrderStatenew: 'OrderStatenew',
        OrderStateOverview: 'OrderStateOverview',
        OrderStateEdit: 'OrderStateEdit',
        OrderStateDelete: 'OrderStateDelete',
        DeviceStateCSV: 'DeviceStateCSV',
        DeviceStatenew: 'DeviceStatenew',
        DeviceStateOverview: 'DeviceStateOverview',
        DeviceStateEdit: 'DeviceStateEdit',
        DeviceStateDelete: 'DeviceStateDelete',
    });
    angular.module('app').constant('States', {
        //default + authentication
        defaultState: 'mobileDevices.list',
        login: 'login.login',
        accessDenied: 'login.accessdenied',

        //csv import
        csvImport: 'csvimport',

        //settings
        settings: 'settings',

        //dashboard
        dashboard: 'dashboard',

        //device
        mobileDeviceList: 'mobileDevices.list',
        mobileDeviceDetail: 'mobileDevices.detail',
        mobileDeviceEdit: 'mobileDevices.edit',
        mobileDeviceNew: 'mobileDevices.new',
        mobileDeviceNewFromOrderItem: 'mobileDevices.newOrderItem',

        //tobetreateddevice
        toBeTreatedMobileDeviceList: 'toBeTreatedMobileDevices.list',
        toBeTreatedMobileDeviceDetail: 'toBeTreatedMobileDevices.detail',
        toBeTreatedMobileDeviceEdit: 'toBeTreatedMobileDevices.edit',
        toBeTreatedMobileDeviceNew: 'toBeTreatedMobileDevices.new',
        toBeTreatedMobileDeviceNewFromOrderItem: 'toBeTreatedMobileDevices.newOrderItem',

        //language
        languageList: 'languages.list',
        languageNew: 'languages.new',
        languageEdit: 'languages.edit',
        languageDetail: 'languages.detail',

        //translations
        translationList: 'translations.list',
        translationDetail: 'translations.detail',
        translationNew: 'translations.new',
        translationEdit: 'translations.edit',

        //users
        userList: 'users.list',
        userDetail: 'users.detail',
        userNew: 'users.new',
        userEdit: 'users.edit',
        userChangePassword: 'users.changePassword',

        //userrole
        userRoleList: 'userRoles.list',

        //userrolegroup
        userRoleGroupList: 'userRoleGroups.list',
        userRoleGroupNew: 'userRoleGroups.new',
        userRoleGroupEdit: 'userRoleGroups.edit',
        userRoleGroupDetail: 'userRoleGroups.detail',

        //states
        stateList: 'states.list',
        stateNew: 'states.new',
        stateDetail: 'states.detail',
        stateEdit: 'states.edit',

        //stateChanges
        stateChangeList: 'stateChanges.list',
        stateChangeNew: 'stateChanges.new',
        stateChangeDetail: 'stateChanges.detail',
        stateChangeEdit: 'stateChanges.edit',

        //orderStateChanges
        orderStateChangeList: 'orderStateChanges.list',
        orderStateChangeNew: 'orderStateChanges.new',
        orderStateChangeDetail: 'orderStateChanges.detail',
        orderStateChangeEdit: 'orderStateChanges.edit',
        
        //toBeTreatedLwpSettings
        lwpSettingNew: 'lwpSettings.new',
        lwpSettingDetail: 'lwpSettings.detail',
        lwpSettingEdit: 'lwpSettings.edit',

        //toBeTreatedLwpSettings
        toBeTreatedLwpSettingNew: 'toBeTreatedLwpSettings.new',
        toBeTreatedLwpSettingDetail: 'toBeTreatedLwpSettings.detail',
        toBeTreatedLwpSettingEdit: 'toBeTreatedLwpSettings.edit',

        //Repair
        repairReasonList: 'repairReasons.list',
        repairReasonNew: 'repairReasons.new',
        repairReasonDetail: 'repairReasons.detail',
        repairReasonEdit: 'repairReasons.edit',

        //DeviceType
        deviceTypeList: 'deviceTypes.list',
        deviceTypeNew: 'deviceTypes.new',
        deviceTypeDetail: 'deviceTypes.detail',
        deviceTypeEdit: 'deviceTypes.edit',

        //ProductType
        productTypeList: 'productTypes.list',
        productTypeNew: 'productTypes.new',
        productTypeDetail: 'productTypes.detail',
        productTypeEdit: 'productTypes.edit',
        
        //LoginSite
        loginSiteList: 'loginSites.list',
        loginSiteNew: 'loginSites.new',
        loginSiteDetail: 'loginSites.detail',
        loginSiteEdit: 'loginSites.edit',

        //LoginLicence
        loginLicenceList: 'loginLicences.list',
        loginLicenceDetail: 'loginLicences.detail',
        loginLicenceEdit: 'loginLicences.edit',
        loginLicenceNewFromOrderItem: 'loginLicences.newOrderItem',

        //Platform
        platformList: 'platforms.list',
        platformNew: 'platforms.new',
        platformDetail: 'platforms.detail',
        platformEdit: 'platforms.edit',

        //FlocId
        flocIdList: 'flocIds.list',
        flocIdNew: 'flocIds.new',
        flocIdDetail: 'flocIds.detail',
        flocIdEdit: 'flocIds.edit',
        flocIdNewFromLoginLicence: 'flocIds.newLoginLicence',

        //PurchaseOrder
        purchaseOrderList: 'purchaseOrders.list',
        purchaseOrderNew: 'purchaseOrders.new',
        purchaseOrderDetail: 'purchaseOrders.detail',
        purchaseOrderEdit: 'purchaseOrders.edit',

        //OrderItem
        orderItemList: 'orderItems.list',
        orderItemNew: 'orderItems.new',
        orderItemDetail: 'orderItems.detail',
        orderItemEdit: 'orderItems.edit',
    });

})();


