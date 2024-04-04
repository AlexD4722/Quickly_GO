import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Layout  from "../views/Layout";
import Profile from "../pages/Profile";
import Chats from "../pages/Chats";
import Settings from "../pages/Settings";
import Notification from "../pages/Notification";
import Contacts from "../pages/Contact";
import Auth from "../pages/auth/Auth";
import LogIn from "../pages/auth/Login";
import Register from "../pages/auth/Register";
import Logout from "../pages/auth/Logout";
import ResetPass from "../pages/auth/ResetPassword";
import VerifyEmail from "../pages/auth/EmailVerify";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        children: [
            {
                path: "/profile",
                element: <Profile />,
            },
            {
                path: "/chats",
                element: <Chats />,
            },
            {
                path: "/settings",
                element: <Settings />,
            },
            {
                path: "/contacts",
                element: <Contacts />,
            },
            {
                path: "/notifications",
                element: <Notification />,
            },

            // {
            //     path: "*",
            //     element: <NotFound />,
            // },
        ],
    },
    {
        path: "/auth",
        element: <Auth/>,
        children: [
            {
                id: 'index',
                path: 'login',
                element: <LogIn/>
            },
            {
                path: 'register',
                element: <Register/>
            },
            {
                path: 'logout',
                element: <Logout/>
            },
            {
                path: 'resetpassword',
                element: <ResetPass/>
            },
            {
                path: 'verifyemail',
                element: <VerifyEmail/>
            }
        ]
    }
]);
export default router;