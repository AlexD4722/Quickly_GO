import UserService, { FormRegister } from "../../services/userService";
import { useForm, SubmitHandler } from "react-hook-form";
import {
  Account,
  AuthFormLayout,
  AuthFormSectionLayout,
  EyeClose,
  EyeOpen,
  IntroTitle,
} from "./Auth";
import { HTMLInputTypeAttribute, useEffect, useRef, useState } from "react";
import Button from "../../components/authButton";
import { UseFormRegister, RegisterOptions, FieldErrors } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import AlertMessage from "../../components/AlertMessage";
import { useAppDispatch } from "../../state/hooks";
import { RootState } from "../../state/store/configureStore";
import {
  MessageNotify,
  activeNotify,
} from "../../state/reducer/mesageNotifiReducer";
import { clear } from "console";

interface FormValues extends Account {
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  repeatpass: string;
}

type name =
  | "email"
  | "password"
  | "userName"
  | "phoneNumber"
  | "repeatpass"
  | "firstName"
  | "lastName";

type FieldRegisterType = {
  name: name;
  type: HTMLInputTypeAttribute;
  register: UseFormRegister<FormValues>;
  registerOptions: RegisterOptions;
  errors: FieldErrors;
};

type ErrorsType = {
  name: name;
  errors: FieldErrors;
  message: string;
};

export default function Register() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const {
    register,
    handleSubmit,
    watch,
    reset,
    formState: { errors },
  } = useForm<FormValues>();

  const onSubmit: SubmitHandler<FormValues> = (data) => {
    let formRegister = new FormRegister(
      data.firstName,
      data.lastName,
      data.userName,
      data.email,
      data.phoneNumber,
      data.password
    );
    UserService.register(formRegister)
      .then((response) => {
        if (response.status === 201) {
          // save user id and email to local storage
          localStorage.setItem(
            "stringRequestConfirmEmail",
            response.data.data
          );
          navigate("/auth/verifyemail");
        }
      })
      .catch((e) => {
        if (e.response.status === 400) {
          const messageNotify: MessageNotify = {
            content: "Registration form failed",
            status: 0,
          };
          dispatch(activeNotify(messageNotify));
        }
      });

    // setIsSuccessfulRegisted(true);
    // setMessage(data);
    // setTimeout(() => {
    //   setIsSuccessfulRegisted(false);
    // }, 3000);

    reset({
      email: "",
      password: "",
      repeatpass: "",
    });
  };

  const pass = watch("password") as string;

  const [showPass, setShowPass] = useState(false);
  const [showRPass, setShowRPass] = useState(false);
  const [isSuccessfulRegisted, setIsSuccessfulRegisted] = useState(false);
  const [message, setMessage] = useState<any>();
  const [regex, setRegex] = useState<string>('');

  const revealPass = () => {
    let x = document.getElementById("password") as HTMLInputElement;
    if (x.type === "password") {
      x.type = "text";
      setShowPass(true);
    } else {
      x.type = "password";
      setShowPass(false);
    }
  };

  const revealRPass = () => {
    let x = document.getElementById("repeatpass") as HTMLInputElement;
    if (x.type === "password") {
      x.type = "text";
      setShowRPass(true);
    } else {
      x.type = "password";
      setShowRPass(false);
    }
  };

  useEffect(() => {
    if (isSuccessfulRegisted) {
      alert(JSON.stringify(message));
    }
  }, [isSuccessfulRegisted, message]);

  useEffect(() => {
    if(typeof watch('password') === 'string'){
      setRegex(watch('password'))
    }
  },[watch])
  
  // const users = useSelector((state: RootState) => state.messageNotify);
  return (
    <>
      <AuthFormSectionLayout className="py-24">
        <IntroTitle
          heading={"Register Account"}
          description={
            "Register your account and connect with friends worldwide."
          }
        />

        <AuthFormLayout onSubmit={handleSubmit(onSubmit)}>
          <div className="flex flex-col mb-1">
            <label htmlFor="firstName" className="font-semibold mb-1">
              First name
            </label>
            <Field
              register={register}
              errors={errors}
              name={"firstName"}
              type="text"
              registerOptions={{ required: true }}
            />
          </div>
          <Errors
            name="firstName"
            errors={errors}
            message="Please enter your first name!"
          />

          <div className="flex flex-col mb-1">
            <label htmlFor="lastName" className="font-semibold mb-1">
              Last name
            </label>
            <Field
              register={register}
              errors={errors}
              name={"lastName"}
              type="text"
              registerOptions={{ required: true }}
            />
          </div>
          <Errors
            name="lastName"
            errors={errors}
            message="Please enter your last name!"
          />
          <div className="flex flex-col mb-1">
            <label htmlFor="userName" className="font-semibold mb-1">
              User name
            </label>
            <Field
              register={register}
              errors={errors}
              name={"userName"}
              type="text"
              registerOptions={{ required: true }}
            />
          </div>
          <Errors
            name="userName"
            errors={errors}
            message="Please enter your user name!"
          />
          <div className="flex flex-col mb-1">
            <label htmlFor="phoneNumber" className="font-semibold mb-1">
              Your phone number
            </label>
            <Field
              register={register}
              errors={errors}
              name={"phoneNumber"}
              type="text"
              registerOptions={{ required: true }}
            />
          </div>
          <Errors
            name="userName"
            errors={errors}
            message="Please enter your user name!"
          />
          <div className="flex flex-col mb-1">
            <label htmlFor="email" className="font-semibold mb-1">
              Email
            </label>
            <Field
              register={register}
              errors={errors}
              name={"email"}
              type="text"
              registerOptions={{
                required: true,
                pattern:
                /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,
              }}
            />
          </div>
          <Errors name="email" errors={errors} message="Email must be valid (have @****.**** extension.)" />

          <div className="flex flex-col mt-5 relative">
            <label htmlFor="password" className="font-semibold mb-1">
              Password
            </label>        
            <Field
            type="password"
            register={register}
            registerOptions={{ required: true, pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/}}
            errors={errors}
            name={"password"}
            />
            <div
              className="absolute top-1/2 mt-1 right-5 hover:cursor-pointer hover:text-[#4eac6d]"
              onClick={revealPass}
            >
              {showPass ? <EyeOpen /> : <EyeClose />}
            </div>
          </div>
          {(errors.password) &&
          <div className="text-red-500">
            {!regex.match(/^[A-Za-z\d]{8,}$/) && <div className="my-1">Not enough 8 characters!</div>}
            {!regex.match(/(?=.*\d)/) && <div className="my-1">Must have at least 1 digit!</div>}
            {!regex.match(/(?=.*[a-z])/) && <div className="my-1">Must have at least 1 lowercase letter!</div>}
            {!regex.match(/(?=.*[A-Z])/) && <div className="my-1">Must have at least 1 uppercase letter!</div>}
          </div>}

          <div className="flex flex-col mt-5 relative">
            <label htmlFor="repeatpass" className="font-semibold mb-1">
              Repeat Password
            </label>
            <Field
              type="password"
              register={register}
              registerOptions={{
                required: true,
                pattern: new RegExp(`${pass}`),
              }}
              errors={errors}
              name={"repeatpass"}
            />

            <div
              className="absolute top-1/2 mt-1 right-5 hover:cursor-pointer hover:text-[#4eac6d]"
              onClick={revealRPass}
            >
              {showRPass ? <EyeOpen /> : <EyeClose />}
            </div>
          </div>
          <Errors
            name="repeatpass"
            errors={errors}
            message="Password not match!"
          />

          <div className="my-10">
            <span>By registering you agree to the QuicklyGo </span>{" "}
            <span className="text-[#4eac6d]">Terms of Use.</span>
          </div>

          <Button type={"submit"}>
            <span>Register</span>
          </Button>
        </AuthFormLayout>

        <div className="mt-10">
          <span>Already have an account ?</span>{" "}
          <a href="/auth/login" className="text-[#4eac6d] underline">
            Sign In
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
}: FieldRegisterType) => {
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
      {errors[`${name}`] && (
        <div className="text-red-500 mt-1 mb-2">{message}</div>
      )}
    </>
  );
};
