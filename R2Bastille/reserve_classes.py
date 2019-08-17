import datetime
from selenium import webdriver


def read_password():
    with open("password.txt") as file:
        return file.read()


def login(driver):
    element = driver.find_element_by_id("maillog2")
    element.send_keys("robkeim@gmail.com")

    element = driver.find_element_by_id("mailpass2")
    element.send_keys(read_password())

    element = driver.find_element_by_css_selector("input.btn-submit")
    element.click()

    # Switch to new tab that opens after login
    driver.switch_to.window(driver.window_handles[1])


def change_week_if_needed(driver, day_to_reserve):
    if datetime.datetime.today().weekday() > day_to_reserve:
        elements = driver.find_elements_by_css_selector("input.btn-filter")
        next_week = list(filter(lambda x: x.get_attribute("value") == " >> ", elements))[0]
        next_week.click()


def click_on_course_if_available(driver, day, time):
    days = driver.find_elements_by_class_name("titre-jour")
    day = list(filter(lambda x: x.text == day, days))[0].find_element_by_xpath("..")  # Access parent
    course = list(filter(lambda x: time in x.text, day.find_elements_by_css_selector("div")))[1]

    if "LIBRE" in course.text:
        course.click()
        return True

    return False


def main():
    driver = webdriver.Chrome()
    driver.get("https://resa-r2training.deciplus.pro/sp_lecons_planning.php")
    login(driver)
    change_week_if_needed(driver, 0)  # 0 -> Monday
    is_available = click_on_course_if_available(driver, "Lundi", "20:00")

    if is_available:
        submit = driver.find_element_by_css_selector("[type=submit]")
        submit.click()
        pass

    driver.close()


if __name__ == "__main__":
    try:
        main()
    except Exception as e:
        print("Error: " + str(e))
