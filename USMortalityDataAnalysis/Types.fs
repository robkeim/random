module Types

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

// TODO: DayOfWeekOfDeath
// TODO: CurrentDataYear

type InjuryAtWork =
    Yes
    | No
    | Unknown

type MannerOfDeath =
    Accident
    | Suicide
    | Homicide
    | PendingInvestigation
    | CouldNotDetermine
    | SelfInflicted
    | Natural
    | NotSpecified

type MethodOfDisposition =
    Buriel
    | Cremation
    | Other
    | Unknown

type Autopsy =
    Yes
    | No
    | Unknown

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

type DeathRecord =
    {
        Id : int
        ResidentStatus : ResidentStatus
        Education1989Revision : Education1989Revision
        Education2003Revision : Education2003Revision
        EducationReportingFlag : EducationReportingFlag
        MonthOfDeath : int // 1 -> January ... 12 -> December
        Sex: Sex
        AgeType: AgeType
        Age: int
        AgeSubstitutionFlag: AgeSubstituionFlag
        // TODO: AgeRecode52
        // TODO: AgeRecode27
        // TODO: AgeRecode12
        // TODO: InfantAgeRecode22
        // TODO: PlaceOfDeathAndDecedentsStatus
        MaritalStatus: MaritalStatus
        // TODO: DayOfWeekOfDeath
        // TODO: CurrentDataYear
        InjuryAtWork: InjuryAtWork
        MannerOfDeath: MannerOfDeath
        MethodOfDisposition: MethodOfDisposition
        Autopsy: Autopsy
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