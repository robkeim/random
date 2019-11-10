import datetime
import os
import sys

from selenium import webdriver
from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions
from selenium.webdriver.support.ui import WebDriverWait

import email_helper


user_directory = "users/"
reservations_dir = "reservations/"


def login(driver, email, password):
    driver.get("https://classpass.com/login")
    element = driver.find_element_by_id("email_field")
    element.send_keys(email)

    element = driver.find_element_by_id("password_field")
    element.send_keys(password)

    button = driver.find_element_by_css_selector("[type=\"submit\"")
    button.click()


def get_credit_count(driver):
    element_present = expected_conditions.presence_of_element_located((By.CLASS_NAME, 'header__credit-count'))
    WebDriverWait(driver, 5).until(element_present)

    element = driver.find_element_by_class_name("header__credit-count")
    return int(element.text.split(" ")[0])


def book_class(user, driver, day_of_week, time, name, url):
    if driver.current_url == url:
        driver.get("about:blank")  # Force refresh of page to clear dialog of a previously reserved class

    driver.get(url)

    date = driver.find_element_by_class_name("Schedule__datebar__date").text

    # Navigate to day
    while day_of_week not in date:
        element = driver.find_elements_by_class_name("Schedule__datebar__arrow")[1]
        element.click()
        date = driver.find_element_by_class_name("Schedule__datebar__date").text

    date = date.split(",")[1].lstrip(" ") + ", " + str(datetime.datetime.now().year)

    element_present = expected_conditions.presence_of_element_located((By.CLASS_NAME, 'Schedule__row'))
    WebDriverWait(driver, 5).until(element_present)

    # Expand to see all classes
    try:
        button = driver.find_element_by_css_selector("button.bt")
        if "See more" in button.text:
            button.click()
    except NoSuchElementException:
        pass # Nothing to expand

    elements = driver.find_elements_by_class_name("Schedule__row")
    elements = [e for e in elements if time in e.text and name in e.text]

    if not elements:
        return date, "No class offered at that time"

    element = elements[0]

    if "RESERVED" in element.text:
        return date, "Already enrolled in class"

    if "Reserve" in element.text:
        return date, "Class full"

    if date_previously_reserved(user, date):
        return date, "Date previously reserved, skipping"

    element = element.find_element_by_class_name("Schedule__row__cta")
    element.click()

    element_present = expected_conditions.presence_of_element_located((By.CSS_SELECTOR, '.modal__content button'))
    WebDriverWait(driver, 5).until(element_present)
    element = driver.find_element_by_css_selector(".modal__content button")

    if "Reserve this class" not in element.text:
        return date, "Not enough credits remaining to reserve class (TODO rkeim: validate this works correctly)"

    element.click()

    expected_conditions.text_to_be_present_in_element((By.CLASS_NAME, "modal__content"), "You're booked for")
    WebDriverWait(driver, 5).until(element_present)

    add_reserved_class(user, date)

    return date, "Class successfully reserved"


def date_previously_reserved(user, date):
    with open(reservations_dir + user, "r") as f:
        if date in f.read():
            return True

    return False


def add_reserved_class(user, date):
    with open(reservations_dir + user, "a") as f:
        f.write(date + "\n")


def process_user(user):
    with open(user_directory + user) as f:
        file_contents = [l.rstrip("\n") for l in f.readlines()]

    email = file_contents[0]
    password = file_contents[1]

    driver = webdriver.Chrome()
    login(driver, email, password)
    remaining_credits = get_credit_count(driver)

    classes_to_reserve = [tuple(l.split(";")) for l in file_contents[2:]]
    results = []

    for (day_of_week, time, name, url) in classes_to_reserve:
        date, result = book_class(user, driver, day_of_week, time, name, url)
        date = date.split(",")[0]
        results.append((day_of_week, date, time, result))

    body = "\n".join([day_of_week + " " + date + " at " + time + ": " + result
                      for (day_of_week, date, time, result) in results])
    body = str(remaining_credits) + " credits remaining\n\n" + body
    email_helper.send("robkeim@gmail.com", "Summary for " + user, body)

    driver.close()


def main():
    for user in os.listdir('users'):
        if sys.gettrace():
            # Running in debug mode
            process_user(user)
        else:
            try:
                process_user(user)
            except Exception as e:
                email_helper.send("robkeim@gmail.com", "Error processing user: " + user, str(e))


if __name__ == "__main__":
    main()
