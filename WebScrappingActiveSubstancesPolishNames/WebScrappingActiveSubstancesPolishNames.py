from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait
from datetime import datetime
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By

PATH = r"D:\EasyMedicine\chromedriver"
driver = webdriver.Chrome(PATH) 

url = "https://www.doz.pl/leki/litera/A"

driver.get(url)

AZMenuPath = '//*[@id="content"]/aside/section/div[1]/ol'


menu = WebDriverWait(driver, 3).until(
    EC.presence_of_element_located(("xpath", AZMenuPath)))


menuItems = WebDriverWait(menu, 3).until(EC.presence_of_all_elements_located(("xpath", "./child::li/a")))

activeSubstancesValues = ""

menuIndex = 0
numberOfMenuElements = len(menuItems)
while menuIndex < numberOfMenuElements:
    driver.get(menuItems[menuIndex].get_attribute('href'))
            
    menuIndex = menuIndex + 1
    substancesList = WebDriverWait(driver, 3).until(
        EC.presence_of_element_located(
            (
                "xpath",
                '/html/body/main/article/section/ul',
            )
        )
    )
    substances = substancesList.find_elements("xpath", "./child::*")

    index = 0
    numberOfElements = len(substances)
    while index < numberOfElements:
        substancesList = WebDriverWait(driver, 3).until(
            EC.presence_of_element_located(
                (
                    "xpath",
                    '//*[@id="infinity-scroll-list"]'
                )
            )
        )
        substances = substancesList.find_elements("xpath", "./child::li/child::a")
        substance = substances[index]
        index = index + 1
        substance.click()

        name = WebDriverWait(driver, 3).until(
            EC.presence_of_element_located(("xpath", '//*[@id="content"]/article/header/div/h1'))).text

        name = name.partition('-')[0]

        names = name.split(',')
        
        names = [x.strip().capitalize() for x in names]

        if len(names) == 2:
            activeSubstancesValues += names[1] + "," + names[0] + "\n"

        driver.back()

    menu = WebDriverWait(driver, 3).until(
    EC.presence_of_element_located(("xpath", AZMenuPath)))
    menuItems = WebDriverWait(menu, 3).until(EC.presence_of_all_elements_located(("xpath", "./child::li/a")))

print(activeSubstancesValues)
