import csv
from prettytable import PrettyTable


with open('records.csv', encoding="utf8") as csv_records:
    records = csv.reader(csv_records, delimiter=';')

    table = PrettyTable(["MedicineName", "Power", "CompanyName"])

    a = 0
    for row in records:

        if a != 0: 

            power = row[7]

            if power == "-":
                power = ""

            if len(power) > 30:
                power = power[:30]

            if len(row[1]) < 30 and len(row[13]) < 30: 
                table.add_row([row[1], power, row[13]])



        a = 1

    print(table)



