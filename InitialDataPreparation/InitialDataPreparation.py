import csv
import re
from prettytable import PrettyTable


with open('records.csv', encoding="utf8") as csv_records:
    records = csv.reader(csv_records, delimiter=';')

    medicinesTable = PrettyTable(["MedicineName", "Power", "CompanyName", "SubstanceName"])


    a = 0
    for row in records:

        if a != 0: 
            
            if len(row[7]) < 30 and row[7] != "-" and len(row[1]) < 30 and len(row[13]) < 30 and '\'' not in row[13] and row[15] != '' and len(row[15]) < 60: 
                medicinesTable.add_row([row[1], row[7], row[13], row[15]])
                
        a = 1

    medicineValues = "insert into Medicines \nvalues"
    substanceValues = "insert into ActiveSubstances \nvalues"

    numberOfMedicineRows = 0
    numberOfAllMedicineRows = len(medicinesTable.rows)
    currentMedicineRow = 0

    numberOfSubstancesRows = 0

    medicineSubstances = [{}]
    medicineSubstances.clear()
    
    for row in medicinesTable:

        if numberOfMedicineRows == 1000:
            numberOfMedicineRows = 0
            medicineValues += "\ninsert into Medicines\nvalues"
            
        if numberOfSubstancesRows == 1000:
            numberOfSubstancesRows = 0
            substanceValues += "\ninsert into ActiveSubstances\nvalues"

        numberOfMedicineRows += 1
        row.border = False
        row.header = False
        medicineValues += "(NEWID(), '" + row.get_string(fields=["MedicineName"]).strip() + "', '" + row.get_string(fields=["CompanyName"]).strip() + "', '" + row.get_string(fields=["Power"]).strip() + "')"

        substanceList = []
        separatedSubstancesNames = []
        value = row.get_string(fields=["SubstanceName"]).strip()
        if "+" in value:
            substanceList = value.split(" + ")

        else:
            substanceList.append(value)
            
        for element in substanceList:
            substanceName = ""
            for character in element:
                if character.isdigit():
                    break
                else:
                    substanceName += character

            substanceName = substanceName[:-1]
            separatedSubstancesNames.append(substanceName)
            
        medicineSubstances.append({"MedicineName": row.get_string(fields=["MedicineName"]).strip(), "Power": row.get_string(fields=["Power"]).strip(), "Substances" : separatedSubstancesNames})

        for element in separatedSubstancesNames:
            if element not in substanceValues:
                substanceValues += "(NEWID(), " + "'" + element + "')"
                numberOfSubstancesRows += 1
                if numberOfSubstancesRows != 1000:
                    substanceValues += ",\n"
                else:
                    substanceValues += "\n"
            

        currentMedicineRow += 1

        if numberOfMedicineRows != 1000 and currentMedicineRow != numberOfAllMedicineRows:
            medicineValues += ","
            
            
        medicineValues += "\n"

        
    substanceValues = substanceValues[:-2]
        
    medicineSubstancesValues = ""

    for element in medicineSubstances:
        medicineID = "declare @medicineID uniqueidentifier = (select top 1 MedicineID from Medicines where MedicineName = '" + element.get("MedicineName") + "' and Power = '" + element.get("Power") + "')\n" 
        for substance in element.get("Substances"):
            substanceID = "declare @substanceID uniqueidentifier = (select top 1 SubstanceID from ActiveSubstances where SubstanceName = '" + substance + "')\n" 
            medicineSubstancesValues += medicineID + substanceID + "insert into MedicineActiveSubstances values (NEWID(), @medicineID, @substanceID)\ngo\n" 

    print(medicineValues)
    print(substanceValues)
    print(medicineSubstancesValues)
    
    script = open("InitialMedicines.sql", "w", encoding="utf8")
    script.write(medicineValues)
    script.close()
    
    script = open("InitialSubstances.sql", "w", encoding="utf8")
    script.write(substanceValues)
    script.close()

    script = open("MedicineInitialSubstances.sql", "w", encoding="utf8")
    script.write(medicineSubstancesValues)
    script.close()

    print(medicinesTable)
    



