import os

from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions
from selenium.webdriver.support.ui import WebDriverWait

import email_helper


def login(driver, email, password):
    driver.get("https://classpass.com/login")
    element = driver.find_element_by_id("email_field")
    element.send_keys(email)

    element = driver.find_element_by_id("password_field")
    element.send_keys(password)

    button = driver.find_element_by_css_selector("[type=\"submit\"")
    button.click()


def get_credit_count(driver, email):
    element_present = expected_conditions.presence_of_element_located((By.CLASS_NAME, 'header__credit-count'))
    WebDriverWait(driver, 5).until(element_present)

    element = driver.find_element_by_class_name("header__credit-count")
    num_credits = int(element.text.split(" ")[0])

    if num_credits < 10:
        email_helper.send(email, "Only " + str(num_credits) + " credits remaining", "")

    return num_credits


def book_class(driver, email, day_of_week, time, url):
    driver.get(url)

    date = driver.find_element_by_class_name("Schedule__datebar__date").text

    # Navigate to day
    while day_of_week not in date:
        element = driver.find_elements_by_class_name("Schedule__datebar__arrow")[1]
        element.click()
        date = driver.find_element_by_class_name("Schedule__datebar__date").text

    element_present = expected_conditions.presence_of_element_located((By.CLASS_NAME, 'Schedule__row'))
    WebDriverWait(driver, 5).until(element_present)

    # Expand to see all classes
    button = driver.find_element_by_css_selector("button.bt")
    if "See more" in button.text:
        button.click()

    elements = driver.find_elements_by_class_name("Schedule__row")
    element = [e for e in elements if time in e.text][0]

    if "RESERVED" in element.text:
        return "Already enrolled in class"

    if "Reserve" in element.text:
        return "Class full"

    element = element.find_element_by_class_name("Schedule__row__cta")
    element.click()

    element = driver.find_element_by_css_selector(".reservation--inquiry__body button")
    element.click()

    element_present = expected_conditions.presence_of_element_located((By.CLASS_NAME, 'reservation--invite-a-friend__schedule__date-time-text'))
    WebDriverWait(driver, 5).until(element_present)

    email_helper.send(email, "Successfully reserved on " + day_of_week + " at " + time, "")
    return "Class booked"


def process_user(file_to_process):
    with open(file_to_process) as f:
        file_contents = [l.rstrip("\n") for l in f.readlines()]

    email = file_contents[0]
    password = file_contents[1]

    driver = webdriver.Chrome()
    login(driver, email, password)
    get_credit_count(driver, email)

    classes_to_reserve = [tuple(l.split(";")) for l in file_contents[2:]]
    results = []

    for (day_of_week, time, url) in classes_to_reserve:
        result = book_class(driver, email, day_of_week, time, url)
        results.append((day_of_week, time, result))

    body = "\n".join([day_of_week + " at " + time + ": " + result for (day_of_week, time, result) in results])
    email_helper.send("robkeim@gmail.com", "Summary for " + file_to_process, body)

    driver.close()


def main():
    for user in os.listdir('users'):
        try:
            process_user("users/" + user)
        except Exception as e:
            email_helper.send("robkeim@gmail.com", "Error processing user: " + user, str(e))


if __name__ == "__main__":
    main()
