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

    pagesList = WebDriverWait(driver, 3).until(
        EC.presence_of_element_located(
            (
            "xpath",
            '//*[@id="pagination"]/ul'
        )   )
    )

    nextPageLinks = WebDriverWait(pagesList, 3).until(EC.presence_of_all_elements_located(("xpath", "./child::li/a")))

    paginationIndex = 0
    numberOfPages = len(nextPageLinks) - 2

    if numberOfPages <= 0:
        numberOfPages = 1
    
    while paginationIndex < numberOfPages:
        paginationIndex = paginationIndex + 1
        index = 0
        substancesList = WebDriverWait(driver, 3).until(
        EC.presence_of_element_located(
            (
                "xpath",
                '/html/body/main/article/section/ul',
            )
        )
        )
        substances = substancesList.find_elements("xpath", "./child::*")
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
            

            try:

                substance.click()

                name = WebDriverWait(driver, 3).until(
                    EC.presence_of_element_located(("xpath", '//*[@id="content"]/article/header/div/h1'))).text

                name = name.partition('-')[0]

                names = name.split(',')
        
                names = [x.strip().capitalize() for x in names]

                if len(names) == 2:
                    activeSubstancesValues += names[1] + "," + names[0] + "\n"

                driver.back()

            except:
                print("Can no find header")
                
            pagesList = WebDriverWait(driver, 3).until(
                EC.presence_of_element_located(
                    (
                    "xpath",
                    '//*[@id="pagination"]/ul'
                )   )
            )

        nextPageLinks = WebDriverWait(pagesList, 3).until(EC.presence_of_all_elements_located(("xpath", "./child::li/a")))

        if len(nextPageLinks) > paginationIndex:
            nextPageLinks[paginationIndex].click()

    menu = WebDriverWait(driver, 3).until(
    EC.presence_of_element_located(("xpath", AZMenuPath)))
    menuItems = WebDriverWait(menu, 3).until(EC.presence_of_all_elements_located(("xpath", "./child::li/a")))

f = open("Language_PL_base_file.csv", "w", encoding = "UTF-8")
f.write(activeSubstancesValues)
f.close()
print(activeSubstancesValues)
