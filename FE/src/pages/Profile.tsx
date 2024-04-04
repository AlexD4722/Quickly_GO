import React, { useState, useEffect } from "react";
import "../styles/page-profile.scss";
import "../styles/button-add.scss";
import BoxSearch from "../components/BoxSearch";
import BtnPlusAdd from "../components/btnPlusAdd";
import imgUser from "../assets/images/avatar-1.jpg";
import backgroud from "../assets/images/img-4.jpg";
import { useSelector } from "react-redux";
import { RootState } from "../state/store/configureStore";
import { UrlHost } from "../services/http-common";
function Profile() {
  const { user } = useSelector((state: RootState) => state.client);
  return (
    <>
      {user && (
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
                  <h5>my profile</h5>
                </div>
                <div className="page-profile__btn-dropdown-wrapper">
                  <div className="page-profile__btn-dropdown">
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      width="20"
                      height="20"
                      fill="currentColor"
                      className="bi bi-three-dots-vertical"
                      viewBox="0 0 16 16"
                    >
                      <path d="M9.5 13a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0m0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0m0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0" />
                    </svg>
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
              </div>
              <div className="page-profile__user-name">
                <h5>
                  {user?.firstName} {user?.lastName}
                </h5>
              </div>
            </div>
            <div className="page-profile__info-user-wrapper">
              <div className="page-profile__info-use-description">
                <h5>
                  {user && user?.description
                    ? user?.description
                    : "Please add more information"}
                </h5>
              </div>
              <div className="page-profile__info-use-flex">
                <div className="page-profile__info-use-icon">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="16"
                    height="16"
                    fill="currentColor"
                    className="bi bi-person"
                    viewBox="0 0 16 16"
                  >
                    <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10s-3.516.68-4.168 1.332c-.678.678-.83 1.418-.832 1.664z" />
                  </svg>
                </div>
                <div className="page-profile__info-value">
                  <p>
                    {user?.firstName} {user?.lastName}
                  </p>
                </div>
              </div>
              <div className="page-profile__info-use-flex">
                <div className="page-profile__info-use-icon">
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
                      d="M8.625 12a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0H8.25m4.125 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0H12m4.125 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm0 0h-.375M21 12c0 4.556-4.03 8.25-9 8.25a9.764 9.764 0 0 1-2.555-.337A5.972 5.972 0 0 1 5.41 20.97a5.969 5.969 0 0 1-.474-.065 4.48 4.48 0 0 0 .978-2.025c.09-.457-.133-.901-.467-1.226C3.93 16.178 3 14.189 3 12c0-4.556 4.03-8.25 9-8.25s9 3.694 9 8.25Z"
                    />
                  </svg>
                </div>
                <div className="page-profile__info-value">
                  <p>{user?.email}</p>
                </div>
              </div>
              <div className="page-profile__info-use-flex">
                <div className="page-profile__info-use-icon">
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
                      d="M15 10.5a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    />
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M19.5 10.5c0 7.142-7.5 11.25-7.5 11.25S4.5 17.642 4.5 10.5a7.5 7.5 0 1 1 15 0Z"
                    />
                  </svg>
                </div>
                <div className="page-profile__info-value">
                  <p>{user?.location ?? "Please add more information"}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default Profile;
