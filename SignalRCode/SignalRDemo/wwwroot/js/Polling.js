function PollingViewModel() {
    const self = this;

    self.accountsMapping = {
        update: (options) => {
            const account = options.data;
            account.balance = ko.observable(account.balance);
            account.loading = ko.observable(false);
            return account;
        }
    };

    self.listOfAccounts = ko.observableArray();

    self.getBalance = async function (data) {
        data.loading(true);
        const timeoutDate = new Date(new Date().getTime() + 100000);

        await self.pollBalance(timeoutDate, data.accountId);
    };

    self.pollBalance = async function (timeout, accountId) {
        setTimeout(async () => {
            $.getJSON(`getBalance?accountId=${accountId}`, async function (result) {
                if (result.isBalanceAvailable) {
                    ko.utils.arrayMap(self.listOfAccounts(), (item) => {
                        if (item.accountId == accountId) {
                            item.balance(result.balance);
                            item.loading(false);
                        }
                    });
                } else {
                    if (new Date() <= timeout) {
                        await self.pollBalance(timeout, accountId);
                    } else {
                        console.log("Request timed out without getting the balance");
                    }
                }
            });
        }, 1000);
    };

    self.getAccounts = function () {
        $.getJSON(`getAccounts${window.location.search}`, function (data) {
            ko.mapping.fromJS(data.accounts, self.accountsMapping, self.listOfAccounts)
        });
    };
}

const viewModel = new PollingViewModel();
ko.applyBindings(viewModel);

viewModel.getAccounts();