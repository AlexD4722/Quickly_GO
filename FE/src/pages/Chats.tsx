import { useEffect, useState } from "react";
import "../styles/tab-content.scss";
import "../styles/button-add.scss";
import BoxSearch from "../components/BoxSearch";
import InboxContact from "../components/inboxContact";
import "../styles/inbox-contact.scss";
import {
  CreateConversation,
  GetConversationData,
  LookForUser,
  requestMessagesInConversation,
} from "../Hubs/ConnectionHub";
import { useDispatch } from "react-redux";
import { setChats, User } from "../state/reducer/Hubs/chatsReducer";
import { useSelector } from "react-redux";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { getAllMessage } from "../state/reducer/Hubs/messageReducer";
import { setDataHeaderChat } from "../state/reducer/Hubs/headerChatReducer";
import {
  activeShowMessage,
  deactiveShowMessage,
} from "../state/reducer/showMessageReducer";
import AlertEmpty from "../components/Alert-empty";
import { Button, Modal } from "flowbite-react";
import { UrlHost } from "../services/http-common";
import { Relationship } from "../state/reducer/Hubs/friendReducer";
import { getMessage } from "@reduxjs/toolkit/dist/actionCreatorInvariantMiddleware";
import { Dropdown } from "flowbite-react";
import {
  activeNotify,
  MessageNotify,
} from "../state/reducer/mesageNotifiReducer";
import { set } from "react-hook-form";

//interface for user
interface UserGroup {
  id: string;
  firstName: string;
  lastName: string;
  urlImgAvatar: string;
  urlBackground: string;
}
function Chats() {
  const dispatch: AppDispatch = useDispatch();
  const [openModal, setOpenModal] = useState(false);
  const { chats } = useSelector((state: RootState) => state.chats);
  const [keySearch, setKeySearch] = useState<string>("");
  const [idFriend, setIdfFriend] = useState("");
  const [dataLookForUser, setDataLookForUser] = useState<any[]>([]);
  const friends: Relationship[] = useSelector(
    (state: RootState) => state.friendUser.relationships
  );
  const [selectedItemsGroup, setSelectedItemsGroup] = useState<User[]>([]);
  const [openModalGroup, setOpenModalGroup] = useState(false);
  //handle search data chat
  const [filteredChats, setFilteredChats] = useState(chats);
  const [dataDemo, setDataDemo] = useState<any[]>([]);
  useEffect(() => {
    console.log("run");
    setFilteredChats(chats);
  }, [chats]);
  const handleSearchChats = (searchTerm: string) => {
    //check if search term is empty
    if (searchTerm === "") {
      setFilteredChats(chats);
    } else {
      //check chat is empty
      if (chats.length === 0) {
        setFilteredChats([]);
      } else {
        setFilteredChats(
          chats.filter((chat) =>
            chat.name.toLowerCase().startsWith(searchTerm.toLowerCase())
          )
        );
      }
    }
  };
  useEffect(() => {
    const elements = document.querySelectorAll(".tab-content__inbox-contact");

    elements.forEach((element) => {
      (element as HTMLElement).addEventListener("click", (event) => {
        elements.forEach((el) => {
          if (
            (el as HTMLElement).classList.contains(
              "tab-content__inbox-contact--effect"
            )
          ) {
            (el as HTMLElement).classList.remove(
              "tab-content__inbox-contact--effect"
            );
          }
        });
        (element as HTMLElement).classList.add(
          "tab-content__inbox-contact--effect"
        );
      });
    });
  });
  const { dataHeaderChat } = useSelector(
    (state: RootState) => state.headerChat
  );
  const handleConversationClick = async (item: any) => {
    const listMessageConversation: any = await requestMessagesInConversation(
      item.id
    );
    listMessageConversation.conversation.messages.sort(
      (a: any, b: any) => a.id - b.id
    );
    dispatch(getAllMessage(listMessageConversation));
    var dataHeader = {
      id: "",
      name: "",
      urlImg: "",
    };

    if (item.group) {
      dataHeader.id = item.id;
      dataHeader.name = item.name;
      dataHeader.urlImg = item.urlImg;
    } else {
      dataHeader.id = item.id;
      dataHeader.name = item.user[0].firstName + " " + item.user[0].lastName;
      dataHeader.urlImg = item.user[0].urlImgAvatar;
    }

    sessionStorage.setItem("checkGroup", item.group);
    dispatch(setDataHeaderChat(dataHeader));
    dispatch(activeShowMessage());
  };
  const getDataLookFor = async (key: string) => {
    let result: any = await LookForUser(key);
    const friendIds = friends
      .filter((friend) => friend.status === 1)
      .map((friend) => friend.friendId);
    const makeFriendIds = friends
      .filter((friend) => friend.status === 2 || friend.status === 4)
      .map((friend) => friend.friendId);
    const acceptFriendIds = friends
      .filter((friend) => friend.status === 3)
      .map((friend) => friend.friendId);
    if (result && result.success) {
      if (Array.isArray(result.users)) {
        const mappedUsers = result.users.map((user: any) => {
          const isFriend = friendIds.includes(user.id);
          const isMakeFriend = makeFriendIds.includes(user.id);
          const isAcceptFriend = acceptFriendIds.includes(user.id);
          return {
            ...user,
            isFriend: isFriend,
            isMakeFriend: isMakeFriend,
            isAcceptFriend: isAcceptFriend,
          };
        });
        setDataLookForUser(mappedUsers);
      }
    }
  };
  useEffect(() => {
    getDataLookFor(keySearch);
  }, [keySearch]);
  const handleLookForAccount = (event: any) => {
    setKeySearch(event.target.value);
  };
  const handleCloseModal = () => {
    setIdfFriend("");
    setOpenModal(false);
    setOpenModalGroup(false);
  };
  const getMessages = async (id: string) => {
    const listMessageConversation: any = await requestMessagesInConversation(
      id
    );
    listMessageConversation.conversation.messages.sort(
      (a: any, b: any) => a.id - b.id
    );
    dispatch(getAllMessage(listMessageConversation));
  };
  

  const handleChooseUserChat = (item: any) => {
    var dataHeader = {
      id: item.id,
      name: item.firstName + " " + item.lastName,
      urlImg: item.urlImgAvatar,
    };
    const userIds = chats
      .filter((item) => item.group === false)
      .map((item) => item.user[0].id);
    if (userIds.includes(item.id)) {
      const conversationIds = chats
        .filter((item) => item.user && item.user[0] && item.user[0].id) // Filter out items without a user or user id
        .map((item) => item.id); // Map to the conversation id
      getMessages(conversationIds[0]);
    } else {
      dispatch(getAllMessage([]));
      // userIds does not contain item.id
    }
    dispatch(setDataHeaderChat(dataHeader));
    setOpenModal(false);
  };

  const cancelCreateGroupChat = () => {
    setSelectedItemsGroup([]);
    setOpenModalGroup(false);
  };
  const handleChooseUserChatGroup = (item: User) => {
    if (selectedItemsGroup.some((user) => user.id === item.id)) {
      setSelectedItemsGroup(selectedItemsGroup.filter((i) => i !== item));
    } else {
      setSelectedItemsGroup([...selectedItemsGroup, item]);
    }
  };
  const { user } = useSelector((state: RootState) => state.client);
  //handle create group chats
  const createGroupChat = async () => {
    const userIds = selectedItemsGroup.map((user) => user.id);
    if (user && user.id) {
      userIds.push(user.id);
    }
    const names = selectedItemsGroup.map(
      (user) => `${user.firstName} ${user.lastName}`
    );
    if (user && user.firstName && user.lastName) {
      names.unshift(`${user.firstName} ${user.lastName}`);
    }
    const groupName = names.join(", ");
    let response = (await CreateConversation({
      Name: groupName,
      Description: "",
      group: true,
      UserIds: userIds,
    })) as { success: boolean; conversationId: string };
    if (response.success) {
      const listMessageConversation: any = await requestMessagesInConversation(
        response.conversationId
      );
      listMessageConversation.conversation.messages.sort(
        (a: any, b: any) => a.id - b.id
      );
      dispatch(getAllMessage(listMessageConversation));
      let result = await GetConversationData(response.conversationId);
      var dataHeader = {
        id: result.conversation.id,
        name: result.conversation.name,
        urlImg: result.conversation.urlImg,
      };
      sessionStorage.setItem("checkGroup", result.conversation.group);
      dispatch(setDataHeaderChat(dataHeader));
      const messageNotify: MessageNotify = {
        content: "Added group chat successfully",
        status: 1,
      };
      setSelectedItemsGroup([]);
      dispatch(activeNotify(messageNotify));
      setOpenModalGroup(false);
    } else {
      const messageNotify: MessageNotify = {
        content: "Added group chat failed",
        status: 0,
      };
      setSelectedItemsGroup([]);
      dispatch(activeNotify(messageNotify));
      setOpenModalGroup(false);
    }
  };
  return (
    <>
      <div className="tab-content">
        <div className="tab-content__header">
          <div className="tab-content__flex tab-content__flex--header-title">
            <div className="tab-content__title">
              <h4>Chats</h4>
            </div>
            <div className="tab-content__btn-option-add">
              <Dropdown label="Add" placement="bottom">
                <Dropdown.Item onClick={() => setOpenModal(true)}>
                  New chat
                </Dropdown.Item>
                <Dropdown.Item onClick={() => setOpenModalGroup(true)}>
                  New group chat
                </Dropdown.Item>
              </Dropdown>
            </div>
          </div>
          <BoxSearch onSearch={handleSearchChats} />
        </div>
        <div></div>
        {filteredChats && filteredChats.length > 0 ? (
          <div>
            <div className="tab-content__body">
              <div className="tab-content__simple-bar">
                <div className="tab-content__simple-bar-body">
                  <ul>
                    {Array.isArray(filteredChats) &&
                      filteredChats &&
                      filteredChats.map((item: any, index: number) => (
                        <li
                          onClick={() => handleConversationClick(item)}
                          key={index}
                          className="tab-content__inbox-contact"
                        >
                          <InboxContact data={item} />
                        </li>
                      ))}
                  </ul>
                </div>
              </div>
            </div>
          </div>
        ) : (
          <AlertEmpty
            message={"Empty"}
            classAdd={"conversation__wrapper--tab-menu"}
          />
        )}
      </div>
      <Modal
        dismissible
        show={openModal}
        onClose={handleCloseModal}
        className="contract-form"
      >
        <Modal.Header className="contract-form__header">
          <p className="contract-form__title-header">Create private conversations</p>
        </Modal.Header>
        <Modal.Body className="contract-form__body">
          <div>
            <label className="contract-form__label" htmlFor="id">
              Enter Name Or Phone Number
            </label>
            <input
              className="contract-form__input contract-form__input--id"
              type="string"
              id="stringSearch"
              name="stringSearch"
              onChange={handleLookForAccount}
            />
          </div>
          <ul className="contract-form__list-search">
            {Array.isArray(dataLookForUser) && dataLookForUser.length > 0 ? (
              dataLookForUser.map((item: any, index: number) => (
                <li
                  key={index}
                  className="contract-form__item-search-detail"
                  onClick={() => handleChooseUserChat(item)}
                >
                  <div className="contract-form__search-item-img">
                    <div className="img-box">
                      <img src={`${UrlHost}${item.urlImgAvatar}`} alt="" />
                    </div>
                  </div>
                  <div className="contract-form__search-item-body">
                    <div className="contract-form__search-item-name">
                      <h5>
                        {item.firstName} {item.lastName}
                      </h5>
                    </div>
                  </div>
                </li>
              ))
            ) : (
              <li className="contract-form__item-not-found">
                <p>Not found</p>
              </li>
            )}
          </ul>
        </Modal.Body>
        <Modal.Footer className="contract-form__footer">
          <Button
            color="gray"
            className="contract-form__btn-option contract-form__btn-option--cancel"
            onClick={() => setOpenModal(false)}
          >
            Exit
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal
        dismissible
        show={openModalGroup}
        onClose={handleCloseModal}
        className="contract-form"
      >
        <Modal.Header className="contract-form__header">
          <p className="contract-form__title-header">Create Group Chat</p>
        </Modal.Header>
        <Modal.Body className="contract-form__body">
          <ul className="contract-form__list-user-group">
            {selectedItemsGroup.map((user, index) => (
              <li className="contract-form__item-user-group">
                <div className="contract-form__item-group-img">
                  <div className="img-box">
                    <img src={`${UrlHost}${user?.urlImgAvatar}`} />
                  </div>
                </div>
                <div
                  onClick={() => handleChooseUserChatGroup(user)}
                  className="contract-form__item-group-btn"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    strokeWidth={1.5}
                    stroke="currentColor"
                    className="w-7 h-7"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M6 18 18 6M6 6l12 12"
                    />
                  </svg>
                </div>
              </li>
            ))}
          </ul>
          <div>
            <label className="contract-form__label" htmlFor="id">
              Search Users
            </label>
            <input
              className="contract-form__input contract-form__input--id"
              type="string"
              id="stringSearch"
              name="stringSearch"
              onChange={handleLookForAccount}
            />
          </div>
          <ul className="contract-form__list-search">
            {Array.isArray(dataLookForUser) && dataLookForUser.length > 0 ? (
              dataLookForUser.map((item: any, index: number) => (
                <li
                  key={index}
                  className="contract-form__item-search-detail"
                  onClick={() => handleChooseUserChatGroup(item)}
                >
                  <div className="contract-form__search-item-img">
                    <div className="img-box">
                      <img src={`${UrlHost}${item.urlImgAvatar}`} alt="" />
                    </div>
                  </div>
                  <div className="contract-form__search-item-body">
                    <div className="contract-form__search-item-name">
                      <h5>
                        {item.firstName} {item.lastName}
                      </h5>
                    </div>
                  </div>
                  {selectedItemsGroup.some((user) => user.id === item.id) ? (
                    <div className="contract-form__item-group-choose">
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={1.5}
                        stroke="currentColor"
                        className="w-8 h-8"
                      >
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
                        />
                      </svg>
                    </div>
                  ) : (
                    <div className="contract-form__item-group-none"></div>
                  )}
                </li>
              ))
            ) : (
              <li className="contract-form__item-not-found">
                <p>Not found</p>
              </li>
            )}
          </ul>
        </Modal.Body>
        <Modal.Footer className="contract-form__footer justify-between">
          <Button
            color="gray"
            className="contract-form__btn-option contract-form__btn-option--cancel"
            onClick={() => cancelCreateGroupChat()}
          >
            Cancel
          </Button>
          {selectedItemsGroup.length >= 2 ? (
            <Button
              className="contract-form__btn-option contract-form__btn-option--send"
              type="button"
              onClick={() => createGroupChat()}
            >
              Create
            </Button>
          ) : null}
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default Chats;
