function () {
    var getConfig = function () {
        var config = {
            baseUrl: karate.properties['basePath'],
            login: karate.properties['login'],
            password: karate.properties['password']
        };
        if (!config.baseUrl) {
            config = karate.read('classpath:environment-config.json');
        }
        // karate.configure('ssl', true);
        return config;
    };

    var signIn = function () {
        var config = getConfig();
        var authResult = karate.callSingle('classpath:src/parts/login.feature', config);
        karate.configure('headers', {
            'Authorization': authResult.responseHeaders.Authorization[0]
        });
    }

    var deleteTasks = function () {
        var config = getConfig();
        var authResult = karate.call('classpath:src/parts/delete-tasks.feature', config);
    }

    return {
        config: getConfig(),
        signIn: signIn,
        deleteTasks: deleteTasks
    };
}