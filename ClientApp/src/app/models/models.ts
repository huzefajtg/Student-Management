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

export interface User{
    username:string,
    password:string
}

