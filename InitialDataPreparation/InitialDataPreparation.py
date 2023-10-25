import csv
from dataclasses import field
import re
from prettytable import PrettyTable


with open('records.csv', encoding="utf8") as csv_records:
    records = csv.reader(csv_records, delimiter=';')
    selectedColums = []
    includedCols = [1, 7, 12, 13, 15, 22]
    for row in records:
        content = list(row[i] for i in includedCols)
        selectedColums.append(content)

    medicinesTable = PrettyTable(["MedicineName", "Power", "ATC", "CompanyName", "SubstanceName", "LeafletURL"])

    a = 0
    for row in selectedColums:

        if a != 0: 
            
            if len(row[1]) < 30 and row[1] != "-" and len(row[2]) > 0 and len(row[3]) < 30 and len(row[3]) < 30 and '\'' not in row[3] and row[4] != '' and len(row[4]) < 60: 
                newRow = [row[0], row[1], row[2], row[3], row[4], row[5]]                
                medicinesTable.add_row(newRow)
                
        a = 1
        

    medicineValues = "insert into Medicines \nvalues"
    substanceValues = "insert into ActiveSubstances \nvalues"
    medicineATCValues = "declare @ACategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'A')\n"
    medicineATCValues += "declare @BCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'B')\n"
    medicineATCValues += "declare @CCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'C')\n"
    medicineATCValues += "declare @DCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'D')\n"
    medicineATCValues += "declare @GCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'G')\n"
    medicineATCValues += "declare @HCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'H')\n"
    medicineATCValues += "declare @JCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'J')\n"
    medicineATCValues += "declare @LCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'L')\n"    
    medicineATCValues += "declare @MCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'M')\n"
    medicineATCValues += "declare @NCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'N')\n"
    medicineATCValues += "declare @PCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'P')\n"
    medicineATCValues += "declare @RCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'R')\n"
    medicineATCValues += "declare @SCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'S')\n"
    medicineATCValues += "declare @VCategory uniqueidentifier = (select ATCCategoryID from ATCCategories where ATCCategoryName = 'V')\n"

    numberOfMedicineRows = 0
    numberOfAllMedicineRows = len(medicinesTable.rows)
    currentMedicineRow = 0

    numberOfSubstancesRows = 0

    medicineSubstances = [{}]
    medicineSubstances.clear()

    substances = []
    index = 0;
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
        medicineValues += "(NEWID(), '" + row.get_string(fields=["MedicineName"]).strip() + "', '" + row.get_string(fields=["CompanyName"]).strip() + "', '" + row.get_string(fields=["Power"]).strip() + "', '" + row.get_string(fields=["LeafletURL"]).strip() + "', null)"

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
        
        atcCat = ""
        atcText = row.get_string(fields=["ATC"]).strip()
        
        if (re.match("^A(.)*", atcText)) :
            atcCat = "@ACategory"
        if (re.match("^B(.)*", atcText)) :
            atcCat = "@BCategory"
        if (re.match("^C(.)*", atcText)) :
            atcCat = "@CCategory"
        if (re.match("^D(.)*", atcText)) :
            atcCat = "@DCategory"
        if (re.match("^G(.)*", atcText)) :
            atcCat = "@GCategory"
        if (re.match("^H(.)*", atcText)) :
            atcCat = "@HCategory"
        if (re.match("^J(.)*", atcText)) :
            atcCat = "@JCategory"
        if (re.match("^L(.)*", atcText)) :
            atcCat = "@LCategory"
        if (re.match("^M(.)*", atcText)) :
            atcCat = "@MCategory"
        if (re.match("^N(.)*", atcText)) :
            atcCat = "@NCategory"
        if (re.match("^P(.)*", atcText)) :
            atcCat = "@PCategory"
        if (re.match("^R(.)*", atcText)) :
            atcCat = "@RCategory"
        if (re.match("^S(.)*", atcText)) :
            atcCat = "@SCategory"
        if (re.match("^V(.)*", atcText)) :
            atcCat = "@VCategory"
            
        if (len(atcCat) > 0) :
            if (index == 0) :
                medicineATCValues += "declare @medicineID uniqueidentifier = (select top 1 MedicineID from Medicines where MedicineName = '" + row.get_string(fields=["MedicineName"]).strip() + "' and Power = '" + row.get_string(fields=["Power"]).strip() + "' and CompanyName = '" + row.get_string(fields=["CompanyName"]).strip() + "')\n" 
            else:
                medicineATCValues += "set @medicineID = (select top 1 MedicineID from Medicines where MedicineName = '" + row.get_string(fields=["MedicineName"]).strip() + "' and Power = '" + row.get_string(fields=["Power"]).strip() + "' and CompanyName = '" + row.get_string(fields=["CompanyName"]).strip() + "')\n"

            medicineATCValues += "insert into MedicineATCCategories values (NEWID(), @medicineID, " + atcCat + ", '" + atcText + "')\n"

        index += 1       

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
    
    script = open("ATCMedicinesInitial.sql", "w", encoding="utf8")
    script.write(medicineATCValues)
    script.close()

    print(medicinesTable)
    



