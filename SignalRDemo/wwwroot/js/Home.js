function HomeViewModel(connection) {
    var self = this;

    self.connection = connection;

    self.accountsMapping = {
        update: (options) => {
            const account = options.data;
            account.balance = ko.observable(account.balance);
            account.loading = ko.observable(false);
            return account;
        }
    };

    self.listOfAccounts = ko.observableArray();

    self.getBalance = function (data) {
        self.connection.invoke("GetBalance", data.accountId);
    };

    self.getAccounts = function () {
        $.getJSON("getAccounts" + window.location.search, function (data) {
            ko.mapping.fromJS(data.accounts, self.accountsMapping, self.listOfAccounts)
        });
    };
}

function buildConnection() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .configureLogging(signalR.LogLevel.Information)
        .withAutomaticReconnect()
        .build();

    connection.on("BalanceReceived", (amount, accountId) => {
        console.log(`BalanceReceived : amount -> ${amount} accountId -> ${accountId}`);

        ko.utils.arrayMap(viewModel.listOfAccounts(), (item) => {
            if (item.accountId == accountId) {
                item.balance(amount);
                item.loading(false);
            }
        })
    });

    connection.on("StartedLoading", (accountId) => {
        console.log(`StartedLoading: accountId -> ${accountId}`);

        ko.utils.arrayMap(viewModel.listOfAccounts(), (item) => {
            if (item.accountId == accountId) {
                item.loading(true);
            }
        })
    });

    connection.start();

    return connection;
}

const connection = buildConnection();
const viewModel = new HomeViewModel(connection);
ko.applyBindings(viewModel);

viewModel.getAccounts();