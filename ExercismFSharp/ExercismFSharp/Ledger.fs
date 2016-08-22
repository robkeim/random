module Ledger

open System
open System.Globalization

type Entry =
    {
        date: DateTime;
        description: string;
        change: int
    }

let mkEntry date description change =
    {
        date = DateTime.Parse(date, CultureInfo.InvariantCulture);
        description = description;
        change = change
    }

type private Currency =
    | EUR
    | USD

type private Locale =
    | NL
    | US

let formatLedger currencyString localeString entries =
    let currency =
        match currencyString with
        | "EUR" -> EUR
        | "USD" -> USD
        | _ -> raise (ArgumentException "Unknown currency")

    let locale =
        match localeString with
        | "nl-NL" -> NL
        | "en-US" -> US
        | _ -> raise (ArgumentException "Unknown locale")

    let header =
        match locale with
        | NL -> "Datum      | Omschrijving              | Verandering  "
        | US -> "Date       | Description               | Change       "
    
    entries
    |> List.sortBy (fun x -> x.date, x.description, x.change)
    |> List.map (fun x ->
        // Date
        let date =
            match locale with
            | NL -> x.date.ToString("dd-MM-yyyy")
            | US -> x.date.ToString("MM\/dd\/yyyy")

        // Description
        let length = x.description.Length
        let description =
            match length with
            |  _ when length <= 25 -> x.description.PadRight(25)
            | _ -> x.description.[0..21] + "..."

        // Change
        let change = float x.change / 100.0

        let formattedNum =
            match locale with
            | NL -> change.ToString("#,#0.00", new CultureInfo("nl-NL"))
            | US -> change.ToString("#,#0.00", new CultureInfo("en-US")).TrimStart('-')

        let change =
            match locale, currency with
            | NL, EUR when change < 0.0 -> sprintf "€ %s" formattedNum
            | NL, USD when change < 0.0 -> sprintf "$ %s" formattedNum
            | US, EUR when change < 0.0 -> sprintf "(€%s)" formattedNum
            | US, USD when change < 0.0 -> sprintf "($%s)" formattedNum
            | NL, EUR -> sprintf "€ %s " formattedNum
            | NL, USD -> sprintf "$ %s " formattedNum
            | US, EUR -> sprintf "€%s " formattedNum
            | US, USD -> sprintf "$%s " formattedNum

        sprintf "\n%s | %s | %s" date description (change.PadLeft(13))
        )
        |> List.fold (fun acc elem -> sprintf "%s%s" acc elem) header
