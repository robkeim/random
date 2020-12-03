# Shameless copied (and then modified) from here:
# https://github.com/Universemul/AdventOfCode2020/blob/master/start.py

import datetime
import os
import requests
import shutil


def main():
    now = datetime.datetime.now()
    day = now.day
    year = now.year

    copy_skeleton(day)
    download_input_file(day, year)


def copy_skeleton(day):
    file = "day{}.py".format(get_day_string(day))

    if os.path.isfile(file):
        print("Python file for today already exists, skipping")
        return

    shutil.copyfile("skeleton.py", file)


def download_input_file(day, year):
    # The value of the session cookie from the website needs to be set in an environment variable in order to
    # be able to download the input
    session_cookie = os.getenv("AOC_SESSION_COOKIE")

    if not session_cookie:
        print("Could not find session cookie in environment variable")
        return

    url = "https://adventofcode.com/{}/day/{}/input".format(year, day)
    response = requests.get(url, cookies={"session": session_cookie})

    with open("day{}.txt".format(get_day_string(day)), "wb") as output:
        output.write(response.content)


def get_day_string(day):
    return str(day) if day > 9 else "0" + str(day)


if __name__ == "__main__":
    main()
