var app = angular.module('app', [
        // Angular modules 
        'ui.router',
        'ngAnimate',
        'ngFileUpload',
        'ngCookies',
        'ui.bootstrap',
        'ui.bootstrap.datepickerPopup',
        'ngSanitize',

        // Custom modules 

        // 3rd Party Modules
        'toaster',
        'pascalprecht.translate',
        'smart-table',
        'lrDragNDrop',
        'n3-line-chart',
        'color.picker'
]);

app.constant("moment", moment);


////////////////////////////////////////////
//          BOOT APP
////////////////////////////////////////////
app.run(['$state', '$rootScope', 'AuthorizationHandler', 'StateService', 'AppConfig', 'Resources', 'States', 'Roles', '$location', '$translate', '$window', 'LoginService', function ($state, $rootScope, authorizationHandler, stateService, config, resources, states, roles, location, $translate, $window, loginService) {
    //Reload oauth token from cookie 
    authorizationHandler.reloadAuthenticationToken();
    //set appconfig
    $rootScope.resources = resources;
    $rootScope.states = states;
    $rootScope.roles = roles;
    if (authorizationHandler.getLanguage() === "NoLanguage") {
        var lang = $window.sessionStorage && $window.sessionStorage.removeItem('PreviousLanguage');
        loginService.logout();
        authorizationHandler.clearAuthenticationToken();
        authorizationHandler.setUser(undefined);
        authorizationHandler.setRoles(undefined);
        authorizationHandler.setLanguage(undefined);
        $state.go(states.login, { target: states.defaultState });
    }

    $rootScope.$on('$stateChangeSuccess', function (e, toState, toParams, fromState, fromParams) {
        if (toState.name !== states.login) {
            stateService.state = toState;
            stateService.stateParams = toParams;
            stateService.location = location.$$path;
        }
    });

    //State change => save on rootscope + check if state has roles and user is authorized
    $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {

        //Get user from storage (if exists)
        var user = authorizationHandler.getUser();
        var roles = authorizationHandler.getRoles();
        var language = authorizationHandler.getLanguage();

        if (language && language.shortCode) {
            $translate.use(language.shortCode);
        }

        if (!angular.isUndefined(toState.roles)) {
            if (angular.isUndefined(user) || user === null) {
                e.preventDefault();
                $state.go(states.login, { target: toState.name });
            } else if (angular.isUndefined(roles) || roles === null) {
                e.preventDefault();
                $state.go(states.login, { target: toState.name });
            } else {
                var enabled = false;
                for (var index = 0; index < toState.roles.length; ++index) {
                    var roleState = toState.roles[index];

                    for (var i = 0; i < roles.length; i++) {
                        var userRole = roles[i];
                        if (angular.isDefined(roleState) && angular.isDefined(userRole)) {
                            //if (userRole.toUpperCase() === $rootScope.roles.admin.toUpperCase()) {
                            //    enabled = true
                            //    break;
                            //}
                            if (roleState.toUpperCase() === userRole.toUpperCase()) {
                                enabled = true;
                                break;
                            }
                        }
                    }

                    if (enabled) break;
                }
                if (!enabled) {
                    //Role not allowed
                    e.preventDefault();
                    $state.go(states.accessDenied);
                }
            }

        }
    });
}]);

app.config(['$stateProvider', '$urlRouterProvider', '$httpProvider', '$translateProvider', 'States', 'Roles', '$provide', function ($stateProvider, $urlRouterProvider, $httpProvider, $translateProvider, states, roles, $provide) {

    //Start IE bug fix
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    //End IE bug fix

    //Color picker settings
    $provide.decorator('ColorPickerOptions', function ($delegate) {
        var options = angular.copy($delegate);
        options.format = 'hex';
        return options;
    });


    //Angular-Translate 
    $translateProvider
    .registerAvailableLanguageKeys(['en', 'nl'], {
        'en_US': 'en',
        'en_UK': 'en',
        'nl_NL': 'nl',
        'nl_BE': 'nl',
    })
    $translateProvider.useLoader('TranlationLoader');
    $translateProvider.determinePreferredLanguage();
    $translateProvider.useMissingTranslationHandler('TranslationMissingHandler');



    //URL Reroute
    $urlRouterProvider
    // The `when` method says if the url is ever the 1st param, then redirect to the 2nd param
    //.when('/c?id', '/contacts/:id')
    //.when('/user/:id', '/contacts/:id')

    // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
    .otherwise('/devices');


    //State Config
    $stateProvider
    ////////////////////////////////////////////
    //          LOGIN
    ////////////////////////////////////////////
    .state("login", {
        url: "/account",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.login, {
        url: "/login?:target",
        controller: 'LoginController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('login/login.html')
    })
    .state(states.accessDenied, {
        url: "/accessdenied",
        templateUrl: getTemplate('login/accessdenied.html')
    })

    //////////////////////////////////////////
    //          DEVICES
    //////////////////////////////////////////
    .state("mobileDevices", {
        url: "/devices",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.mobileDeviceList, {
        url: "",
        controller: 'MobileDeviceOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.deviceOverview],
        templateUrl: getTemplate('device/deviceOverview.html')
    })
    .state(states.mobileDeviceNew, {
        url: "/new",
        controller: 'MobileDeviceCreateController',
        controllerAs: 'ctrl',
        roles: [roles.deviceNew],
        templateUrl: getTemplate('device/deviceCreate.html')
    })
    .state(states.mobileDeviceNewFromOrderItem, {
        url: "/new/orderitem/:orderItemId",
        controller: 'MobileDeviceCreateController',
        controllerAs: 'ctrl',
        roles: [roles.deviceNew],
        templateUrl: getTemplate('device/deviceCreate.html')
    })
    .state(states.mobileDeviceDetail, {
        url: "/:id",
        controller: 'MobileDeviceDetailController',
        controllerAs: 'ctrl',
        roles: [roles.deviceOverview],
        templateUrl: getTemplate('device/deviceDetail.html')
    })
    .state(states.mobileDeviceEdit, {
        url: "/:id/edit",
        controller: 'MobileDeviceEditController',
        controllerAs: 'ctrl',
        roles: [roles.deviceEdit],
        templateUrl: getTemplate('device/deviceCreate.html')
    })

    //////////////////////////////////////////
    //          TOBETREATEDDEVICES
    //////////////////////////////////////////
    .state("toBeTreatedMobileDevices", {
        url: "/toBeTreatedDevices",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.toBeTreatedMobileDeviceList, {
        url: "",
        controller: 'ToBeTreatedMobileDeviceOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.toBeTreatedMobileDeviceOverview],
        templateUrl: getTemplate('toBeTreatedDevice/toBeTreatedDeviceOverview.html')
    })
    .state(states.toBeTreatedMobileDeviceNew, {
        url: "/new",
        controller: 'ToBeTreatedMobileDeviceCreateController',
        controllerAs: 'ctrl',
        roles: [roles.toBeTreatedMobileDeviceNew],
        templateUrl: getTemplate('toBeTreatedDevice/toBeTreatedDeviceCreate.html')
    })
    .state(states.toBeTreatedMobileDeviceNewFromOrderItem, {
        url: "/new/orderitem/:orderItemId",
        controller: 'ToBeTreatedMobileDeviceCreateController',
        controllerAs: 'ctrl',
        roles: [roles.toBeTreatedMobileDeviceNew],
        templateUrl: getTemplate('toBeTreatedDevice/toBeTreatedDeviceCreate.html')
    })
    .state(states.toBeTreatedMobileDeviceDetail, {
        url: "/:id",
        controller: 'ToBeTreatedMobileDeviceDetailController',
        controllerAs: 'ctrl',
        roles: [roles.toBeTreatedMobileDeviceOverview],
        templateUrl: getTemplate('toBeTreatedDevice/toBeTreatedDeviceDetail.html')
    })
    .state(states.toBeTreatedMobileDeviceEdit, {
        url: "/:id/edit",
        controller: 'ToBeTreatedMobileDeviceEditController',
        controllerAs: 'ctrl',
        roles: [roles.toBeTreatedMobileDeviceEdit],
        templateUrl: getTemplate('toBeTreatedDevice/toBeTreatedDeviceCreate.html')
    })


    ////////////////////////////////////////////
    //          LANGUAGES
    ////////////////////////////////////////////
    .state("languages", {
        url: "/languages",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.languageList, {
        url: "",
        controller: 'LanguageOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.languageOverview],
        templateUrl: getTemplate('language/languageOverview.html')
    })
    .state(states.languageNew, {
        url: "/new",
        controller: 'LanguageCreateController',
        controllerAs: 'ctrl',
        roles: [roles.languageNew],
        templateUrl: getTemplate('language/languageCreate.html')
    })
    .state(states.languageDetail, {
        url: "/:id",
        controller: 'LanguageDetailController',
        controllerAs: 'ctrl',
        roles: [roles.languageOverview],
        templateUrl: getTemplate('language/languageDetail.html')
    })
    .state(states.languageEdit, {
        url: "/:id/edit",
        controller: 'LanguageEditController',
        controllerAs: 'ctrl',
        roles: [roles.languageEdit],
        templateUrl: getTemplate('language/languageCreate.html')
    })

    ////////////////////////////////////////////
    //          TRANSLATIONS
    ////////////////////////////////////////////
    .state("translations", {
        url: "/translations",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.translationList, {
        url: "",
        controller: 'TranslationOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.translationOverview],
        templateUrl: getTemplate('translation/translationOverview.html')
    })
    .state(states.translationNew, {
        url: "/new",
        controller: 'TranslationCreateController',
        controllerAs: 'ctrl',
        roles: [roles.translationNew],
        templateUrl: getTemplate('translation/translationCreate.html')
    })
    .state(states.translationDetail, {
        url: "/:id",
        controller: 'TranslationDetailController',
        controllerAs: 'ctrl',
        roles: [roles.translationOverview],
        templateUrl: getTemplate('translation/translationDetail.html')
    })
    .state(states.translationEdit, {
        url: "/:id/edit",
        controller: 'TranslationEditController',
        controllerAs: 'ctrl',
        roles: [roles.translationEdit],
        templateUrl: getTemplate('translation/translationCreate.html')
    })

    ////////////////////////////////////////////
    //          USERS
    ////////////////////////////////////////////
    .state("users", {
        url: "/users",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.userList, {
        url: "",
        controller: 'UserOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.usersOverview],
        templateUrl: getTemplate('user/userOverview.html')
    })
    .state(states.userNew, {
        url: "/new",
        controller: 'UserCreateController',
        controllerAs: 'ctrl',
        roles: [roles.languageNew],
        templateUrl: getTemplate('user/userCreate.html')
    })
    .state(states.userDetail, {
        url: "/:id",
        controller: 'UserDetailController',
        controllerAs: 'ctrl',
        roles: [roles.usersOverview],
        templateUrl: getTemplate('user/userDetail.html')
    })
    .state(states.userEdit, {
        url: "/:id/edit",
        controller: 'UserEditController',
        controllerAs: 'ctrl',
        roles: [roles.usersEdit],
        templateUrl: getTemplate('user/userCreate.html')
    })
    .state(states.userChangePassword, {
        url: "/:id/changepassword",
        controller: 'UserChangePasswordController',
        controllerAs: 'ctrl',
        roles: [roles.usersEditPasswordOnly, roles.usersEdit],
        templateUrl: getTemplate('user/userChangePassword.html')
    })


    ////////////////////////////////////////////
    //          USERROLES
    ////////////////////////////////////////////
    .state("userRoles", {
        url: "/userRoles",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.userRoleList, {
        url: "",
        controller: 'UserRoleOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.userRolesView],
        templateUrl: getTemplate('userRole/userRoleOverview.html')
    })

    ////////////////////////////////////////////
    //          USERROLEGROUPS
    ////////////////////////////////////////////
    .state("userRoleGroups", {
        url: "/userrolegroups",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.userRoleGroupList, {
        url: "",
        controller: 'UserRoleGroupOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.userRolesGroupOverview],
        templateUrl: getTemplate('userRoleGroup/userRoleGroupOverview.html')
    })
    .state(states.userRoleGroupEdit, {
        url: "/:id/edit",
        controller: 'UserRoleGroupEditController',
        controllerAs: 'ctrl',
        roles: [roles.userRoleGroupEdit],
        templateUrl: getTemplate('userRoleGroup/userRoleGroupCreate.html')
    })
    .state(states.userRoleGroupNew, {
        url: "/new",
        controller: 'UserRoleGroupCreateController',
        controllerAs: 'ctrl',
        roles: [roles.userRoleGroupNew],
        templateUrl: getTemplate('userRoleGroup/userRoleGroupCreate.html')
    })
    .state(states.userRoleGroupDetail, {
        url: "/:id",
        controller: 'UserRoleGroupDetailController',
        controllerAs: 'ctrl',
        roles: [roles.userRolesGroupOverview],
        templateUrl: getTemplate('userRoleGroup/userRoleGroupDetail.html')
    })


     ////////////////////////////////////////////
    //          STATE
    ////////////////////////////////////////////
    .state("states", {
        url: "/states",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.stateList, {
        url: "",
        controller: 'StateOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.statesOverview],
        templateUrl: getTemplate('state/stateOverview.html')
    })
    .state(states.stateNew, {
        url: "/new",
        controller: 'StateCreateController',
        controllerAs: 'ctrl',
        roles: [roles.statesNew],
        templateUrl: getTemplate('state/stateCreate.html')
    })
    .state(states.stateDetail, {
        url: "/:id",
        controller: 'StateDetailController',
        controllerAs: 'ctrl',
        roles: [roles.statesOverview],
        templateUrl: getTemplate('state/stateDetail.html')
    })
    .state(states.stateEdit, {
        url: "/:id/edit",
        controller: 'StateEditController',
        controllerAs: 'ctrl',
        roles: [roles.statesEdit],
        templateUrl: getTemplate('state/stateCreate.html')
    })

    ////////////////////////////////////////////
    //          STATECHANGE
    ////////////////////////////////////////////
    .state("stateChanges", {
        url: "/stateChanges",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.stateChangeList, {
        url: "",
        controller: 'StateChangeOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.deviceStatesFlowOverview],
        templateUrl: getTemplate('stateChange/stateChangeOverview.html')
    })
    .state(states.stateChangeNew, {
        url: "/new",
        controller: 'StateChangeCreateController',
        controllerAs: 'ctrl',
        roles: [roles.deviceStatesFlowNew],
        templateUrl: getTemplate('stateChange/stateChangeCreate.html')
    })
    .state(states.stateChangeDetail, {
        url: "/:id",
        controller: 'StateChangeDetailController',
        controllerAs: 'ctrl',
        roles: [roles.deviceStatesFlowOverview],
        templateUrl: getTemplate('stateChange/stateChangeDetail.html')
    })
    .state(states.stateChangeEdit, {
        url: "/:id/edit",
        controller: 'StateChangeEditController',
        controllerAs: 'ctrl',
        roles: [roles.deviceStatesFlowEdit],
        templateUrl: getTemplate('stateChange/stateChangeCreate.html')
    })

    ////////////////////////////////////////////
    //          ORDERSTATECHANGE
    ////////////////////////////////////////////
    .state("orderStateChanges", {
        url: "/orderStateChanges",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.orderStateChangeList, {
        url: "",
        controller: 'OrderStateChangeOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.orderStatesFlowOverview],
        templateUrl: getTemplate('orderStateChange/orderStateChangeOverview.html')
    })
    .state(states.orderStateChangeNew, {
        url: "/new",
        controller: 'OrderStateChangeCreateController',
        controllerAs: 'ctrl',
        roles: [roles.orderStatesFlowNew],
        templateUrl: getTemplate('orderStateChange/orderStateChangeCreate.html')
    })
    .state(states.orderStateChangeDetail, {
        url: "/:id",
        controller: 'OrderStateChangeDetailController',
        controllerAs: 'ctrl',
        roles: [roles.orderStatesFlowOverview],
        templateUrl: getTemplate('orderStateChange/orderStateChangeDetail.html')
    })
    .state(states.orderStateChangeEdit, {
        url: "/:id/edit",
        controller: 'OrderStateChangeEditController',
        controllerAs: 'ctrl',
        roles: [roles.orderStatesFlowEdit],
        templateUrl: getTemplate('orderStateChange/orderStateChangeCreate.html')
    })

    ////////////////////////////////////////////
    //          LwpSetting
    ////////////////////////////////////////////
    .state("lwpSettings", {
        url: "/lwpSettings",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.lwpSettingNew, {
        url: "/new",
        controller: 'LwpSettingCreateController',
        controllerAs: 'ctrl',
        roles: [roles.deviceNew],
        templateUrl: getTemplate('lwpSetting/lwpSettingCreate.html')
    })
    .state(states.lwpSettingDetail, {
        url: "/:id",
        controller: 'LwpSettingDetailController',
        controllerAs: 'ctrl',
        roles: [roles.deviceOverview],
        templateUrl: getTemplate('lwpSetting/lwpSettingDetail.html')
    })
    .state(states.lwpSettingEdit, {
        url: "/:id/edit",
        controller: 'LwpSettingEditController',
        controllerAs: 'ctrl',
        roles: [roles.deviceEdit],
        templateUrl: getTemplate('lwpSetting/lwpSettingCreate.html')
    })

    ////////////////////////////////////////////
    //          ToBeTreatedLwpSetting
    ////////////////////////////////////////////
    .state("toBeTreatedLwpSettings", {
        url: "/toBeTreatedLwpSettings",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.toBeTreatedLwpSettingNew, {
        url: "/new",
        controller: 'ToBeTreatedLwpSettingCreateController',
        controllerAs: 'ctrl',
        roles: [roles.deviceNew],
        templateUrl: getTemplate('toBeTreatedLwpSetting/toBeTreatedLwpSettingCreate.html')
    })
    .state(states.toBeTreatedLwpSettingDetail, {
        url: "/:id",
        controller: 'ToBeTreatedLwpSettingDetailController',
        controllerAs: 'ctrl',
        roles: [roles.deviceOverview],
        templateUrl: getTemplate('toBeTreatedLwpSetting/toBeTreatedLwpSettingDetail.html')
    })
    .state(states.toBeTreatedLwpSettingEdit, {
        url: "/:id/edit",
        controller: 'ToBeTreatedLwpSettingEditController',
        controllerAs: 'ctrl',
        roles: [roles.deviceEdit],
        templateUrl: getTemplate('toBeTreatedLwpSetting/toBeTreatedLwpSettingCreate.html')
    })

    ////////////////////////////////////////////
    //          RepairReason
    ////////////////////////////////////////////
    .state("repairReasons", {
        url: "/repairReasons",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.repairReasonList, {
        url: "",
        controller: 'RepairReasonOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.repairReasonOverview],
        templateUrl: getTemplate('repairReason/repairReasonOverview.html')
    })
    .state(states.repairReasonNew, {
        url: "/new",
        controller: 'RepairReasonCreateController',
        controllerAs: 'ctrl',
        roles: [roles.repairReasonNew],
        templateUrl: getTemplate('repairReason/repairReasonCreate.html')
    })
    .state(states.repairReasonDetail, {
        url: "/:id",
        controller: 'RepairReasonDetailController',
        controllerAs: 'ctrl',
        roles: [roles.repairReasonOverview],
        templateUrl: getTemplate('repairReason/repairReasonDetail.html')
    })
    .state(states.repairReasonEdit, {
        url: "/:id/edit",
        controller: 'RepairReasonEditController',
        controllerAs: 'ctrl',
        roles: [roles.repairReasonEdit],
        templateUrl: getTemplate('repairReason/repairReasonCreate.html')
    })

    ////////////////////////////////////////////
    //          DEVICETYPES
    ////////////////////////////////////////////
    .state("deviceTypes", {
        url: "/deviceTypes",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.deviceTypeList, {
        url: "",
        controller: 'DeviceTypeOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.deviceTypeOverview],
        templateUrl: getTemplate('deviceType/deviceTypeOverview.html')
    })
    .state(states.deviceTypeNew, {
        url: "/new",
        controller: 'DeviceTypeCreateController',
        controllerAs: 'ctrl',
        roles: [roles.deviceTypeNew],
        templateUrl: getTemplate('deviceType/deviceTypeCreate.html')
    })
    .state(states.deviceTypeDetail, {
        url: "/:id",
        controller: 'DeviceTypeDetailController',
        controllerAs: 'ctrl',
        roles: [roles.deviceTypeOverview],
        templateUrl: getTemplate('deviceType/deviceTypeDetail.html')
    })
    .state(states.deviceTypeEdit, {
        url: "/:id/edit",
        controller: 'DeviceTypeEditController',
        controllerAs: 'ctrl',
        roles: [roles.deviceTypeEdit],
        templateUrl: getTemplate('deviceType/deviceTypeCreate.html')
    })

    ////////////////////////////////////////////
    //          PRODUCTTYPES
    ////////////////////////////////////////////
    .state("productTypes", {
        url: "/productTypes",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.productTypeList, {
        url: "",
        controller: 'ProductTypeOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.productTypeOverview],
        templateUrl: getTemplate('productType/productTypeOverview.html')
    })
    .state(states.productTypeNew, {
        url: "/new",
        controller: 'ProductTypeCreateController',
        controllerAs: 'ctrl',
        roles: [roles.productTypeNew],
        templateUrl: getTemplate('productType/productTypeCreate.html')
    })
    .state(states.productTypeDetail, {
        url: "/:id",
        controller: 'ProductTypeDetailController',
        controllerAs: 'ctrl',
        roles: [roles.productTypeOverview],
        templateUrl: getTemplate('productType/productTypeDetail.html')
    })
    .state(states.productTypeEdit, {
        url: "/:id/edit",
        controller: 'ProductTypeEditController',
        controllerAs: 'ctrl',
        roles: [roles.productTypeEdit],
        templateUrl: getTemplate('productType/productTypeCreate.html')
    })


    ////////////////////////////////////////////
    //          LoginSite
    ////////////////////////////////////////////
    .state("loginSites", {
        url: "/loginSites",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.loginSiteList, {
        url: "",
        controller: 'LoginSiteOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.loginSiteOverview],
        templateUrl: getTemplate('loginSite/loginSiteOverview.html')
    })
    .state(states.loginSiteNew, {
        url: "/new",
        controller: 'LoginSiteCreateController',
        controllerAs: 'ctrl',
        roles: [roles.loginSiteNew],
        templateUrl: getTemplate('loginSite/loginSiteCreate.html')
    })
    .state(states.loginSiteDetail, {
        url: "/:id",
        controller: 'LoginSiteDetailController',
        controllerAs: 'ctrl',
        roles: [roles.loginSiteOverview],
        templateUrl: getTemplate('loginSite/loginSiteDetail.html')
    })
    .state(states.loginSiteEdit, {
        url: "/:id/edit",
        controller: 'LoginSiteEditController',
        controllerAs: 'ctrl',
        roles: [roles.loginSiteEdit],
        templateUrl: getTemplate('loginSite/loginSiteCreate.html')
    })

    ////////////////////////////////////////////
    //          LoginLicence
    ////////////////////////////////////////////
    .state("loginLicences", {
        url: "/loginLicences",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.loginLicenceList, {
        url: "",
        controller: 'LoginLicenceOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.loginLicenceOverview],
        templateUrl: getTemplate('loginLicence/loginLicenceOverview.html')
    })
    .state(states.loginLicenceNewFromOrderItem, {
        url: "/new/orderitem/:orderItemId",
        controller: 'LoginLicenceCreateController',
        controllerAs: 'ctrl',
        roles: [roles.loginLicenceNew],
        templateUrl: getTemplate('loginLicence/loginLicenceCreate.html')
    })
    .state(states.loginLicenceDetail, {
        url: "/:id",
        controller: 'LoginLicenceDetailController',
        controllerAs: 'ctrl',
        roles: [roles.loginLicenceOverview],
        templateUrl: getTemplate('loginLicence/loginLicenceDetail.html')
    })
    .state(states.loginLicenceEdit, {
        url: "/:id/edit",
        controller: 'LoginLicenceEditController',
        controllerAs: 'ctrl',
        roles: [roles.loginLicenceEdit],
        templateUrl: getTemplate('loginLicence/loginLicenceCreate.html')
    })

    ////////////////////////////////////////////
    //          MISC
    ////////////////////////////////////////////
    .state(states.dashboard, {
        url: "/dashboard",
        controller: 'DashboardController',
        controllerAs: 'ctrl',
        roles: [roles.dashBoardX],
        templateUrl: getTemplate('dashboard/dashboard.html')
    })

    .state(states.csvImport, {
        url: "/csv",
        controller: 'CsvImportController',
        controllerAs: 'ctrl',
        roles: [roles.cSVImport],
        templateUrl: getTemplate('import/csvImport.html')
    })

    .state(states.settings, {
        url: "/settings",
        controller: 'SettingsController',
        controllerAs: 'ctrl',
        roles: [roles.settings],
        templateUrl: getTemplate('settings/settings.html')
    })

    ////////////////////////////////////////////
    //          Platform
    ////////////////////////////////////////////
    .state("platforms", {
        url: "/platforms",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.platformList, {
        url: "",
        controller: 'PlatformOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.platformOverview],
        templateUrl: getTemplate('platform/platformOverview.html')
    })
    .state(states.platformNew, {
        url: "/new",
        controller: 'PlatformCreateController',
        controllerAs: 'ctrl',
        roles: [roles.platformNew],
        templateUrl: getTemplate('platform/platformCreate.html')
    })
    .state(states.platformDetail, {
        url: "/:id",
        controller: 'PlatformDetailController',
        controllerAs: 'ctrl',
        roles: [roles.platformOverview],
        templateUrl: getTemplate('platform/platformDetail.html')
    })
    .state(states.platformEdit, {
        url: "/:id/edit",
        controller: 'PlatformEditController',
        controllerAs: 'ctrl',
        roles: [roles.platformEdit],
        templateUrl: getTemplate('platform/platformCreate.html')
    })

    ////////////////////////////////////////////
    //          FlocId
    ////////////////////////////////////////////
    .state("flocIds", {
        url: "/flocIds",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.flocIdList, {
        url: "",
        controller: 'FlocIdOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.flocIdOverview],
        templateUrl: getTemplate('flocId/flocIdOverview.html')
    })
    .state(states.flocIdNewFromLoginLicence, {
        url: "/new/loginlicence/:loginLicenceId",
        controller: 'FlocIdCreateController',
        controllerAs: 'ctrl',
        roles: [roles.flocIdNew],
        templateUrl: getTemplate('flocId/flocIdCreate.html')
    })
    .state(states.flocIdDetail, {
        url: "/:id",
        controller: 'FlocIdDetailController',
        controllerAs: 'ctrl',
        roles: [roles.flocIdOverview],
        templateUrl: getTemplate('flocId/flocIdDetail.html')
    })
    .state(states.flocIdEdit, {
        url: "/:id/edit",
        controller: 'FlocIdEditController',
        controllerAs: 'ctrl',
        roles: [roles.flocIdEdit],
        templateUrl: getTemplate('flocId/flocIdCreate.html')
    })

    ////////////////////////////////////////////
    //          PurchaseOrder
    ////////////////////////////////////////////
    .state("purchaseOrders", {
        url: "/purchaseOrders",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.purchaseOrderList, {
        url: "",
        controller: 'PurchaseOrderOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.poOverview],
        templateUrl: getTemplate('purchaseOrder/purchaseOrderOverview.html')
    })
    .state(states.purchaseOrderNew, {
        url: "/new",
        controller: 'PurchaseOrderCreateController',
        controllerAs: 'ctrl',
        roles: [roles.poNew],
        templateUrl: getTemplate('purchaseOrder/purchaseOrderCreate.html')
    })
    .state(states.purchaseOrderDetail, {
        url: "/:id",
        controller: 'PurchaseOrderDetailController',
        controllerAs: 'ctrl',
        roles: [roles.poOverview],
        templateUrl: getTemplate('purchaseOrder/purchaseOrderDetail.html')
    })
    .state(states.purchaseOrderEdit, {
        url: "/:id/edit",
        controller: 'PurchaseOrderEditController',
        controllerAs: 'ctrl',
        roles: [roles.poEdit],
        templateUrl: getTemplate('purchaseOrder/purchaseOrderCreate.html')
    })

    ////////////////////////////////////////////
    //          OrderItem
    ////////////////////////////////////////////
    .state("orderItems", {
        url: "/orderItems",
        abstract: true,
        template: '<ui-view/>'
    })
    .state(states.orderItemList, {
        url: "",
        controller: 'OrderItemOverviewController',
        controllerAs: 'ctrl',
        roles: [roles.itemLineOverview],
        templateUrl: getTemplate('orderItem/orderItemOverview.html')
    })
    .state(states.orderItemDetail, {
        url: "/:id",
        controller: 'OrderItemDetailController',
        controllerAs: 'ctrl',
        roles: [roles.itemLineOverview],
        templateUrl: getTemplate('orderItem/orderItemDetail.html')
    })
    .state(states.orderItemEdit, {
        url: "/:id/edit",
        controller: 'OrderItemEditController',
        controllerAs: 'ctrl',
        roles: [roles.itemLineEditItemLine],
        templateUrl: getTemplate('orderItem/orderItemCreate.html')
    })
}]);

function getTemplate(route) {
    return '/app_content/' + route;
};