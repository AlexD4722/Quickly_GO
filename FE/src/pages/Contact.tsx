import React, { useState, useEffect } from "react";
import "../styles/tab-content.scss";
import "../styles/button-add.scss";
import "../styles/contract-form.scss";
import BoxSearch from "../components/BoxSearch";
import BtnPlusAdd from "../components/btnPlusAdd";
import { Button, Modal } from "flowbite-react";
import { LookForUser, sendAddFriend } from "../Hubs/ConnectionHub";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { useDispatch, useSelector } from "react-redux";
import {
  activeNotify,
  MessageNotify,
} from "../state/reducer/mesageNotifiReducer";
import { fetchFriend, Relationship } from "../state/reducer/Hubs/friendReducer";
import { UrlHost } from "../services/http-common";
import {
  fetchUserLookFor,
  UserLookFor,
} from "../state/reducer/Hubs/lookforUserReducer";
import Chats from "./Chats";

interface Data {
  message: string;
  success: boolean;
  isFriend: boolean;
  isMakeFriend: boolean;
  isAcceptFriend: boolean;
  isDeclinedFriend: boolean;
}
function Contact() {
  const dispatch: AppDispatch = useDispatch();
  const [keySearch, setKeySearch] = useState<string>("");
  const [openModal, setOpenModal] = useState(false);
  const [keyOnSearch, setKeyOnSearch] = useState<string>("");
  const [idFriend, setIdfFriend] = useState("");
  const [dataFriend, setDataFriend] = useState<any>();
  const friends: Relationship[] = useSelector(
    (state: RootState) => state.friendUser.relationships
  );
  const dataLookFor: UserLookFor[] = useSelector(
    (state: RootState) => state.lookForUser.users
  );

  //handle search data chat
  const [filteredContact, setFilteredContact] = useState(friends);
  const handleSearchChats = (searchTerm: string) => {
    //check if search term is empty
    if (searchTerm === "") {
      setFilteredContact(friends);
    } else {
      //check chat is empty
      if (friends.length === 0) {
        setFilteredContact([]);
      } else {
        setFilteredContact(
          friends.filter((friend) => {
            const fullName =
              friend.friend.firstName + " " + friend.friend.lastName;
            return fullName.toLowerCase().startsWith(searchTerm.toLowerCase());
          })
        );
      }
    }
  };

  const handleAddMakeFriend = async (keyFriend: string) => {
    const messageNotify: MessageNotify = {
      content: "",
      status: 0,
    };
    const data = await sendAddFriend(keyFriend);
    dispatch(fetchFriend(data.dataRelationships));
    if ((data as Data).success === true) {
      messageNotify.content = "Sent a friend request";
      messageNotify.status = 1;
      dispatch(activeNotify(messageNotify));
      getDataLookFor(keySearch);
    } else {
      messageNotify.content = "Adding a friend failed, please re-enter your ID";
      messageNotify.status = 0;
      dispatch(activeNotify(messageNotify));
    }
  };
  const getDataLookFor = async (key: string) => {
    let result: any = await LookForUser(key);
    const friendIds = friends
      .filter((friend) => friend.status === 1)
      .map((friend) => friend.friendId);
    const makeFriendIds = friends
      .filter((friend) => friend.status === 2)
      .map((friend) => friend.friendId);
    const acceptFriendIds = friends
      .filter((friend) => friend.status === 3)
      .map((friend) => friend.friendId);
    const declinedFriendIds = friends
      .filter((friend) => friend.status === 4)
      .map((friend) => friend.friendId);
    if (result && result.success) {
      if (Array.isArray(result.users)) {
        const mappedUsers = result.users.map((user: any) => {
          const isFriend = friendIds.includes(user.id);
          const isMakeFriend = makeFriendIds.includes(user.id);
          const isAcceptFriend = acceptFriendIds.includes(user.id);
          const isDeclinedFriend = declinedFriendIds.includes(user.id);
          return {
            ...user,
            isFriend: isFriend,
            isMakeFriend: isMakeFriend,
            isAcceptFriend: isAcceptFriend,
            isDeclinedFriend: isDeclinedFriend,
          };
        });
        dispatch(fetchUserLookFor(mappedUsers));
        setDataFriend(mappedUsers);
      }
    }
  };

  useEffect(() => {
    getDataLookFor(keySearch);
  }, [keySearch, friends]);
  const handleLookForAccount = (event: any) => {
    setKeySearch(event.target.value);
    setIdfFriend(event.target.value);
  };
  const handleCloseModal = () => {
    setIdfFriend("");
    setOpenModal(false);
  };

  return (
    <>
      <div className="tab-content">
        <div className="tab-content__header">
          <div className="tab-content__flex tab-content__flex--header-title">
            <div className="tab-content__title">
              <h4>Contacts</h4>
            </div>
            <div onClick={() => setOpenModal(true)}>
              <BtnPlusAdd />
            </div>
          </div>
          <BoxSearch onSearch={handleSearchChats} />
        </div>
        <div className="tab-content__body">
          <div className="tab-content__simple-bar tab-content__simple-bar--contract">
            {/* <div className="tab-content__simple-bar-header">
              <div className="tab-content__simple-bar-title">
                <h5>favorites</h5>
              </div>
              <BtnPlusAdd />
            </div> */}
            <div className="tab-content__simple-bar-body">
              <div className="tab-content__contact-list-wrapper">
                <div className="tab-content__contact-title">
                  <p>A</p>
                </div>
                <ul className="tab-content__contact-list-detail">
                  {Array.isArray(filteredContact) && filteredContact.length
                    ? filteredContact
                        .filter((item) => item.status === 1)
                        .map((item: Relationship, index) => (
                          <li key={index} className="tab-content__item-contact">
                            <div className="tab-content__item-wrapper">
                              <div className="tab-content__item-contact-img">
                                <div className="img-box">
                                  <img
                                    src={`${UrlHost}${item.friend.urlImgAvatar}`}
                                    alt=""
                                  />
                                </div>
                              </div>
                              <div className="tab-content__name-user">
                                <p>
                                  {item.friend.firstName} {item.friend.lastName}
                                </p>
                              </div>
                            </div>
                            <button className="tab-content__btn-option">
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
                                  d="M12 6.75a.75.75 0 1 1 0-1.5.75.75 0 0 1 0 1.5ZM12 12.75a.75.75 0 1 1 0-1.5.75.75 0 0 1 0 1.5ZM12 18.75a.75.75 0 1 1 0-1.5.75.75 0 0 1 0 1.5Z"
                                />
                              </svg>
                            </button>
                          </li>
                        ))
                    : "No data"}
                </ul>
              </div>
            </div>
          </div>
          {/* <Button onClick={() => setOpenModal(true)}>Toggle modal</Button> */}
          <Modal
            dismissible
            show={openModal}
            onClose={handleCloseModal}
            className="contract-form"
          >
            <Modal.Header className="contract-form__header">
              <p className="contract-form__title-header">Add friends</p>
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
                {Array.isArray(dataLookFor) && dataLookFor.length > 0 ? (
                  dataLookFor.map((item: any, index: number) => (
                    <li
                      key={index}
                      className="contract-form__item-search-detail"
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
                        <div className="contract-form__search-item-option">
                          {item.isFriend &&
                          item.isMakeFriend === false &&
                          item.isAcceptFriend === false &&
                          item.isDeclinedFriend === false ? (
                            <p className="contract-form__status-friend">
                              Friend
                            </p>
                          ) : item.isFriend === false &&
                            item.isMakeFriend &&
                            item.isAcceptFriend === false &&
                            item.isDeclinedFriend === false ? (
                            <div className="contract-form__btn-add-wrapper">
                              <p className="contract-form__content-btn">
                                Friend sent
                              </p>
                            </div>
                          ) : item.isFriend === false &&
                            item.isMakeFriend === false &&
                            item.isAcceptFriend &&
                            item.isDeclinedFriend === false ? (
                            <div className="contract-form__btn-add-wrapper">
                              <button
                                className="contract-form__btn-add"
                                onClick={() => handleAddMakeFriend(item.id)}
                              >
                                <p className="contract-form__content-btn">
                                  Accept friend request
                                </p>
                              </button>
                            </div>
                          ) : (
                            <div className="contract-form__btn-add-wrapper">
                              <button
                                className="contract-form__btn-add"
                                onClick={() => handleAddMakeFriend(item.id)}
                              >
                                <p className="contract-form__content-btn">
                                  Invite friend
                                </p>
                              </button>
                            </div>
                          )}
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
              {/* <Button
                className="contract-form__btn-option contract-form__btn-option--send"
                type="submit"
              >
                Send
              </Button> */}
            </Modal.Footer>
          </Modal>
        </div>
      </div>
    </>
  );
}

export default Contact;
