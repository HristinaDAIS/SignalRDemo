function AccountsViewModel(connection) {
    const self = this;

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
        data.loading(true);
        self.connection.invoke("GetBalance", data.accountId);
    };

    self.getAccounts = function () {
        $.getJSON(`getAccounts${window.location.search}`, function (data) {
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

    connection.start();

    return connection;
}

function ClickCommand(obj, e) {
    element = e.delegateTarget;
    siblings = e.delegateTarget.parentElement.children;
    for (var i = 0; i < siblings.length; i++) {
        siblings[i].classList.remove('active');
    }
    element.classList.add('active');
}

const connection = buildConnection();
const viewModel = new AccountsViewModel(connection);
ko.applyBindings(viewModel);

viewModel.getAccounts();