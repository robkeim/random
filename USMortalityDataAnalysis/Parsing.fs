﻿module Parsing

open System
open System.IO
open FParsec
open Types

//let private file =  @"C:\Users\rkeim\OneDrive\code\random\USMortalityDataAnalysis\OriginalData\DeathRecords_TestSet.csv"
let private file =  @"C:\Users\rkeim\OneDrive\code\random\USMortalityDataAnalysis\OriginalData\DeathRecords.csv"

let private pWord : Parser<string, unit> =
    manySatisfy ((<>) ',')

let private pLine : Parser<string list, unit> =
    sepBy pWord (pstring "," >>. spaces)

let private parseLines line =
    match run pLine line with
    | Success(result, _, _) -> result
    | Failure(errorMsg, _, _) -> raise (Exception(errorMsg))

let private parseInt string =
    match run pint32 string with
    | Success(result, _, _) -> result
    | Failure(errorMsg, _, _) -> raise (Exception(errorMsg))

let private parseResidentStatus string =
    match parseInt string with
    | 1 -> ResidentStatus.Residents
    | 2 -> ResidentStatus.InterstateResidents
    | 3 -> ResidentStatus.IntrastateResidents
    | 4 -> ResidentStatus.ForeignResidents
    | _ -> raise (Exception("Unexpected value for ResidentStatus"))

let private parseEducation1989Revision string =
    match parseInt string with
    | 0 -> Education1989Revision.NoFormalEducation
    | 1 -> Education1989Revision.OneYearOfElementarySchool
    | 2 -> Education1989Revision.TwoYearsOfElementarySchool
    | 3 -> Education1989Revision.ThreeYearsOfElementarySchool
    | 4 -> Education1989Revision.FourYearsOfElementarySchool
    | 5 -> Education1989Revision.FiveYearsOfElementarySchool
    | 6 -> Education1989Revision.SixYearsOfElementarySchool
    | 7 -> Education1989Revision.SevenYearsOfElementarySchool
    | 8 -> Education1989Revision.EightYearsOfElementarySchool
    | 9 -> Education1989Revision.OneYearOfHighSchool
    | 10 -> Education1989Revision.TwoYearsOfHighSchool
    | 11 -> Education1989Revision.ThreeYearsOfHighSchool
    | 12 -> Education1989Revision.FourYearsOfHighSchool
    | 13 -> Education1989Revision.OneYearOfCollege
    | 14 -> Education1989Revision.TwoYearsOfCollege
    | 15 -> Education1989Revision.ThreeYearsOfCollege
    | 16 -> Education1989Revision.FourYearsOfCollege
    | 17 -> Education1989Revision.FiveOrMoreYearsOfCollege
    | 99 -> Education1989Revision.NotStated
    | _ -> raise (Exception("Unexpected value for Education1989Revision"))

let private parseEducation2003Revision string =
    match parseInt string with
    | 1 -> Education2003Revision.EighthGradeOrLess
    | 2 -> Education2003Revision.NinthThroughTwelvethGraveNoDiploma
    | 3 -> Education2003Revision.HighSchoolGraduateOrGedcompleted
    | 4 -> Education2003Revision.SomeCollegeCreditButNoDegree
    | 5 -> Education2003Revision.AssociateDegree
    | 6 -> Education2003Revision.BachelorsDegree
    | 7 -> Education2003Revision.MastersDegree
    | 8 -> Education2003Revision.DoctorateOrProfessionalDegree
    | 9 -> Education2003Revision.Unknown
    | 0 -> Education2003Revision.Unknown // Not in the spec but shows up in the data
    | _ -> raise (Exception("Unexpected value for Education2003Revision"))

let private parseEducationReportingFlag string =
    match parseInt string with
    | 0 -> EducationReportingFlag.NineteenEightyNineRevisionOfEducationItemOnCertificate
    | 1 -> EducationReportingFlag.TwoThousandAndThreeRevisionOfEducationItemOnCertificate
    | 2 -> EducationReportingFlag.NoEducationItemOnCertificate
    | _ -> raise (Exception("Unexpected value for EducationReportingFlag"))

let private parseSex string =
    match string with
    | "M" -> Sex.Male
    | "F" -> Sex.Female
    | _ -> raise (Exception("Unexpected value for Sex"))

let private parseAgeType string =
    match parseInt string with
    | 1 -> AgeType.Years
    | 2 -> AgeType.Months
    | 4 -> AgeType.Days
    | 5 -> AgeType.Hours
    | 6 -> AgeType.Minutes
    | 9 -> AgeType.AgeNotStated
    | _ -> raise (Exception("Unexpected value for AgeType"))

let private parseAgeSubstitutionFlag string =
    match parseInt string with
    | 0 -> AgeSubstituionFlag.ReportedAge
    | 1 -> AgeSubstituionFlag.CalculatedAge
    | _ -> raise (Exception("Unexpected value for AgeSubstituionFlag"))

// TODO: AgeRecode52
// TODO: AgeRecode27
// TODO: AgeRecode12
// TODO: InfantAgeRecode22
// TODO: PlaceOfDeathAndDecedentsStatus

let private parseMaritalStatus string =
    match string with
    | "S" -> MaritalStatus.SingleNeverMarried
    | "M" -> MaritalStatus.Married
    | "W" -> MaritalStatus.Widowed
    | "D" -> MaritalStatus.Divorced
    | "U" -> MaritalStatus.Unknown
    | _ -> raise (Exception("Unexpected value for MaritalStatus"))

// TODO: DayOfWeekOfDeath
// TODO: CurrentDataYear

let private parseInjuryAtWork string =
    match string with
    | "Y" -> InjuryAtWork.Yes
    | "N" -> InjuryAtWork.No
    | "U" -> InjuryAtWork.Unknown
    | _ -> raise (Exception("Unexpected value for InjuryAtWork"))

let private parseMannerOfDeath string =
    match parseInt string with
    | 1 -> MannerOfDeath.Accident
    | 2 -> MannerOfDeath.Suicide
    | 3 -> MannerOfDeath.Homicide
    | 4 -> MannerOfDeath.PendingInvestigation
    | 5 -> MannerOfDeath.CouldNotDetermine
    | 6 -> MannerOfDeath.SelfInflicted
    | 7 -> MannerOfDeath.Natural
    | 0 -> MannerOfDeath.NotSpecified
    | _ -> raise (Exception("Unexpected value for MannerOfDeath"))

let private parseMethodOfDisposition string =
    match string with
    | "B" -> MethodOfDisposition.Buriel
    | "C" -> MethodOfDisposition.Cremation
    | "O" -> MethodOfDisposition.Other
    | "U" -> MethodOfDisposition.Unknown
    | "R" -> MethodOfDisposition.Unknown // Not in the spec but shows up in the data
    | "D" -> MethodOfDisposition.Unknown // Not in the spec but shows up in the data
    | "E" -> MethodOfDisposition.Unknown // Not in the spec but shows up in the data
    | _ -> raise (Exception("Unexpected value for MethodOfDisposition"))

let private parseAutopsy (string : string) =
    match string.ToUpper() with
    | "Y" -> Autopsy.Yes
    | "N" -> Autopsy.No
    | "U" -> Autopsy.Unknown
    | _ -> raise (Exception("Unexpected value for Autopsy"))

// TODO: ActivityCode
// TODO: PlaceOfInjury
// TODO: Icd10Code
// TODO: CauseRecode358
// TODO: CauseRecode113
// TODO: InfantCauseRecode130
// TODO: CauseRecode39
// TODO: NumberOfEntityAxisConditions
// TODO: NumberOfRecordAxisConditions
// TODO: Race
// TODO: BridgedRaceFlag
// TODO: RaceImputationFlag
// TODO: RaceRecode3
// TODO: RaceRecode5
// TODO: HispanicOrigin
// TODO: HispanicOriginRaceRecode

let private parseDeathRecord (line : string) : Option<DeathRecord> =
    let fields = parseLines line

    let id = parseInt fields.[0]
    let residentStatus = parseResidentStatus fields.[1]
    let education1989Revision = parseEducation1989Revision fields.[2]
    let education2003Revision = parseEducation2003Revision fields.[3]
    let educationReportingFlag = parseEducationReportingFlag fields.[4]
    let monthOfDeath = parseInt fields.[5]
    let sex = parseSex fields.[6]
    let ageType = parseAgeType fields.[7]
    let age = parseInt fields.[8]
    let ageSubstitutionFlag = parseAgeSubstitutionFlag fields.[9]
    // TODO: AgeRecode52
    // TODO: AgeRecode27
    // TODO: AgeRecode12
    // TODO: InfantAgeRecode22
    // TODO: PlaceOfDeathAndDecedentsStatus
    let maritalStatus = parseMaritalStatus fields.[15]
    // TODO: DayOfWeekOfDeath
    // TODO: CurrentDataYear
    let injuryAtWork = parseInjuryAtWork fields.[18]
    let mannerOfDeath = parseMannerOfDeath fields.[19]
    let methodOfDisposition = parseMethodOfDisposition fields.[20]
    let autopsy = parseAutopsy fields.[21]
    // TODO: ActivityCode
    // TODO: PlaceOfInjury
    // TODO: Icd10Code
    // TODO: CauseRecode358
    // TODO: CauseRecode113
    // TODO: InfantCauseRecode130
    // TODO: CauseRecode39
    // TODO: NumberOfEntityAxisConditions
    // TODO: NumberOfRecordAxisConditions
    // TODO: Race
    // TODO: BridgedRaceFlag
    // TODO: RaceImputationFlag
    // TODO: RaceRecode3
    // TODO: RaceRecode5
    // TODO: HispanicOrigin
    // TODO: HispanicOriginRaceRecode

    let deathRecord =
        {
            Id = id
            ResidentStatus = residentStatus
            Education1989Revision = education1989Revision
            Education2003Revision = education2003Revision
            EducationReportingFlag = educationReportingFlag
            MonthOfDeath = monthOfDeath
            Sex = sex
            AgeType = ageType
            Age = age
            AgeSubstitutionFlag = ageSubstitutionFlag
            // TODO: AgeRecode52
            // TODO: AgeRecode27
            // TODO: AgeRecode12
            // TODO: InfantAgeRecode22
            // TODO: PlaceOfDeathAndDecedentsStatus
            MaritalStatus = maritalStatus
            // TODO: DayOfWeekOfDeath
            // TODO: CurrentDataYear
            InjuryAtWork = injuryAtWork
            MannerOfDeath = mannerOfDeath
            MethodOfDisposition = methodOfDisposition
            Autopsy = autopsy
            // TODO: ActivityCode
            // TODO: PlaceOfInjury
            // TODO: Icd10Code
            // TODO: CauseRecode358
            // TODO: CauseRecode113
            // TODO: InfantCauseRecode130
            // TODO: CauseRecode39
            // TODO: NumberOfEntityAxisConditions
            // TODO: NumberOfRecordAxisConditions
            // TODO: Race
            // TODO: BridgedRaceFlag
            // TODO: RaceImputationFlag
            // TODO: RaceRecode3
            // TODO: RaceRecode5
            // TODO: HispanicOrigin
            // TODO: HispanicOriginRaceRecode
        }
    Some deathRecord
    
let GetDeathRecords : DeathRecord array =
    File.ReadAllLines file
    |> Array.tail // Remove the first line which describes the format of the file
    |> Array.choose parseDeathRecord