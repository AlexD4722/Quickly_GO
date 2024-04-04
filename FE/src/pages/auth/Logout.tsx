import Button from "../../components/authButton";
import { AuthFormSectionLayout, IntroTitle, User } from "./Auth";

export default function Logout () {
    return (
        <>
            <AuthFormSectionLayout>
                <div className="mt-10
                lg:w-1/3
                sm:w-2/3
                ">
                    <div className="flex justify-center items-center mb-14">
                        <div className="text-[#4eac6d] bg-green-200 rounded-full">
                            <div className="m-10 w-16 h-16"><User/></div>
                        </div>
                    </div>
                    <IntroTitle heading={'You are logged out!'} description={'Thanks for joining us.'} />

                    <div className="p-2 my-6"></div>

                    <Button type="button">
                        <a className="inline-block w-full h-full" href="/auth/login">Sign In</a>
                    </Button>
                </div>
            </AuthFormSectionLayout>
        </>
    )
}