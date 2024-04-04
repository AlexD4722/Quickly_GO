import React, { useState, useEffect } from "react";
import "../styles/page-profile.scss";
import "../styles/button-add.scss";
import BoxSearch from "../components/BoxSearch";
import BtnPlusAdd from "../components/btnPlusAdd";
import imgUser from "../assets/images/avatar-1.jpg";
import backgroud from "../assets/images/img-4.jpg";
import { Accordion, Button, Label, Modal, TextInput } from "flowbite-react";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { UrlHost } from "../services/http-common";
import UserService from "../services/userService";
import ClientService from "../services/clientService";
import { fetchUsers } from "../state/reducer/clientReducer";
import { EditProfile } from "../Hubs/ConnectionHub";
import { activeNotify, deActiveNotify, MessageNotify } from "../state/reducer/mesageNotifiReducer";

function Setting() {
  const dispatch: AppDispatch = useDispatch();
  const [openModal, setOpenModal] = useState(false);
  const { user } = useSelector((state: RootState) => state.client);
  // handle upload avatar
  const handleUploadAvatar = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const file =
      event.target.files && event.target.files.length > 0
        ? event.target.files[0]
        : null;
    if (!file) {
      return;
    }
    try {
      const response = await UserService.UploadFile(file);
      if (response.status === 200) {
        ClientService.GetDataUserInfomationView()
          .then((response) => {
            dispatch(fetchUsers(response.data));
          })
          .catch((error) => {});
          const messageNotify: MessageNotify = {
            content: "File upload successfully",
            status: 1,
          };
          dispatch(activeNotify(messageNotify));
      } else {
        console.error("File upload failed");
        const messageNotify: MessageNotify = {
          content: "File upload failed",
          status: 0,
        };
        dispatch(activeNotify(messageNotify));
      }
    } catch (error) {
      const messageNotify: MessageNotify = {
        content: "File upload failed",
        status: 0,
      };
      dispatch(activeNotify(messageNotify));
    }
  };
  const UploadFileBackground = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const file =
      event.target.files && event.target.files.length > 0
        ? event.target.files[0]
        : null;
    if (!file) {
      return;
    }
    try {
      const response = await UserService.UploadFileBackground(file);
      if (response.status === 200) {
        ClientService.GetDataUserInfomationView()
          .then((response) => {
            dispatch(fetchUsers(response.data));
          })
          .catch((error) => {});
      } else {
        console.error("File upload failed");
      }
    } catch (error) {
      console.error("An error occurred while uploading the file", error);
    }
  };
  // submit form
  const handleSubmitProfile = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const description = (document.getElementById(
      "descriptionInput"
    ) as HTMLInputElement).value;
    const firstName = (document.getElementById(
      "fisNameUserInput"
    ) as HTMLInputElement).value;
    const lastName = (document.getElementById(
      "lastNameUserInput"
    ) as HTMLInputElement).value;
    const location = (document.getElementById(
      "locationInput"
    ) as HTMLInputElement).value;
    const data = {
      Id: user?.id,
      FirstName: firstName,
      LastName: lastName,
      Location: location,
      Description: description,
    };
    let result = await EditProfile(data);
    if(result){
      setOpenModal(false);
      const messageNotify: MessageNotify = {
        content: "Edited information successfully",
        status: 1,
      };
      dispatch(activeNotify(messageNotify));
      ClientService.GetDataUserInfomationView()
      .then((response) => {
        dispatch(fetchUsers(response.data));
      })
      .catch((error) => {});
    }else{
      const messageNotify: MessageNotify = {
        content: "Editing information failed",
        status: 0,
      };
      dispatch(activeNotify(messageNotify));
    }
  }
  return (
    <>
      <div className="page-profile">
        <div className="page-profile__img-background">
          <img
            src={`${UrlHost}${user?.urlBackground || backgroud}`}
            alt=""
            className="page-profile__img-background-detail"
          />
          <div className="page-profile__header">
            <div className="page-profile__flex page-profile__flex--header-title">
              <div className="page-profile__title">
                <h5>Settings</h5>
              </div>
              <input
                id="fileImgBackgroundUpload"
                type="file"
                style={{ display: "none" }}
                onChange={UploadFileBackground}
              />
              <div className="page-profile__btn-dropdown-wrapper">
                <div className="page-profile__btn-edit-info">
                  <label htmlFor="fileImgBackgroundUpload">
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 24 24"
                      fill="currentColor"
                      className="w-6 h-6"
                    >
                      <path d="M21.731 2.269a2.625 2.625 0 0 0-3.712 0l-1.157 1.157 3.712 3.712 1.157-1.157a2.625 2.625 0 0 0 0-3.712ZM19.513 8.199l-3.712-3.712-12.15 12.15a5.25 5.25 0 0 0-1.32 2.214l-.8 2.685a.75.75 0 0 0 .933.933l2.685-.8a5.25 5.25 0 0 0 2.214-1.32L19.513 8.2Z" />
                    </svg>
                  </label>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="page-profile__body">
          <div className="page-profile__img-avatar-wrapper">
            <div className="page-profile__img-avatar">
              <div className="img-box">
                <img src={`${UrlHost}${user?.urlImgAvatar}`} alt="" />
              </div>
              <div className="page-profile__btn-edit-avatar">
                <label htmlFor="fileImgAvatarUpload">
                  <input
                    id="fileImgAvatarUpload"
                    type="file"
                    style={{ display: "none" }}
                    onChange={handleUploadAvatar}
                  />
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="currentColor"
                    className="w-6 h-6"
                  >
                    <path d="M12 9a3.75 3.75 0 1 0 0 7.5A3.75 3.75 0 0 0 12 9Z" />
                    <path
                      fillRule="evenodd"
                      d="M9.344 3.071a49.52 49.52 0 0 1 5.312 0c.967.052 1.83.585 2.332 1.39l.821 1.317c.24.383.645.643 1.11.71.386.054.77.113 1.152.177 1.432.239 2.429 1.493 2.429 2.909V18a3 3 0 0 1-3 3h-15a3 3 0 0 1-3-3V9.574c0-1.416.997-2.67 2.429-2.909.382-.064.766-.123 1.151-.178a1.56 1.56 0 0 0 1.11-.71l.822-1.315a2.942 2.942 0 0 1 2.332-1.39ZM6.75 12.75a5.25 5.25 0 1 1 10.5 0 5.25 5.25 0 0 1-10.5 0Zm12-1.5a.75.75 0 1 0 0-1.5.75.75 0 0 0 0 1.5Z"
                      clipRule="evenodd"
                    />
                  </svg>
                </label>
              </div>
            </div>
            <div className="page-profile__user-name">
              <h5>{user?.firstName} {user?.lastName}</h5>
            </div>
          </div>
          <Accordion>
            <Accordion.Panel>
              <Accordion.Title>
                <div className="page-profile__collapse-header-name">
                  <div className="page-profile__collapse-name-icon">
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 24 24"
                      fill="currentColor"
                      className="w-6 h-6"
                    >
                      <path
                        fillRule="evenodd"
                        d="M7.5 6a4.5 4.5 0 1 1 9 0 4.5 4.5 0 0 1-9 0ZM3.751 20.105a8.25 8.25 0 0 1 16.498 0 .75.75 0 0 1-.437.695A18.683 18.683 0 0 1 12 22.5c-2.786 0-5.433-.608-7.812-1.7a.75.75 0 0 1-.437-.695Z"
                        clipRule="evenodd"
                      />
                    </svg>
                  </div>
                  <h3>Personal Info</h3>
                </div>
              </Accordion.Title>
              <Accordion.Content>
                <div className="page-profile__accordion-body">
                  <div className="page-profile__accordion-body-item">
                    <p>Description</p>
                    <h5>
                      {user
                        ? user.description
                          ? user.description
                          : "Please update more information"
                        : ""}
                    </h5>
                  </div>
                  <div className="page-profile__accordion-body-item">
                    <p>Name</p>
                    <h5>
                      {user?.firstName} {user?.lastName}
                    </h5>
                  </div>
                  <div className="page-profile__accordion-body-item">
                    <p>Email</p>
                    <h5>{user?.email}</h5>
                  </div>
                  <div className="page-profile__accordion-body-item">
                    <p>Location</p>
                    <h5>
                      {user
                        ? user.location
                          ? user.location
                          : "Please update more information"
                        : ""}
                    </h5>
                  </div>
                  <div
                    onClick={() => setOpenModal(true)}
                    className="page-profile__accordion-btn-edit-info"
                  >
                    <BtnPlusAdd />
                  </div>
                </div>
              </Accordion.Content>
            </Accordion.Panel>
          </Accordion>
          <div
            className="page-profile__list-item-collapse"
            id="accordion-collapse-information"
            data-accordion="collapse"
          ></div>
          <Modal show={openModal} onClose={() => setOpenModal(false)}>
            <Modal.Header>Edit personal information</Modal.Header>
            <form onSubmit={handleSubmitProfile}>
              <Modal.Body>
                <div className="page-profile__edit-input">
                  <div className="mb-2 block">
                    <Label htmlFor="descriptionInput" value="Description" />
                  </div>
                  <textarea
                    className="page-profile__input-detail page-profile__input-detail--text-area"
                    id="descriptionInput"
                    typeof="text"
                    placeholder="Description"
                    rows={2}
                    defaultValue={user?.description}
                  />
                </div>
                <div className="page-profile__edit-input">
                  <div className="mb-2 block">
                    <Label htmlFor="fisNameUserInput" value="Fist Name" />
                  </div>
                  <input
                    className="page-profile__input-detail"
                    id="fisNameUserInput"
                    typeof="text"
                    placeholder="Fist Name"
                    required
                    defaultValue={user?.firstName}
                  />
                </div>
                <div className="page-profile__edit-input">
                  <div className="mb-2 block">
                    <Label htmlFor="lastNameUserInput" value="Last Name" />
                  </div>
                  <input
                    className="page-profile__input-detail"
                    id="lastNameUserInput"
                    typeof="text"
                    placeholder="Last Name"
                    required
                    defaultValue={user?.lastName}
                  />
                </div>
                <div className="page-profile__edit-input">
                  <div className="mb-2 block">
                    <Label htmlFor="locationInput" value="Your Location" />
                  </div>
                  <input
                    className="page-profile__input-detail"
                    id="locationInput"
                    typeof="text"
                    placeholder="Name"
                    defaultValue={user?.location}
                  />
                </div>
              </Modal.Body>
              <Modal.Footer className="flex justify-between ">
                <Button color="gray" onClick={() => setOpenModal(false)}>
                  Cancel
                </Button>
                <Button type = "submit" >Save</Button>
              </Modal.Footer>
            </form>
          </Modal>
        </div>
      </div>
    </>
  );
}

export default Setting;
