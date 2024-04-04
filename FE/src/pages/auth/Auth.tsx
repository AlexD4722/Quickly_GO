import { Outlet } from "react-router-dom";
import bgLayerImg from '../../assets/images/black-woman-hold-sign.png';
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { FormEventHandler } from 'react'

export type Account = {
    email: string;
    password: string;
}

type IntroTitleType = {
    heading: string,
    description: string
}
export default function Auth () {

    const navigate = useNavigate();

    return (
        <>
           <div className="bg-[#4eac6d] min-h-screen w-screen relative">

                <div className="fixed top-0 left-0 w-screen h-screen pointer-events-none select-none z-[1] lg:visible sm:invisible">
                    <div className="absolute ml-24 w-auto bottom-0
                    xl:h-[55%]
                    lg:h-[40%]
                    ">
                        <img src={bgLayerImg} alt="" className="h-full w-auto" />
                    </div>
                </div>

                <div className="p-10 flex w-full min-h-screen 
                lg:flex-row
                sm:flex-col
                ">

                    <div className="
                    lg:h-full lg:w-[25%] lg:p-12 
                    sm:h-auto sm:w-auto sm:p-8
                    ">
                        <div className="flex items-center lg:justify-start sm:justify-center">
                            <Logo className="text-white"/>
                            <h1 className="lg:ml-3 font-semibold text-[2.85rem] text-white">QuicklyGo</h1>
                        </div>
                        <div className="mt-2 flex items-center lg:justify-start sm:justify-center">
                            <h2 className="lg:ml-2 text-white opacity-85 text-[2rem]">Let&apos;s get connected!</h2>
                        </div>
                    </div>

                    <div 
                    className="ml-5 bg-white rounded-3xl relative z-[0]                     
                    lg:overflow-hidden lg:py-0 lg:flex-grow lg:text-2xl
                    sm:overflow-scroll sm:py-10 sm:text-xl
                    ">
                        <Outlet/>

                        <div className=" bottom-0 left-0 w-full flex justify-center items-center text-2xl 
                        lg:my-10 lg:absolute lg:flex-row lg:pt-0
                        sm:my-5 sm:relative sm:flex-col sm:pt-5
                        ">
                            <span>&copy; 2024 QuicklyGo Inc.</span>
                            <span>All rights reserved.</span>
                        </div>
                    </div>
                </div>

                
           </div>
        </>
    )
}

const Logo = ({className = ''} : {className?: string}) => {
    return (
        <span className={`${className} inline-block`}>
            <svg
            xmlns="http://www.w3.org/2000/svg"
            width="35"
            height="35"
            viewBox="0 0 24 24"
            className="fill-current"
            >
                <path d="M8.5,18l3.5,4l3.5-4H19c1.103,0,2-0.897,2-2V4c0-1.103-0.897-2-2-2H5C3.897,2,3,2.897,3,4v12c0,1.103,0.897,2,2,2H8.5z M7,7h10v2H7V7z M7,11h7v2H7V11z"></path>
            </svg>
        </span>
    )
}





export const IntroTitle = ({heading, description} : IntroTitleType) => {
    return (
        <>
        <div className="text-center">
            <h2 className="lg:text-[2.75rem] sm:text-[2.125rem] leading-[1.75] font-semibold mb-3">{heading}</h2>
            <p className="">{description}</p>
        </div>
        </>
    )
}

export const AuthFormSectionLayout = ({children, className} : {children: React.ReactNode, className?: string}) => {

    return (
        <>
        <div className={`w-full flex flex-col justify-center items-center 
        lg:h-full
        sm:h-auto ${className}`}>
            {children}
        </div>
        </>
    )
}


export const AuthFormLayout = ({onSubmit, children} : {onSubmit: FormEventHandler<HTMLFormElement>, children: React.ReactNode}) => {

    return (
        <>
        <div className="mt-10
        lg:w-1/3
        sm:w-2/3
        ">
            <form className="flex flex-col w-full" action="" onSubmit={onSubmit}>
                {children}
            </form>
        </div>
        </>
    )
}

export const EyeOpen = () => {
    return(
        <>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="w-6 h-6">
        <path d="M12 15a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z" />
        <path fillRule="evenodd" d="M1.323 11.447C2.811 6.976 7.028 3.75 12.001 3.75c4.97 0 9.185 3.223 10.675 7.69.12.362.12.752 0 1.113-1.487 4.471-5.705 7.697-10.677 7.697-4.97 0-9.186-3.223-10.675-7.69a1.762 1.762 0 0 1 0-1.113ZM17.25 12a5.25 5.25 0 1 1-10.5 0 5.25 5.25 0 0 1 10.5 0Z" clipRule="evenodd" />
        </svg>   
        </>
    )
}

export const EyeClose = () => {
    return(
        <>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="w-6 h-6">
        <path d="M3.53 2.47a.75.75 0 0 0-1.06 1.06l18 18a.75.75 0 1 0 1.06-1.06l-18-18ZM22.676 12.553a11.249 11.249 0 0 1-2.631 4.31l-3.099-3.099a5.25 5.25 0 0 0-6.71-6.71L7.759 4.577a11.217 11.217 0 0 1 4.242-.827c4.97 0 9.185 3.223 10.675 7.69.12.362.12.752 0 1.113Z" />
        <path d="M15.75 12c0 .18-.013.357-.037.53l-4.244-4.243A3.75 3.75 0 0 1 15.75 12ZM12.53 15.713l-4.243-4.244a3.75 3.75 0 0 0 4.244 4.243Z" />
        <path d="M6.75 12c0-.619.107-1.213.304-1.764l-3.1-3.1a11.25 11.25 0 0 0-2.63 4.31c-.12.362-.12.752 0 1.114 1.489 4.467 5.704 7.69 10.675 7.69 1.5 0 2.933-.294 4.242-.827l-2.477-2.477A5.25 5.25 0 0 1 6.75 12Z" />
        </svg>
        </>
    )
}

                                 
export const User = () => {
    return (
        <>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="w-full h-full">
        <path fillRule="evenodd" d="M7.5 6a4.5 4.5 0 1 1 9 0 4.5 4.5 0 0 1-9 0ZM3.751 20.105a8.25 8.25 0 0 1 16.498 0 .75.75 0 0 1-.437.695A18.683 18.683 0 0 1 12 22.5c-2.786 0-5.433-.608-7.812-1.7a.75.75 0 0 1-.437-.695Z" clipRule="evenodd" />
        </svg>
        </>
    )
}