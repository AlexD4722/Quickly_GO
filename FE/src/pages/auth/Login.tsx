import {
  useForm,
  SubmitHandler,
  UseFormRegister,
  RegisterOptions,
  FieldErrors,
} from "react-hook-form";
import {
  Account,
  AuthFormLayout,
  AuthFormSectionLayout,
  EyeClose,
  EyeOpen,
  IntroTitle,
} from "./Auth";
import { useEffect, useRef, useState } from "react";
import Button from "../../components/authButton";
import { HTMLInputTypeAttribute } from "react";
import { logIn, setFalseAuth } from "../../state/reducer/authReducer";
import { AppDispatch, RootState } from "../../state/store/configureStore";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import {
  MessageNotify,
  activeNotify,
} from "../../state/reducer/mesageNotifiReducer";
import Cookies from "js-cookie";
import UserService, { objTokeString } from "../../services/userService";
import verifyToken from "../../services/userService";

// interface LoginFormValues extends Account {
//     savepass: string
// }
interface LoginFormValues {
  userName: string;
  password: string;
  savepass: string;
}

type name = "userName" | "password" | "savepass";

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

export default function LogIn() {
  const dispatch: AppDispatch = useDispatch();
  const navigate = useNavigate();
  // check token 
  const [token, setToken] = useState("");
  useEffect(() => {
    var tokenString = Cookies.get("token");
    if (tokenString) {
      setToken(tokenString);
      let objTokenString = {
        tokenString: tokenString,
      };
      UserService.verifyToken(objTokenString)
        .then((res) => {
          if (res.status === 200) {
            navigate("/profile");
          }
        })
        .catch((error) => {
          if (error.response.status === 401) {
            const messageNotify: MessageNotify = {
              content: "Please login again",
              status: 0,
            };
            dispatch(setFalseAuth());
            dispatch(activeNotify(messageNotify));
            navigate("/auth/login");
          }
        });
    }else{
      dispatch(setFalseAuth());
    }
  }, [token]);

  // handle login
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormValues>();
  interface Payload {
    data: {
      token: string;
      refreshToken: string;
    };
    status: number;
  }
  const onSubmit: SubmitHandler<LoginFormValues> = async (data) => {
    const { userName, password, savepass } = data;
   
    dispatch(logIn({ userName, password }))
      .then((response) => {
        const payload = response.payload as Payload;
        if (payload.status === 200) {
          Cookies.set("token", payload.data.token, { secure: true });
          navigate("/profile");
        } else {
          const messageNotify: MessageNotify = {
            content: "Login failed",
            status: 0,
          };
          dispatch(activeNotify(messageNotify));
        }
      })
      .catch((error) => {
        const messageNotify: MessageNotify = {
          content: "Login failed",
          status: 0,
        };
        dispatch(activeNotify(messageNotify));
      });
  };
  const [showPass, setShowPass] = useState(false);

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

  //   useEffect(() => {
  //     if (isAuthenticated) {
  //       const messageNotify: MessageNotify = {
  //         content: "Logged in successfully",
  //         status: 1,
  //       };
  //       dispatch(activeNotify(messageNotify));
  //       navigate("/profile");
  //     }
  //   }, [isAuthenticated, navigate]);

  return (
    <>
      <AuthFormSectionLayout>
        <IntroTitle
          heading={"Welcome Back!"}
          description={"Sign in to continue to QuicklyGo."}
        />

        <AuthFormLayout onSubmit={handleSubmit(onSubmit)}>
          <div className="flex flex-col">
            <label htmlFor="email" className="font-semibold mb-1">
              User Name
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
            message="Email is required!"
          />

          <div className="flex flex-col mt-5 relative">
            <div className="flex justify-between mb-1">
              <label htmlFor="password" className="font-semibold">
                Password
              </label>
              {/* <a
                href="/auth/resetpassword"
                className="password opacity-80 hover:cursor-pointer"
              >
                Forget password ?
              </a> */}
            </div>
            <Field
              type="password"
              register={register}
              registerOptions={{ required: true }}
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
          <Errors
            name="password"
            errors={errors}
            message="Password is required!"
          />

          <div className="mt-5 mb-10">
            <div className="flex items-center">
              <input
                type="checkbox"
                className=" checked:bg-[#4eac6d] w-6 h-6 rounded border-gray-400"
                id="save-pass"
                {...register("savepass")}
              />
              <label
                className="ml-2 mt-1 leading-[1] font-semibold"
                htmlFor="save-pass"
              >
                Remember me
              </label>
            </div>
          </div>

          <Button type={"submit"}>
            <span>Sign In</span>
          </Button>
        </AuthFormLayout>

        <div className="mt-10">
          <span>Don't have an account ?</span>{" "}
          <a href="/auth/register" className="text-[#4eac6d] underline">
            Register
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
