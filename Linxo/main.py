import re
from datetime import date
from pathlib import Path


def previous_year_month(today: date) -> tuple[int, int]:
    year, month = today.year, today.month - 1

    if month == 0:
        month, year = 12, year - 1

    return year, month


def main() -> None:
    src = Path("~/Downloads/operations.csv").expanduser()
    year, month = previous_year_month(date.today())
    dst = src.with_name(f"Linxo - {year}-{month:02d}.csv")

    re_date = re.compile(r"(\d{2})/(\d{2})/(\d{4})")
    re_decimal = re.compile(r"(\d+),(\d+)")

    with src.open(encoding="utf-16", newline="") as fin, \
         dst.open("w", encoding="utf-16", newline="") as fout:
        for line in fin:
            line = re_date.sub(r"\2/\1/\3", line)
            line = re_decimal.sub(r"\1.\2", line)
            fout.write(line)


if __name__ == "__main__":
    main()
