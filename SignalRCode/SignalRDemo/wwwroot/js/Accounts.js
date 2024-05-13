function AccountsViewModel(connection) {
    const self = this;

    self.connection = connection;
    self.selectedCard = ko.observable();
    self.listOfAccounts = ko.observableArray();
    self.totalBalance = ko.computed(() => {
        return self.listOfAccounts().reduce((accumulator, account) => accumulator + account.balance(), 0);
    });

    self.accountsMapping = {
        update: (options) => {
            const account = options.data;
            account.balance = ko.observable(account.balance);
            account.loading = ko.observable(false);
            account.onSelectClass = ko.computed(() => {
                if (self.selectedCard() && self.selectedCard().accountId == account.accountId) {
                    return "active"
                }
                else return "none";
            });

            return account;
        }
    };

    self.makePayment = function (data) {
        let balance = data.balance() - 20;

        data.balance(balance);
        self.connection.invoke("PaymentMade", data.accountId, balance);
    };

    self.getBalance = function (data) {
        data.loading(true);
        self.connection.invoke("GetBalance", data.accountId);
    };

    self.getAccounts = function () {
        $.getJSON(`getAccounts${window.location.search}`, function (data) {
            ko.mapping.fromJS(data.accounts, self.accountsMapping, self.listOfAccounts)
        });
    };

    self.selectCard = function (item) {
        self.selectedCard(item);
    }
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
const viewModel = new AccountsViewModel(connection);
ko.applyBindings(viewModel);

viewModel.getAccounts();