import {
  useForm,
  SubmitHandler,
  UseFormRegister,
  RegisterOptions,
  FieldErrors,
} from "react-hook-form";
import { AuthFormLayout, AuthFormSectionLayout, IntroTitle } from "./Auth";
import { useEffect, useState } from "react";
import Button from "../../components/authButton";
import { HTMLInputTypeAttribute } from "react";
import { useNavigate } from "react-router-dom";
import UserService, { ComfirmEmail } from "../../services/userService";
import {
  MessageNotify,
  activeNotify,
} from "../../state/reducer/mesageNotifiReducer";
import { useAppDispatch } from "../../state/hooks";

interface LoginFormValues {
  verifyCode: string;
}

type name = "verifyCode";

type FieldLoginType = {
  name: name;
  type: HTMLInputTypeAttribute;
  register: UseFormRegister<LoginFormValues>;
  registerOptions: RegisterOptions;
  errors: FieldErrors;
};

type ErrorsType = {
  name: name;
  errors: FieldErrors;
  message: string;
};

export default function VerifyEmail() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [isVerify, setIsVerify] = useState(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormValues>();

  const onSubmit: SubmitHandler<LoginFormValues> = (data) => {
    const { verifyCode } = data;
    // get data from local storage
    const stringRequest = localStorage.getItem("stringRequestConfirmEmail");
    if (stringRequest) {
      var objComfirm = new ComfirmEmail(stringRequest, verifyCode);
      UserService.confirm2FaByEmail(objComfirm)
        .then((res) => {
          if(res.status === 200){
            setIsVerify(true);
            const messageNotify: MessageNotify = {
              content: "You have completed account registration",
              status: 1,
            };
            dispatch(activeNotify(messageNotify));
            localStorage.removeItem("stringRequestConfirmEmail");
          }else{
            const messageNotify: MessageNotify = {
              content: "Authentication code failed",
              status: 0,
            };
            dispatch(activeNotify(messageNotify));
            localStorage.removeItem("stringRequestConfirmEmail");
          }
        })
        .catch((err) => {
          const messageNotify: MessageNotify = {
            content: "Authentication code failed",
            status: 0,
          };
          dispatch(activeNotify(messageNotify));
        });
      //remove data from local storage
    }
    //call api
  };

  useEffect(() => {
    if (isVerify) {
      navigate("/auth/login");
    }
  }, [isVerify, navigate]);

  return (
    <>
      <AuthFormSectionLayout>
        <IntroTitle
          heading={"Email Verification"}
          description={"Verify your email."}
        />

        <AuthFormLayout onSubmit={handleSubmit(onSubmit)}>
          <div className="flex flex-col">
            <label htmlFor="verifyCode" className="font-semibold mb-1">
              Verify code
            </label>
            <Field
              register={register}
              errors={errors}
              name={"verifyCode"}
              type="text"
              registerOptions={{ required: true, pattern: /^\S{6}$/ }}
            />
          </div>
          <Errors
            name="verifyCode"
            errors={errors}
            message="Verify code must at least 6 characters!"
          />

          <div className="w-[1px] py-5"></div>
          <Button type={"submit"}>
            <span>Submit</span>
          </Button>
        </AuthFormLayout>

        <div className="mt-10">
          <span>Not receive verify code yet?</span>{" "}
          <a href="/" className="text-[#4eac6d] underline">
            Resend email?!
          </a>
        </div>
      </AuthFormSectionLayout>
    </>
  );
}

export const Field = ({
  register,
  errors,
  name,
  type,
  registerOptions,
}: FieldLoginType) => {
  return (
    <>
      <input
        type={type}
        id={name}
        autoComplete={"off"}
        className={`rounded-md p-3 text-2xl border-gray-300 ${
          errors[`${name}`]
            ? "border-red-500 focus:border-red-500"
            : "focus:border-black"
        } `}
        {...register(name, registerOptions)}
      />
    </>
  );
};

export const Errors = ({ errors, name, message }: ErrorsType) => {
  return (
    <>
      {errors[`${name}`] && <div className="text-red-500 mt-2">{message}</div>}
    </>
  );
};
