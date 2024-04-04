import { useState, useEffect, useRef } from "react";
import "../styles/conversation.scss";
import { Form } from "react-router-dom";
import {
  CreateConversation,
  sendMessageInConversation,
} from "../Hubs/ConnectionHub";
import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { getAllMessage } from "../state/reducer/Hubs/messageReducer";
import Cookies from "js-cookie";
import { JwtPayload, jwtDecode } from "jwt-decode";
import { Chat} from "../state/reducer/Hubs/chatsReducer";
import {
  activeShowMessage,
  deactiveShowMessage,
} from "../state/reducer/showMessageReducer";
import { UrlHost } from "../services/http-common";
import AlertEmpty from "../components/Alert-empty";
import { group } from "console";

interface MyJwtPayload extends JwtPayload {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"?: string[];
}

const Conversation = () => {
  const [isLoading, setIsLoading] = useState(true);
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
  const [flag, setFlag] = useState(false);
  const token = Cookies.get("token");
  let stringClientId: any;
  if (token) {
    if (token) {
      const decoded: MyJwtPayload = jwtDecode(token);
      stringClientId =
        decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ];
    }
  }
  const messages = useSelector((state: RootState) =>
    state.messageConversation ? state.messageConversation.messages : []
  );
  const [messageSend, setMessageSend] = useState("");
  const handleInputChange = (event: any) => {
    setMessageSend(event.target.value);
  };

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    const checkGroup = sessionStorage.getItem("checkGroup");
    if (checkGroup === "true") {
      if (messages && messages.conversation) {
        let data: any = {
          CreatorId: stringClientId,
          conversationId: messages.conversation.conversationId,
          bodyContent: messageSend,
        };
        const listMessageConversation: any = await sendMessageInConversation(
          data
        );
        if (
          listMessageConversation &&
          listMessageConversation.conversation &&
          listMessageConversation.conversation.messages
        ) {
          listMessageConversation.conversation.messages.sort(
            (a: any, b: any) => a.id - b.id
          );
          dispatch(getAllMessage(listMessageConversation));
        }
        setMessageSend("");
      }
    } else {
      if (Array.isArray(messages) && messages.length === 0) {
        let response = (await CreateConversation({
          Name: "",
          Description: "",
          group: false,
          UserIds: [dataHeaderChat?.id],
        })) as { success: boolean; conversationId: string };
        if (response.success) {
          if (messageSend && response.conversationId) {
            let data: any = {
              CreatorId: stringClientId,
              conversationId: response.conversationId,
              bodyContent: messageSend,
            };
            const listMessageConversation: any =
              await sendMessageInConversation(data);
            listMessageConversation.conversation.messages.sort(
              (a: any, b: any) => a.id - b.id
            );
            dispatch(getAllMessage(listMessageConversation));
          }
          setMessageSend(""); // Clear the input field
        }
      } else {
        if (messageSend && messages?.conversation?.conversationId) {
          let data: any = {
            CreatorId: stringClientId,
            conversationId: messages.conversation.conversationId,
            bodyContent: messageSend,
          };
          const listMessageConversation: any = await sendMessageInConversation(
            data
          );
          listMessageConversation.conversation.messages.sort(
            (a: any, b: any) => a.id - b.id
          );
          dispatch(getAllMessage(listMessageConversation));
        }
        setMessageSend(""); // Clear the input field
      }
    }
  };
  const { dataHeaderChat } = useSelector(
    (state: RootState) => state.headerChat
  );
  const chats: Chat[] = useSelector((state: RootState) => state.chats.chats);
  const { isAuthenticated } = useSelector((state: RootState) => state.auth);
  const [isAuth, setIsAuth] = useState(false);
  const messagesEndRef = useRef<HTMLDivElement>(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };
  useEffect(scrollToBottom, [messages, flag]);
  return (
    <>
      {(chats && chats.length > 0) || dataHeaderChat ? (
        <div
          className={`conversation__wrapper ${
            isShowMessageConversation ? "conversation__wrapper--show" : ""
          }`}
        >
          <div className="conversation__header-chat">
            <div className="conversation__header-user">
              <div className="conversation__header-user-img">
                <div className="img-box">
                  <img
                    src={`${UrlHost}${
                      dataHeaderChat ? dataHeaderChat.urlImg : ""
                    }`}
                    alt=""
                  />
                </div>
              </div>
              <div className="conversation__header-user-body">
                <p className="conversation__name-user">
                  {dataHeaderChat ? dataHeaderChat.name : "App User"}
                </p>
                <p className="conversation__status-user">Online</p>
              </div>
            </div>
            <div className="conversation__header-option-tools">
              <div>
                <button
                  onClick={() => closeConversationMessage()}
                  type="button"
                  className="conversation__header-btn conversation__header-btn--close"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="currentColor"
                    className="w-10 h-10"
                  >
                    <path
                      fillRule="evenodd"
                      d="M16.5 3.75a1.5 1.5 0 0 1 1.5 1.5v13.5a1.5 1.5 0 0 1-1.5 1.5h-6a1.5 1.5 0 0 1-1.5-1.5V15a.75.75 0 0 0-1.5 0v3.75a3 3 0 0 0 3 3h6a3 3 0 0 0 3-3V5.25a3 3 0 0 0-3-3h-6a3 3 0 0 0-3 3V9A.75.75 0 1 0 9 9V5.25a1.5 1.5 0 0 1 1.5-1.5h6ZM5.78 8.47a.75.75 0 0 0-1.06 0l-3 3a.75.75 0 0 0 0 1.06l3 3a.75.75 0 0 0 1.06-1.06l-1.72-1.72H15a.75.75 0 0 0 0-1.5H4.06l1.72-1.72a.75.75 0 0 0 0-1.06Z"
                      clipRule="evenodd"
                    />
                  </svg>
                </button>
              </div>
            </div>
          </div>
          <div className="conversation__body-content-chat-wrapper">
            <ul className="conversation__list-chat">
              {messages &&
                messages.conversation &&
                messages.conversation.messages.map(
                  (msg: any, index: number) => {
                    let isSameUserAsPrevious: boolean = 
                      index > 0 &&
                      messages.conversation.messages[index - 1] &&
                      msg &&
                      messages.conversation.messages[index - 1].creatorId === msg.creatorId;
                    return (
                      <li
                        key={index}
                        className={`conversation__item-chat-wrapper ${
                          msg.creatorId === stringClientId
                            ? "conversation__item-right"
                            : "conversation__item-left"
                        }`}
                      >
                        {!isSameUserAsPrevious && (
                          <div className="conversation__item-image">
                            <div className="img-box">
                              <img
                                src={`${UrlHost}${msg.creator?.urlImgAvatar}`}
                                alt=""
                              />
                            </div>
                          </div>
                        )}
                        <div className="conversation__item-chat">
                          {!isSameUserAsPrevious && (
                            <div>
                              <h5 className="conversation__name-item-chat">
                                {msg.creatorId === stringClientId
                                  ? "You"
                                  : `${msg.creator?.firstName} ${msg.creator?.lastName}`}
                              </h5>
                            </div>
                          )}
                          <div className="conversation__item-chat-content">
                            <p>{msg.bodyContent}</p>
                            <button className="conversation__btn-item-option">
                              <svg
                                xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 24 24"
                                fill="currentColor"
                                className="w-7 h-7"
                              >
                                <path
                                  fillRule="evenodd"
                                  d="M10.5 6a1.5 1.5 0 1 1 3 0 1.5 1.5 0 0 1-3 0Zm0 6a1.5 1.5 0 1 1 3 0 1.5 1.5 0 0 1-3 0Zm0 6a1.5 1.5 0 1 1 3 0 1.5 1.5 0 0 1-3 0Z"
                                  clipRule="evenodd"
                                />
                              </svg>
                            </button>
                          </div>
                          {/* <p className="conversation__item-chat-time">11 ago</p> */}
                        </div>
                      </li>
                    );
                  }
                )}
              <div ref={messagesEndRef} />
            </ul>
          </div>
          <div className="conversation__wrapper-input">
            <Form
              className="conversation__form-input-message"
              onSubmit={handleSubmit}
            >
              <input
                type="text"
                name="boyMessage"
                id=""
                placeholder="Type your message..."
                className="conversation__input-message-detail"
                value={messageSend}
                onChange={handleInputChange}
              />
              <button className="conversation__btn-send-message">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 24 24"
                  fill="currentColor"
                  className="w-8 h-8"
                >
                  <path d="M3.478 2.404a.75.75 0 0 0-.926.941l2.432 7.905H13.5a.75.75 0 0 1 0 1.5H4.984l-2.432 7.905a.75.75 0 0 0 .926.94 60.519 60.519 0 0 0 18.445-8.986.75.75 0 0 0 0-1.218A60.517 60.517 0 0 0 3.478 2.404Z" />
                </svg>
              </button>
            </Form>
          </div>
        </div>
      ) : (
        <AlertEmpty
          message={"You don't have any conversations!"}
          classAdd={""}
        />
      )}
    </>
  );
};
export default Conversation;
