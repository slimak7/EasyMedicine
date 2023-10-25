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
                      'Server=192.168.0.231,1433;'
                      'Database=MedicinesDB;'
                      'Trusted_Connection=no;'
                      'UID=medicines_client;'
                      'PWD=medicines2525;'
                      'TrustServerCertificate=yes;')

cursor = connection.cursor()


root = minidom.Document()
translationsMain = root.createElement("Translations")
translationsMain.setAttribute("Lang", "PL")
root.appendChild(translationsMain)

for pair in translations:
    cursor.execute(f"select SubstanceID from ActiveSubstances S where S.SubstanceName = '{pair[0]}'")    
    
    for result in cursor:
        substance = root.createElement("ActiveSubstance")
        substance.setAttribute("Name", pair[1])
        substance.setAttribute("SubstanceID", result[0])
        translationsMain.appendChild(substance)
        break


xml_str = root.toprettyxml(indent ="\t")

f = open("Translation_PL.xml", "w", encoding="UTF-8")
f.write(xml_str)
f.close()



