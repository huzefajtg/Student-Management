export interface RegisterDetails{
    PersonalInfo:Personal,
    UserInfo:User,

}

export interface Personal{
    firstName:string,
    secondName:string,
    lastname:string,
    isReg:boolean,

    gender:string,
    emailId:string,
    contactNumber:string,
    contactAddress:string,
    dob:string,
    type:string
}
export interface KeyValuePairResource{
    id:number,
    name:string
}

export interface User{
    username:string,
    password:string
}

export interface TeacherResource{
    personalInfo:Personal,
    hod:KeyValuePairResource,
    subjectInfo:KeyValuePairResource,

    username:string,
    teacherId:number,
    courseId:number,
    isHod:boolean,
    isReg:boolean
}

