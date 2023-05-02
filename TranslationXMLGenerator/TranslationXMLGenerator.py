import csv
import pyodbc 

with open("Language_PL_base_file.csv", encoding="UTF-8") as baseFile:
    reader = csv.reader(baseFile, delimiter=',')

    translations = [[]]

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

cursor.execute('SELECT top 50 * FROM Medicines')

for i in cursor:
    print(i)

