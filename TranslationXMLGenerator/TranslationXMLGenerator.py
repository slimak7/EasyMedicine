import csv
import pyodbc 
from xml.dom import minidom

with open("Language_PL_base_file.csv", encoding="UTF-8") as baseFile:
    reader = csv.reader(baseFile, delimiter=',')

    translations = [[]]
    translations.clear()

    for row in reader:
        translations.append([row[0], row[1]])

    print(translations)

connection = pyodbc.connect('Driver={ODBC Driver 18 for SQL Server};'
                      'Server=(localdb)\\MSSQLLocalDB;'
                      'Database=MedicinesDB;'
                      'Trusted_Connection=no;'
                      'UID=sk;'
                      'PWD=sssqddffrg45$$g;')

cursor = connection.cursor()


root = minidom.Document()
translationsMain = root.createElement("Translations")
translationsMain.setAttribute("Lang", "PL")
root.appendChild(translationsMain)

for pair in translations:
    cursor.execute(f"select M.MedicineID from Medicines M inner join MedicineActiveSubstances MS on M.MedicineID = MS.MedicineID inner join ActiveSubstances S on MS.ActiveSubstanceSubstanceID = S.SubstanceID where S.SubstanceName = '{pair[0]}'")
    substance = root.createElement("ActiveSubstance")
    substance.setAttribute("Name", pair[1])
    
    for result in cursor:
        medicine = root.createElement("Medicine")
        medicine.setAttribute("ID", result[0])
        substance.appendChild(medicine)

    translationsMain.appendChild(substance)

xml_str = root.toprettyxml(indent ="\t")

f = open("Translation_PL.xml", "w", encoding="UTF-8")
f.write(xml_str)
f.close()



