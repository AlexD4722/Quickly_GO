import React, { useState, useEffect } from "react";
import "../styles/alert-message.scss";
import { useSelector } from "react-redux";
import { RootState } from "../state/store/configureStore";
import {
  MessageNotify,
  activeNotify,
  deActiveNotify,
} from "../state/reducer/mesageNotifiReducer";
import { useAppDispatch } from "../state/hooks";
export default function AlertMessage() {
  const dispatch = useAppDispatch();
  const content = useSelector(
    (state: RootState) => state.messageNotify.content
  );
  const status = useSelector((state: RootState) => state.messageNotify.status);
  const alertMessage = document.querySelector(".alert-message");
  if (alertMessage && content !== "") {
    (alertMessage as HTMLElement).classList.add("alert-message--effect");
  }
  let button = document.querySelector(".alert-message__button-close");
  if (button) {
    button.addEventListener("click", () => {
      if (alertMessage) {
        (alertMessage as HTMLElement).classList.remove("alert-message--effect");
      }
      dispatch(deActiveNotify());
    });
  }
  return (
    <>
      <div className="alert-message">
        <div className="alert-message__box">
          <h5 className="alert-message__message-detail">{content}</h5>
          {status === 1 ? (
            <div className="alert-message__icon-message alert-message__icon-message--success">
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
                  d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
                />
              </svg>
            </div>
          ) : status === 0 ? (
            <div className="alert-message__icon-message alert-message__icon-message--failed">
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
                  d="m9.75 9.75 4.5 4.5m0-4.5-4.5 4.5M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
                />
              </svg>
            </div>
          ) : (
            <div className="alert-message__icon-message alert-message__icon-message--warning">
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
                  d="M12 9v3.75m9-.75a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 3.75h.008v.008H12v-.008Z"
                />
              </svg>
            </div>
          )}

          <div className="alert-message__button-close">
            <button
              type="button"
              className="alert-message__button-close-detail"
            >
              <p>Close</p>
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
