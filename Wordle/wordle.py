import re
import time
from collections import defaultdict
from selenium import webdriver
from selenium.webdriver.common.by import By


def main():
    dictionary = get_dictionary()

    driver = webdriver.Chrome()
    driver.get("https://www.powerlanguage.co.uk/wordle/")

    close_explanation_popup(driver)

    # Using the first guess recommended by this article (I didn't dive deep to see if there were better starting words):
    # https://theconversation.com/want-to-master-wordle-heres-the-best-strategy-for-your-first-guess-176325
    guess = "slice"
    excluded_letters = set()

    for i in range(6):
        result = guess_and_read_row(driver, guess, i)

        # We've found the solution
        if len(set(result)) == 1 and result[0] == "correct":
            break

        guess, excluded_letters = get_next_guess(dictionary, excluded_letters, guess, result)

    # Wait for confirmation before exiting, so you can see what the answer was
    input()
    driver.close()


def get_dictionary():
    # This dictionary comes from looking at the source code for the Wordle website. All of their computation is done
    # client side, so they have the full dictionary as well as the answer in the source code.
    with open("wordle_dictionary.txt", "r") as file:
        return [line.strip() for line in file.readlines()]


def get_next_guess(dictionary, excluded_letters, prev_guess, prev_result):
    regex_pattern = ""
    needs_to_have = defaultdict(set)

    for i in range(5):
        if prev_result[i] == "correct":
            regex_pattern += prev_guess[i]
        else:
            regex_pattern += "."

            if prev_result[i] == "absent":
                excluded_letters.add(prev_guess[i])
            else:
                needs_to_have[prev_guess[i]].add(i)

    for word in dictionary:
        if re.search(regex_pattern, word):
            match = True

            for letter in needs_to_have:
                if letter not in word or word.index(letter) in needs_to_have[letter]:
                    match = False
                    break

            for i in range(5):
                if regex_pattern[i] == "." and word[i] in excluded_letters:
                    match = False
                    break

            if match:
                return word, excluded_letters

    raise Exception("No compatible word found in the dictionary")


def close_explanation_popup(driver):
    app = driver.find_element(By.CSS_SELECTOR, "game-app")
    app = expand_shadow_root(driver, app)

    modal = app.find_element(By.CSS_SELECTOR, "game-modal")
    modal = expand_shadow_root(driver, modal)

    close_icon = modal.find_element(By.CSS_SELECTOR, ".close-icon")
    close_icon.click()


def guess_and_read_row(driver, guess, row_number):
    try_guess(driver, guess)

    # Wait for animation (there has to be a more elegant solution here, but I'm applying the KISS principle)
    time.sleep(5)

    return read_row(driver, row_number)


def try_guess(driver, guess):
    guess += "↵"

    app = driver.find_element(By.CSS_SELECTOR, "game-app")
    app = expand_shadow_root(driver, app)

    keyboard = app.find_element(By.CSS_SELECTOR, "game-keyboard")
    keyboard = expand_shadow_root(driver, keyboard)

    for letter in guess:
        key = keyboard.find_element(By.CSS_SELECTOR, "[data-key=" + letter + "]")
        key.click()


def read_row(driver, row_number):
    app = driver.find_element(By.CSS_SELECTOR, "game-app")
    app = expand_shadow_root(driver, app)

    theme_manager = app.find_element(By.CSS_SELECTOR, "game-theme-manager")
    row = theme_manager.find_elements(By.CSS_SELECTOR, "game-row")[row_number]
    row = expand_shadow_root(driver, row)

    tiles = row.find_elements(By.CSS_SELECTOR, "game-tile")

    result = []

    for tile in tiles:
        result.append(tile.get_attribute("evaluation"))

    if not result[0]:
        raise Exception("Unable to read result from row " + str(row_number + 1))

    return result


def expand_shadow_root(driver, element):
    return driver.execute_script("return arguments[0].shadowRoot", element)


if __name__ == '__main__':
    main()
