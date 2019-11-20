from selenium import webdriver
from selenium.common.exceptions import TimeoutException
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions
from selenium.webdriver.support.ui import WebDriverWait


def login(driver):
    with open("credentials.txt") as f:
        file_contents = [l.rstrip("\n") for l in f.readlines()]

    email = file_contents[0]
    password = file_contents[1]

    driver.get("https://www.smartrecruiters.com/account/sign-in")

    element = driver.find_element_by_id("email")
    element.send_keys(email)

    element = driver.find_element_by_id("password")
    element.send_keys(password)

    element = driver.find_element_by_id("sign-in-btn")
    element.click()


def close_cookie_banner_if_needed(driver):
    elements = driver.find_elements_by_css_selector("[aria-label='dismiss cookie message']")

    if len(elements) == 0:
        return

    elements[0].click()


def process_job(driver, job_id):
    with open("job_" + job_id + ".txt", "w", encoding="utf-8") as output_file:
        driver.get("https://www.smartrecruiters.com/app/jobs/details/" + job_id + "/people")

        element_present = expected_conditions.presence_of_element_located((By.TAG_NAME, "h3"))
        WebDriverWait(driver, 5).until(element_present)
        elements = driver.find_elements_by_tag_name("h3")

        close_cookie_banner_if_needed(driver)

        for i in range(0, len(elements)):
            if i != 0:
                driver.get("https://www.smartrecruiters.com/app/jobs/details/" + job_id + "/people")

                element_present = expected_conditions.presence_of_element_located((By.TAG_NAME, "h3"))
                WebDriverWait(driver, 5).until(element_present)
                elements = driver.find_elements_by_tag_name("h3")

            actions = ActionChains(driver)
            actions.move_to_element(elements[i]).perform()

            elements[i].click()
            try:
                process_candidate(driver, output_file)
            except:
                print("Exception occurred")


def process_candidate(driver, output_file):
    element_present = expected_conditions.presence_of_element_located((By.TAG_NAME, "h1"))
    WebDriverWait(driver, 10).until(element_present)
    candidate_name = driver.find_element_by_tag_name("h1")

    try:
        element_present = expected_conditions.text_to_be_present_in_element((By.ID, "st-reviews"), "(")
        WebDriverWait(driver, 5).until(element_present)
        element = driver.find_element_by_id("st-reviews")
    except TimeoutException:
        # No accessible reviews for the candidate
        return

    element.click()

    review_blocks = []

    while len(review_blocks) == 0:
        # Drop last element which is a link to see more reviews
        review_blocks = driver.find_elements_by_class_name("storyWrapper")[:-1]

    for review_block in review_blocks:
        reviewer_name = review_block.find_elements_by_class_name("user")

        if len(reviewer_name) == 0:
            reviewer_name = "You" # The html structure is different when you are the reviewer
        else:
            reviewer_name = reviewer_name[0].text
        rating = review_block.find_element_by_tag_name("sr-rating").get_attribute("value")
        content = review_block.find_element_by_class_name("content").text.replace("\n", "\\n")
        output_file.write(candidate_name.text + ";" + reviewer_name + ";" + rating + ";" + content + "\n")


def main():
    driver = webdriver.Chrome()
    login(driver)
    # process_job(driver, "c44db5c3-ff28-48a5-af43-12628ff8b893")
    # process_job(driver, "e8c926db-dc73-44fc-ad91-efcf6d19d4ea")
    # process_job(driver, "7fcfe8a4-795e-4b3f-b281-17e614fc676e")
    # process_job(driver, "c454b830-d061-4b4f-83ae-88b4f2f294e4")
    # process_job(driver, "43c8849f-0592-4950-9351-98e1fd2ccb37")
    driver.close()


if __name__ == "__main__":
    main()