# Shamelessly copied (and then modified) from here:
# https://github.com/Universemul/AdventOfCode2020/blob/master/start.py

import datetime
import os
import requests
import shutil
import sys


def main():
    now = datetime.datetime.now()
    day = now.day if len(sys.argv) < 2 else int(sys.argv[1])
    year = now.year if len(sys.argv) < 3 else int(sys.argv[2])

    session_cookie = get_session_cookie()

    target_dir = str(year)
    os.makedirs(target_dir, exist_ok=True)

    copy_skeleton(day, target_dir)
    download_input(day, year, target_dir, session_cookie)


def get_session_cookie():
    # The value of the session cookie from the website needs to be set in an environment variable in order to
    # be able to download the input
    session_cookie = os.getenv("AOC_SESSION_COOKIE")

    if not session_cookie:
        raise Exception("No AOC_SESSION_COOKIE set in environment variable")

    return session_cookie


def copy_skeleton(day, target_dir):
    day_str = get_day_string(day)
    dst_file = os.path.join(target_dir, f"day{day_str}.py")

    if os.path.isfile(dst_file):
        print(f"{dst_file} already exists, skipping.")
        sys.exit(1)

    shutil.copyfile("skeleton.py", dst_file)
    print(f"Created {dst_file}.")


def download_input(day, year, target_dir, session_cookie):
    url = f"https://adventofcode.com/{year}/day/{day}/input"
    r = requests.get(url, cookies={"session": session_cookie})
    r.raise_for_status()

    day_str = get_day_string(day)
    txt_file = os.path.join(target_dir, f"day{day_str}.txt")

    with open(txt_file, "wb") as f:
        f.write(r.content)

    # Make a backup copy of the input file
    real_file = txt_file.replace(".", "real.")
    shutil.copy(txt_file, real_file)
    print(f"Downloaded input to {txt_file} and {real_file}.")


def get_day_string(day):
    return str(day).zfill(2)


if __name__ == "__main__":
    main()
