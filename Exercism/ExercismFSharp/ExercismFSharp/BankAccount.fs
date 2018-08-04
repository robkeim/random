module BankAccount

type BankAccount = { balance : float; isOpen : bool }

let mkBankAccount() = { balance = 0.0; isOpen = false }

let openAccount account = { account with isOpen = true }

let closeAccount account = { account with isOpen = false }

let getBalance account =
    if account.isOpen then
        Some account.balance
    else
        None

let updateBalance balance account = { account with balance = account.balance + balance }