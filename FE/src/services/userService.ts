import { render } from "@testing-library/react";
import http from "./http-common";

interface userRegister {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  phoneNumber: string;
  password: string;
}
export class FormRegister implements userRegister {
  firstName: string = "";
  lastName: string = "";
  userName: string = "";
  email: string = "";
  phoneNumber: string = "";
  password: string = "";
  constructor(
    firstName: string,
    lastName: string,
    userName: string,
    email: string,
    phoneNumber: string,
    password: string
  ) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.userName = userName;
    this.email = email;
    this.phoneNumber = phoneNumber;
    this.password = password;
  }
}

const register = (data: FormRegister) => {
  return http.post("/Auth/register", data);
};
interface formComfirmEmail {
  UserId: string;
  VerifyCode: string;
}
export class ComfirmEmail implements formComfirmEmail {
  UserId: string = "";
  VerifyCode: string = "";
  constructor(stringRequest: string, codeConfirm: string) {
    this.UserId = stringRequest;
    this.VerifyCode = codeConfirm;
  }
}
const confirm2FaByEmail = (objComfirm: ComfirmEmail) => {
  return http.post("/Auth/verifyEmail", objComfirm);
};

export interface formLogin {
  userName: string;
  password: string;
}
const LoginHandler = (data: formLogin) => {
  return http.post("/Auth/login", data);
};
  
export interface formVerifyToken {
    tokenString: string;
}
export class objTokeString implements formVerifyToken{
    tokenString: string = "";
    constructor(token: string){
        this.tokenString = token;
    }
}
const verifyToken = (data: formVerifyToken) => {
  return http.post("/Auth/ValidateToken", data);
};
 
const UploadFile = (file: any) => {
  const formData = new FormData();
  formData.append('file', file);

  return http.post("/File/avatar-upload", formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}
const UploadFileBackground = (file: any) => {
  const formData = new FormData();
  formData.append('file', file);

  return http.post("/File/background-upload", formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}
const UserService = {
  register,
  confirm2FaByEmail,
  LoginHandler,
  verifyToken,
  UploadFile,
  UploadFileBackground
};

export default UserService;
