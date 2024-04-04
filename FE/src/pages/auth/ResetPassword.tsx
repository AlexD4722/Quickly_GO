import { useForm, SubmitHandler } from "react-hook-form"
import { AuthFormLayout, AuthFormSectionLayout, EyeClose, EyeOpen, IntroTitle } from "./Auth";
import { HTMLInputTypeAttribute, useEffect, useRef, useState } from "react";
import Button from "../../components/authButton";
import { UseFormRegister, RegisterOptions, FieldErrors } from 'react-hook-form';


interface FormValues {
    email: string,
    oldpassword: string,
    newpassword: string
}

type name  = 'email' | 'oldpassword' | 'newpassword';

type FieldRegisterType = {
    name: name,
    type: HTMLInputTypeAttribute,
    register: UseFormRegister<FormValues>,
    registerOptions: RegisterOptions,
    errors: FieldErrors
}

type ErrorsType = {
    name: name,
    errors: FieldErrors,
    message: string
}



export default function ResetPass () {

    const { 
        register,
        handleSubmit, 
        watch,
        reset,
        formState: {errors} 
    } = useForm<FormValues>();

    const onSubmit: SubmitHandler<FormValues> = (data) => {
        setIsSuccessfulRegisted(true);
        setMessage(data);
        setTimeout(()=>{setIsSuccessfulRegisted(false)},3000);
        reset({
            email: '',
            oldpassword: '',
            newpassword: ''
        })
    }


    const [ showPass, setShowPass ] = useState(false);
    const [ showRPass, setShowRPass ] = useState(false);
    const [ isSuccessfulRegisted, setIsSuccessfulRegisted ] = useState(false);
    const [ message, setMessage ] = useState<any>();

    const revealPass = () => {
        let x = document.getElementById("oldpassword")  as HTMLInputElement;
        if (x.type === "password") {
            x.type = "text";
            setShowPass(true);
          } else {
            x.type = "password"; 
            setShowPass(false)       
          }
            
    }

    const revealRPass = () => {
        let x = document.getElementById("newpassword")  as HTMLInputElement;
        if (x.type === "password") {
            x.type = "text";
            setShowRPass(true);
          } else {
            x.type = "password"; 
            setShowRPass(false)       
          }
    }

    useEffect(() => {
        if(isSuccessfulRegisted){
            alert(JSON.stringify(message));
        }
    },[isSuccessfulRegisted, message])

    return (
        <>
            <AuthFormSectionLayout>
                <IntroTitle heading={'Change Password'} description={'Reset your password using this form.'} />

                <AuthFormLayout onSubmit={handleSubmit(onSubmit)}>
                        <div className="flex flex-col">
                            <label htmlFor="email" className="font-semibold mb-1">Email</label> 
                            <Field register={register} errors={errors} name={'email'} type="text"
                            registerOptions={{required: true, pattern: /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/}} />                         
                        </div>
                        <Errors name="email" errors={errors} message="Email is required!"/>

                        <div className="flex flex-col mt-5 relative">  
                            <label htmlFor="oldpassword" className="font-semibold mb-1">Old Password</label>
                            <Field type="password" register={register} registerOptions={{required: true}} errors={errors} name={'oldpassword'}/>
                            
                            <div className="absolute top-1/2 mt-1 right-5 hover:cursor-pointer hover:text-[#4eac6d]" onClick={revealPass}>
                                {showPass ? <EyeOpen/> : <EyeClose/> }
                            </div>
                        </div>
                        <Errors name="oldpassword" errors={errors} message="Enter your old password!"/>

                        <div className="flex flex-col mt-5 relative">  
                            <label htmlFor="newpassword" className="font-semibold mb-1">New Password</label>
                            <Field type="password" register={register} registerOptions={{required: true}} errors={errors} name={'newpassword'}/>
                            
                            <div className="absolute top-1/2 mt-1 right-5 hover:cursor-pointer hover:text-[#4eac6d]" onClick={revealRPass}>
                            {showRPass ? <EyeOpen/> : <EyeClose/> }
                            </div>
                        </div>
                        <Errors name="newpassword" errors={errors} message="Enter your new password!"/>

                        <div className="p-2 my-5"></div>

                        <Button type={'submit'}>
                            <span>Change Password</span>
                        </Button>
                </AuthFormLayout>

                <div className="mt-10">
                    <a href="/auth/login" className="text-[#4eac6d] underline">Back to Sign In</a>               
                </div>
            </AuthFormSectionLayout>
        </>
    )
}

export const Field = ({register, errors, name, type, registerOptions} : FieldRegisterType) => {

    return (
        <>
        <input type={type} id={name} autoComplete={'off'}
        className={`rounded-md p-3 text-2xl border-gray-300 ${errors[`${name}`] ? 'border-red-500 focus:border-red-500': 'focus:border-black'} `}  
        {...register(name, registerOptions)} />
        </>
    )
}

export const Errors = ({errors, name, message} : ErrorsType) => {

    return (
        <>
       {errors[`${name}`] && <div className="text-red-500 mt-2">{message}</div>}
        </>
    )
}