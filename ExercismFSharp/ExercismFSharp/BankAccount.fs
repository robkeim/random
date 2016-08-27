module BankAccount

type BankAccount = { balance : float; isOpen : bool }

let mkBankAccount() = { balance = 0.0; isOpen = false }

let openAccount account = { account with isOpen = true }

let closeAccount account = { account with isOpen = false }

let getBalance account =
    match account.isOpen with
    | true -> Some account.balance
    | false -> None

let updateBalance balance account = { account with balance = account.balance + balance }