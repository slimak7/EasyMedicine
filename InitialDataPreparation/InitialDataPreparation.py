import csv
from prettytable import PrettyTable
from more_itertools import unique_everseen


with open('records.csv', encoding="utf8") as csv_records:
    records = csv.reader(csv_records, delimiter=';')
    selectedColums = []
    includedCols = [1, 7, 13, 15, 22]
    for row in records:
        content = list(row[i] for i in includedCols)
        selectedColums.append(content)
    selectedRecords = unique_everseen(selectedColums)

    medicinesTable = PrettyTable(["MedicineName", "Power", "CompanyName", "SubstanceName", "LeafletURL"])

    a = 0
    for row in selectedRecords:

        if a != 0: 
            
            if len(row[1]) < 30 and row[1] != "-" and len(row[2]) < 30 and len(row[2]) < 30 and '\'' not in row[2] and row[3] != '' and len(row[3]) < 60: 
                newRow = [row[0], row[1], row[2], row[3], row[4]]                
                medicinesTable.add_row(newRow)
                
        a = 1
        

    medicineValues = "insert into Medicines \nvalues"
    substanceValues = "insert into ActiveSubstances \nvalues"

    numberOfMedicineRows = 0
    numberOfAllMedicineRows = len(medicinesTable.rows)
    currentMedicineRow = 0

    numberOfSubstancesRows = 0

    medicineSubstances = [{}]
    medicineSubstances.clear()

    substances = []
    
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
        medicineValues += "(NEWID(), '" + row.get_string(fields=["MedicineName"]).strip() + "', '" + row.get_string(fields=["CompanyName"]).strip() + "', '" + row.get_string(fields=["Power"]).strip() + "', '" + row.get_string(fields=["LeafletURL"]).strip() + "')"

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
            
        medicineSubstances.append({"MedicineName": row.get_string(fields=["MedicineName"]).strip(), "Power": row.get_string(fields=["Power"]).strip(),"CompanyName": row.get_string(fields=["CompanyName"]).strip(), "Substances": separatedSubstancesNames})

        for element in separatedSubstancesNames:
            if element not in substances:
                substanceValues += "(NEWID(), " + "'" + element + "')"
                numberOfSubstancesRows += 1
                substances.append(element)
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
        medicineID = "declare @medicineID uniqueidentifier = (select top 1 MedicineID from Medicines where MedicineName = '" + element.get("MedicineName") + "' and Power = '" + element.get("Power") + "' and CompanyName = '" + element.get("CompanyName") + "')\n" 
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
    



