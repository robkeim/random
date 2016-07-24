module Types

// The following fields are still to be parsed:
// - AgeRecode52
// - AgeRecode27
// - AgeRecode12
// - InfantAgeRecode22
// - PlaceOfDeathAndDecedentsStatus
// - DayOfWeekOfDeath
// - CurrentDataYear
// - InjuryAtWork
// - MannerOfDeath
// - MethodOfDisposition
// - Autopsy
// - ActivityCode
// - PlaceOfInjury
// - Icd10Code
// - CauseRecode358
// - CauseRecode113
// - InfantCauseRecode130
// - CauseRecode39
// - NumberOfEntityAxisConditions
// - NumberOfRecordAxisConditions
// - Race
// - BridgedRaceFlag
// - RaceImputationFlag
// - RaceRecode3
// - RaceRecode5
// - HispanicOrigin
// - HispanicOriginRaceRecode

type ResidentStatus =
    Residents
    | IntrastateResidents
    | InterstateResidents
    | ForeignResidents

type Education1989Revision =
    NoFormalEducation
    | OneYearOfElementarySchool
    | TwoYearsOfElementarySchool
    | ThreeYearsOfElementarySchool
    | FourYearsOfElementarySchool
    | FiveYearsOfElementarySchool
    | SixYearsOfElementarySchool
    | SevenYearsOfElementarySchool
    | EightYearsOfElementarySchool
    | OneYearOfHighSchool
    | TwoYearsOfHighSchool
    | ThreeYearsOfHighSchool
    | FourYearsOfHighSchool
    | OneYearOfCollege
    | TwoYearsOfCollege
    | ThreeYearsOfCollege
    | FourYearsOfCollege
    | FiveOrMoreYearsOfCollege
    | NotStated

type Education2003Revision =
    EighthGradeOrLess
    | NinthThroughTwelvethGraveNoDiploma
    | HighSchoolGraduateOrGedcompleted
    | SomeCollegeCreditButNoDegree
    | AssociateDegree
    | BachelorsDegree
    | MastersDegree
    | DoctorateOrProfessionalDegree
    | Unknown

type EducationReportingFlag =
    NineteenEightyNineRevisionOfEducationItemOnCertificate
    | TwoThousandAndThreeRevisionOfEducationItemOnCertificate
    | NoEducationItemOnCertificate

type Sex =
    Male
    | Female

type AgeType =
    Years
    | Months
    | Days
    | Hours
    | Minutes
    | AgeNotStated

type AgeSubstituionFlag =
    ReportedAge
    | CalculatedAge

// TODO: AgeRecode52
// TODO: AgeRecode27
// TODO: AgeRecode12
// TODO: InfantAgeRecode22
// TODO: PlaceOfDeathAndDecedentsStatus

type MaritalStatus =
    SingleNeverMarried
    | Married
    | Widowed
    | Divorced
    | Unknown

type DeathRecord =
    {
        Id : int;
        ResidentStatus : ResidentStatus
        Education1989Revision : Education1989Revision
        Education2003Revision : Education2003Revision
        EducationReportingFlag : EducationReportingFlag
        MonthOfDeath : int; // 1 -> January ... 12 -> December
        Sex: Sex;
        AgeType: AgeType;
        Age: int;
        AgeSubstitutionFlag: AgeSubstituionFlag;
        // TODO: AgeRecode52
        // TODO: AgeRecode27
        // TODO: AgeRecode12
        // TODO: InfantAgeRecode22
        // TODO: PlaceOfDeathAndDecedentsStatus
        MaritalStatus: MaritalStatus;
    }