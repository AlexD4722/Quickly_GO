import Conversation from "../pages/Conversantion";
import SideMenu from "../pages/SideMenu";
import { Outlet } from "react-router";
import { useSelector } from "react-redux";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { useEffect, useState } from "react";
import Cookies from "js-cookie";
import UserService from "../services/userService";
import { useNavigate } from "react-router-dom";
import {
  connection,
  requestMessagesInConversation,
  startConnection,
} from "../Hubs/ConnectionHub";
import { useDispatch } from "react-redux";
import { getAllMessage } from "../state/reducer/Hubs/messageReducer";
import { Chat, setChats } from "../state/reducer/Hubs/chatsReducer";
import { setDataHeaderChat } from "../state/reducer/Hubs/headerChatReducer";
import { fetchFriend, Relationship } from "../state/reducer/Hubs/friendReducer";
import { fetchUserLookFor } from "../state/reducer/Hubs/lookforUserReducer";
import { get } from "http";

function Layout() {
  localStorage.setItem("showMessage", "false");
  const dispatch: AppDispatch = useDispatch();
  const navigate = useNavigate();
  const isAuth = useSelector((state: RootState) => state.auth.isAuthenticated);
  const [token, setToken] = useState("");
  const { dataHeaderChat } = useSelector(
    (state: RootState) => state.headerChat
  );
  const initializeApp = async () => {
    try {
      await startConnection();
      console.log("Connection started successfully.");
      // Now you can use the connection
    } catch (error) {
      console.error("Failed to start connection.", error);
      // Handle the error
    }
  };
  useEffect(() => {
    var tokenString = Cookies.get("token");
    if (tokenString) {
      setToken(tokenString);
      let objTokenstring = {
        tokenString: tokenString,
      };
      UserService.verifyToken(objTokenstring)
        .then((res) => {
          if (res.status === 200) {
            navigate("/profile");
          }
        })
        .catch((error) => {
          if (error.response.status === 401) {
            navigate("/auth/login");
          }
        });
    } else {
      navigate("/auth/login");
    }
  }, [token]);

  const chats: Chat[] = useSelector((state: RootState) => state.chats.chats);
  const friends: Relationship[] = useSelector(
    (state: RootState) => state.friendUser.relationships
  );
  useEffect(() => {
    if (connection.state === "Disconnected") {
      initializeApp();
    }
    connection.on("RefreshChats", (data) => {
      // Handle the event here
      const response = data.conversations;
      dispatch(setChats(response));
      // dispatch(getAllMessage(data.conversation.messages));
      //check dataHeaderChat is null
      if (dataHeaderChat && dataHeaderChat.id) {
        //compare data.conversation.conversationId with dataHeaderChat.id
        if (data.conversation.conversationId === dataHeaderChat.id) {
          fetchMessages(data.conversation.conversationId);
        }
      }
    });
    connection.on("Connected", (response) => {
      dispatch(fetchFriend(response.relationships));
      if (response.conversations) {
        console.log("Connected----", response.conversations);
        dispatch(setChats(response.conversations));
        getDataDefault(response.conversations);
        getDataHeaderChat(response.conversations);
      }
    });
    // this is not unnecessary
    connection.on("RefreshRelationShip", (data) => {
      dispatch(fetchFriend(data));
    });
    connection.on("ReceiveFriendRequest", (data) => {
      dispatch(fetchFriend(data.relationships));
    });
  }, []);
  useEffect(() => {
    const handleRefreshConversation = async (data: any) => {
      console.log(data.conversationId, dataHeaderChat);
      console.log("-------------------------------------");
      if (dataHeaderChat?.id && data.conversationId === dataHeaderChat.id) {
        const listMessageConversation = await requestMessagesInConversation(
          data.conversationId
        );
        if (listMessageConversation?.conversation) {
          listMessageConversation.conversation.messages.sort(
            (a:any, b:any) => a.id - b.id
          );
          dispatch(getAllMessage(listMessageConversation));
        }
      }
    };

    connection.on("RefreshConversation", handleRefreshConversation);

    return () => {
      connection.off("RefreshConversation", handleRefreshConversation);
    };
  }, [dataHeaderChat]);
  const fetchMessages = async (id: string) => {
    let listMessageConversation: any;
    listMessageConversation = await requestMessagesInConversation(id);
    if (listMessageConversation && listMessageConversation.conversation) {
      listMessageConversation.conversation.messages.sort(
        (a: any, b: any) => a.id - b.id
      );
    }
    dispatch(getAllMessage(listMessageConversation));
  };
  const getDataDefault = async (data: any) => {
    let firstChatId: string = "";
    if (data && data[0]) {
      firstChatId = data[0].id;
      sessionStorage.setItem("checkGroup", data[0].group);
    }
    fetchMessages(firstChatId);
  };
  const getDataHeaderChat = async (data: any) => {
    var dataHeader = {
      id: "",
      name: "",
      urlImg: "",
    };
    if (data && data[0] && data.length > 0) {
      if (data[0].group) {
        dataHeader.id = data[0].id;
        dataHeader.name = data[0].name;
        dataHeader.urlImg = data[0].urlImg;
      } else {
        dataHeader.id = data[0].id;
        dataHeader.name =
          data[0].user[0].firstName + " " + data[0].user[0].lastName;
        dataHeader.urlImg = data[0].user[0].urlImgAvatar;
      }
      dispatch(setDataHeaderChat(dataHeader));
    }
  };
  return (
    <div>
      {isAuth && (
        <>
          <SideMenu />
          <Conversation />
          <Outlet />
        </>
      )}
    </div>
  );
}
export default Layout;
