import React, { useEffect, useState } from "react";
import "../styles/site-menu.scss";
import "../styles/tooltips.scss";
import avatarImg from "../assets/images/avatar-1.jpg";
import { Link, useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import UserService from "../services/userService";
import {
  MessageNotify,
  activeNotify,
} from "../state/reducer/mesageNotifiReducer";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { useSelector, useDispatch } from "react-redux";
import ClientService from "../services/clientService";
import { fetchUsers } from "../state/reducer/clientReducer";
import { set } from "react-hook-form";
import { connection } from "../Hubs/ConnectionHub";
import { setChats } from "../state/reducer/Hubs/chatsReducer";
import {
  activeShowMessage,
  deactiveShowMessage,
} from "../state/reducer/showMessageReducer";
import { logout } from "../state/actions/logout";
import { UrlHost } from "../services/http-common";
function SideMenu() {
  const dispatch: AppDispatch = useDispatch();
  const isShowMessageConversation = useSelector(
    (state: RootState) => state.showMessage.status
  );

  const openConversationMessage = () => {
    dispatch(activeShowMessage());
  };
  const closeConversationMessage = () => {
    dispatch(deactiveShowMessage());
  };
  const navigate = useNavigate();
  const [darkMode, setDarkMode] = useState(
    localStorage.getItem("dark-mode") === "enabled"
  );
  const enableDarkMode = () => {
    document.body.classList.add("dark-mode");
    localStorage.setItem("dark-mode", "enabled");
    setDarkMode(true);
  };

  const disableDarkMode = () => {
    document.body.classList.remove("dark-mode");
    localStorage.setItem("dark-mode", "disabled");
    setDarkMode(false);
  };

  const handleLogout = async () => {
    if (connection.state !== "Disconnected") {
      await connection.stop();
    }
    localStorage.clear();
    dispatch(logout());
    Cookies.remove("token");
    navigate("/auth/login");
  };
  useEffect(() => {
    const darkModeToggle = document.querySelector("#dark-mode-toggle");

    const toggleDarkMode = () => {
      if (!darkMode) {
        (darkModeToggle as HTMLElement).classList.add(
          "side-menu__dark-mode-active"
        );
        enableDarkMode();
      } else {
        (darkModeToggle as HTMLElement).classList.remove(
          "side-menu__dark-mode-active"
        );
        disableDarkMode();
      }
    };

    // if (darkMode) {
    //   enableDarkMode();
    // }
    if (darkModeToggle) {
      darkModeToggle.addEventListener("click", toggleDarkMode);
    }
    // if (darkModeToggle) {
    //   darkModeToggle.removeEventListener('click', toggleDarkMode);
    // }
    return () => {
      if (darkModeToggle) {
        darkModeToggle.removeEventListener("click", toggleDarkMode);
      }
    };
  }, [darkMode]);
  useEffect(() => {
    const elements = document.querySelectorAll(
      ".side-menu__nav-link-icon--tooltip"
    );
    elements.forEach((element) => {
      (element as HTMLElement).addEventListener("click", (event) => {
        elements.forEach((el) => {
          if (
            (el as HTMLElement).classList.contains(
              "side-menu__nav-link-icon--tooltip-effect"
            ) &&
            (el as HTMLElement).classList.contains("side-menu__pills--active")
          ) {
            (el as HTMLElement).classList.remove(
              "side-menu__nav-link-icon--tooltip-effect"
            );
            (el as HTMLElement).classList.remove("side-menu__pills--active");
          }
        });
        (element as HTMLElement).classList.add(
          "side-menu__nav-link-icon--tooltip-effect"
        );
        (element as HTMLElement).classList.add("side-menu__pills--active");
      });
    });
  });
  // const client = useSelector((state: RootState) => state.client.user);
  const { isAuthenticated } = useSelector((state: RootState) => state.auth);
  const { user } = useSelector((state: RootState) => state.client);
  const [isAuth, setIsAuth] = useState(false);
  useEffect(() => {
    setIsAuth(isAuthenticated);
    if (isAuth) {
      ClientService.GetDataUserInfomationView()
        .then((response) => {
          // response.data =
          dispatch(fetchUsers(response.data));
        })
        .catch((error) => {});
    }
  }, [isAuth]);
  const { chats } = useSelector((state: RootState) => state.chats);
  const handleGetConversation = async () => {
    if (connection.state === "Disconnected") {
      connection
        .start()
        .then(() => console.log("Connection established :)"))
    }
    connection.on("Connected", (response) => {
      const data = response.conversations;
      dispatch(setChats(data));
      // setConversations(response.conversations);
    });
  };
  const [isOpenDropDownAvt, setIsOpenDropDownAvt] = useState(true);

  const toggleOpenDropDownAvt = () => setIsOpenDropDownAvt(!isOpenDropDownAvt);
  return (
    <>
      <div className="side-menu">
        <div className="side-menu__navbar-brand-box">
          <Link to="/profile" className="side-menu__nav-item-logo">
            <span className="side-menu__logo-sm">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="30"
                height="30"
                viewBox="0 0 24 24"
                className="fill-current"
              >
                <path d="M8.5,18l3.5,4l3.5-4H19c1.103,0,2-0.897,2-2V4c0-1.103-0.897-2-2-2H5C3.897,2,3,2.897,3,4v12c0,1.103,0.897,2,2,2H8.5z M7,7h10v2H7V7z M7,11h7v2H7V11z"></path>
              </svg>
            </span>
          </Link>
        </div>
        <div className="side-menu__navigation">
          <ul>
            <li className="side-menu__nav-link side-menu__nav-link--profile">
              <div
                id="tooltip-profile"
                role="tooltip"
                className="tooltip-text absolute z-13 invisible inline-block px-4 py-4  rounded-lg shadow-sm opacity-0 tooltip "
              >
                Profile
                <div className="tooltip-arrow" data-popper-arrow></div>
              </div>

              <Link
                to="/profile"
                className="side-menu__nav-link-icon side-menu__pills side-menu__nav-link-icon--tooltip"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-6 h-6"
                  data-tooltip-target="tooltip-profile"
                  data-tooltip-placement="right"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                  />
                </svg>
              </Link>
            </li>
            <li
              className="side-menu__nav-link"
              onClick={() => handleGetConversation()}
            >
              <div
                id="tooltip-chats"
                role="tooltip"
                className="tooltip-text absolute z-13 invisible inline-block px-4 py-4  rounded-lg shadow-sm opacity-0 tooltip "
              >
                Chats
                <div className="tooltip-arrow" data-popper-arrow></div>
              </div>
              <Link
                to="/chats"
                className="side-menu__nav-link-icon side-menu__pills  side-menu__nav-link-icon--tooltip"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-6 h-6"
                  data-tooltip-target="tooltip-chats"
                  data-tooltip-placement="right"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M20.25 8.511c.884.284 1.5 1.128 1.5 2.097v4.286c0 1.136-.847 2.1-1.98 2.193-.34.027-.68.052-1.02.072v3.091l-3-3c-1.354 0-2.694-.055-4.02-.163a2.115 2.115 0 0 1-.825-.242m9.345-8.334a2.126 2.126 0 0 0-.476-.095 48.64 48.64 0 0 0-8.048 0c-1.131.094-1.976 1.057-1.976 2.192v4.286c0 .837.46 1.58 1.155 1.951m9.345-8.334V6.637c0-1.621-1.152-3.026-2.76-3.235A48.455 48.455 0 0 0 11.25 3c-2.115 0-4.198.137-6.24.402-1.608.209-2.76 1.614-2.76 3.235v6.226c0 1.621 1.152 3.026 2.76 3.235.577.075 1.157.14 1.74.194V21l4.155-4.155"
                  />
                </svg>
              </Link>
            </li>
            <li
              className="side-menu__nav-link"
              onClick={() => closeConversationMessage()}
            >
              <div
                id="tooltip-contacts"
                role="tooltip"
                className="tooltip-text absolute z-13 invisible inline-block px-4 py-4  rounded-lg shadow-sm opacity-0 tooltip "
              >
                Contacts
                <div className="tooltip-arrow" data-popper-arrow></div>
              </div>
              <Link
                to="/contacts"
                className="side-menu__nav-link-icon side-menu__pills side-menu__nav-link-icon--tooltip"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-6 h-6"
                  data-tooltip-target="tooltip-contacts"
                  data-tooltip-placement="right"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M15 19.128a9.38 9.38 0 0 0 2.625.372 9.337 9.337 0 0 0 4.121-.952 4.125 4.125 0 0 0-7.533-2.493M15 19.128v-.003c0-1.113-.285-2.16-.786-3.07M15 19.128v.106A12.318 12.318 0 0 1 8.624 21c-2.331 0-4.512-.645-6.374-1.766l-.001-.109a6.375 6.375 0 0 1 11.964-3.07M12 6.375a3.375 3.375 0 1 1-6.75 0 3.375 3.375 0 0 1 6.75 0Zm8.25 2.25a2.625 2.625 0 1 1-5.25 0 2.625 2.625 0 0 1 5.25 0Z"
                  />
                </svg>
              </Link>
            </li>
            <li
              className="side-menu__nav-link side-menu__nav-link--settings"
              onClick={() => closeConversationMessage()}
            >
              <div
                id="tooltip-settings"
                role="tooltip"
                className="tooltip-text absolute z-1 invisible inline-block px-4 py-4  rounded-lg shadow-sm opacity-0 tooltip "
              >
                Settings
                <div className="tooltip-arrow" data-popper-arrow></div>
              </div>
              <Link
                to="settings"
                className="side-menu__nav-link-icon side-menu__pills side-menu__nav-link-icon--tooltip"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-6 h-6"
                  data-tooltip-target="tooltip-settings"
                  data-tooltip-placement="right"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M9.594 3.94c.09-.542.56-.94 1.11-.94h2.593c.55 0 1.02.398 1.11.94l.213 1.281c.063.374.313.686.645.87.074.04.147.083.22.127.325.196.72.257 1.075.124l1.217-.456a1.125 1.125 0 0 1 1.37.49l1.296 2.247a1.125 1.125 0 0 1-.26 1.431l-1.003.827c-.293.241-.438.613-.43.992a7.723 7.723 0 0 1 0 .255c-.008.378.137.75.43.991l1.004.827c.424.35.534.955.26 1.43l-1.298 2.247a1.125 1.125 0 0 1-1.369.491l-1.217-.456c-.355-.133-.75-.072-1.076.124a6.47 6.47 0 0 1-.22.128c-.331.183-.581.495-.644.869l-.213 1.281c-.09.543-.56.94-1.11.94h-2.594c-.55 0-1.019-.398-1.11-.94l-.213-1.281c-.062-.374-.312-.686-.644-.87a6.52 6.52 0 0 1-.22-.127c-.325-.196-.72-.257-1.076-.124l-1.217.456a1.125 1.125 0 0 1-1.369-.49l-1.297-2.247a1.125 1.125 0 0 1 .26-1.431l1.004-.827c.292-.24.437-.613.43-.991a6.932 6.932 0 0 1 0-.255c.007-.38-.138-.751-.43-.992l-1.004-.827a1.125 1.125 0 0 1-.26-1.43l1.297-2.247a1.125 1.125 0 0 1 1.37-.491l1.216.456c.356.133.751.072 1.076-.124.072-.044.146-.086.22-.128.332-.183.582-.495.644-.869l.214-1.28Z"
                  />
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                  />
                </svg>
              </Link>
            </li>
            <li
              className="side-menu__nav-link side-menu__nav-link--notification"
              onClick={() => closeConversationMessage()}
            >
              <div
                id="tooltip-notification"
                role="tooltip"
                className="tooltip-text absolute z-1 invisible inline-block px-4 py-4  rounded-lg shadow-sm opacity-1 tooltip "
              >
                Notification
                <div className="tooltip-arrow" data-popper-arrow></div>
              </div>
              <Link
                to="notifications"
                className="side-menu__nav-link-icon side-menu__pills side-menu__nav-link-icon--tooltip"
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  fill="currentColor"
                  className="w-6 h-6"
                >
                  <path
                    fillRule="evenodd"
                    d="M5.25 9a6.75 6.75 0 0 1 13.5 0v.75c0 2.123.8 4.057 2.118 5.52a.75.75 0 0 1-.297 1.206c-1.544.57-3.16.99-4.831 1.243a3.75 3.75 0 1 1-7.48 0 24.585 24.585 0 0 1-4.831-1.244.75.75 0 0 1-.298-1.205A8.217 8.217 0 0 0 5.25 9.75V9Zm4.502 8.9a2.25 2.25 0 1 0 4.496 0 25.057 25.057 0 0 1-4.496 0Z"
                    clipRule="evenodd"
                  />
                </svg>
              </Link>
            </li>
            <li className="side-menu__nav-link  tooltip" id="dark-mode-toggle">
              <div className="side-menu__nav-link-icon side-menu__nav-link-icon--dark">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-6 h-6"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M21.752 15.002A9.72 9.72 0 0 1 18 15.75c-5.385 0-9.75-4.365-9.75-9.75 0-1.33.266-2.597.748-3.752A9.753 9.753 0 0 0 3 11.25C3 16.635 7.365 21 12.75 21a9.753 9.753 0 0 0 9.002-5.998Z"
                  />
                </svg>
              </div>
              <div className="side-menu__nav-link-icon side-menu__nav-link-icon--sun">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth={1.5}
                  stroke="currentColor"
                  className="w-6 h-6"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M12 3v2.25m6.364.386-1.591 1.591M21 12h-2.25m-.386 6.364-1.591-1.591M12 18.75V21m-4.773-4.227-1.591 1.591M5.25 12H3m4.227-4.773L5.636 5.636M15.75 12a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0Z"
                  />
                </svg>
              </div>
            </li>
            <li
              className="side-menu__nav-link side-menu__nav-link--avatar tooltip dropdown"
            >
              <div className="side-menu__nav-link-icon side-menu__nav-link-icon--avatar"
                onClick={toggleOpenDropDownAvt}
              >
                <div className="img-box">
                  <img src={`${UrlHost}${user?.urlImgAvatar}`} alt="" />
                </div>
              </div>
              <div
                className={isOpenDropDownAvt ? "dropdown-DropDownAvt" : "dropdown-DropDownAvt hidden dropdown-top"}
              >
                <ul
                  className="py-2 text-sm text-gray-700 dark:text-gray-200 side-menu__dropdown-list"
                >
                  <li>
                    <Link
                      to={"/profile"}
                      className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white flex items-center justify-between"
                    >
                      <p className="side-menu__title-item-dropdown">Profile</p>
                      <div className="side-menu__icon-item-dropdown">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          fill="none"
                          viewBox="0 0 24 24"
                          strokeWidth={1.5}
                          stroke="currentColor"
                          className="w-6 h-6"
                        >
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                          />
                        </svg>
                      </div>
                    </Link>
                  </li>
                  <li>
                    <Link
                      to={"/settings"}
                      className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white flex items-center justify-between"
                    >
                      <p className="side-menu__title-item-dropdown">Settings</p>
                      <div className="side-menu__icon-item-dropdown">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          fill="none"
                          viewBox="0 0 24 24"
                          strokeWidth={1.5}
                          stroke="currentColor"
                          className="w-6 h-6"
                        >
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M10.343 3.94c.09-.542.56-.94 1.11-.94h1.093c.55 0 1.02.398 1.11.94l.149.894c.07.424.384.764.78.93.398.164.855.142 1.205-.108l.737-.527a1.125 1.125 0 0 1 1.45.12l.773.774c.39.389.44 1.002.12 1.45l-.527.737c-.25.35-.272.806-.107 1.204.165.397.505.71.93.78l.893.15c.543.09.94.559.94 1.109v1.094c0 .55-.397 1.02-.94 1.11l-.894.149c-.424.07-.764.383-.929.78-.165.398-.143.854.107 1.204l.527.738c.32.447.269 1.06-.12 1.45l-.774.773a1.125 1.125 0 0 1-1.449.12l-.738-.527c-.35-.25-.806-.272-1.203-.107-.398.165-.71.505-.781.929l-.149.894c-.09.542-.56.94-1.11.94h-1.094c-.55 0-1.019-.398-1.11-.94l-.148-.894c-.071-.424-.384-.764-.781-.93-.398-.164-.854-.142-1.204.108l-.738.527c-.447.32-1.06.269-1.45-.12l-.773-.774a1.125 1.125 0 0 1-.12-1.45l.527-.737c.25-.35.272-.806.108-1.204-.165-.397-.506-.71-.93-.78l-.894-.15c-.542-.09-.94-.56-.94-1.109v-1.094c0-.55.398-1.02.94-1.11l.894-.149c.424-.07.765-.383.93-.78.165-.398.143-.854-.108-1.204l-.526-.738a1.125 1.125 0 0 1 .12-1.45l.773-.773a1.125 1.125 0 0 1 1.45-.12l.737.527c.35.25.807.272 1.204.107.397-.165.71-.505.78-.929l.15-.894Z"
                          />
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                          />
                        </svg>
                      </div>
                    </Link>
                  </li>
                  <li>
                    <Link
                      to={"/change-password"}
                      className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white flex items-center justify-between"
                    >
                      <p className="side-menu__title-item-dropdown">
                        Changes Password
                      </p>
                      <div className="side-menu__icon-item-dropdown">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          fill="none"
                          viewBox="0 0 24 24"
                          strokeWidth={1.5}
                          stroke="currentColor"
                          className="w-6 h-6"
                        >
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M16.5 10.5V6.75a4.5 4.5 0 1 0-9 0v3.75m-.75 11.25h10.5a2.25 2.25 0 0 0 2.25-2.25v-6.75a2.25 2.25 0 0 0-2.25-2.25H6.75a2.25 2.25 0 0 0-2.25 2.25v6.75a2.25 2.25 0 0 0 2.25 2.25Z"
                          />
                        </svg>
                      </div>
                    </Link>
                  </li>
                  <li>
                    <button
                      onClick={() => handleLogout()}
                      className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white flex items-center justify-between w-full"
                    >
                      <p className="side-menu__title-item-dropdown">Sign out</p>
                      <div className="side-menu__icon-item-dropdown">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          fill="none"
                          viewBox="0 0 24 24"
                          strokeWidth={1.5}
                          stroke="currentColor"
                          className="w-6 h-6"
                        >
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M8.25 9V5.25A2.25 2.25 0 0 1 10.5 3h6a2.25 2.25 0 0 1 2.25 2.25v13.5A2.25 2.25 0 0 1 16.5 21h-6a2.25 2.25 0 0 1-2.25-2.25V15m-3 0-3-3m0 0 3-3m-3 3H15"
                          />
                        </svg>
                      </div>
                    </button>
                  </li>
                </ul>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </>
  );
}

export default SideMenu;
