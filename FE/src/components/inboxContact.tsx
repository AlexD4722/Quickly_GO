import React, { useState, useEffect } from "react";
import "../styles/inbox-contact.scss";
import imgUser from "../assets/images/avatar-3.jpg";
import { UrlHost } from "../services/http-common";
function InboxContact({ data }: { data: any }) {
  return (
    <>
      <div className="inbox-contact__wrapper">
        <div className="inbox-contact__inbox-user-img">
          <div className="img-box">
            <img
              src={`${UrlHost}${
                data.group ? data.urlImg : data.user[0].urlImgAvatar
              }`}
              alt=""
            />
          </div>
        </div>
        <div className="inbox-contact__inbox-content">
          <div className="inbox-contact__inbox-flex">
            <div className="inbox-contact__inbox-name">
              {data.group ? (
                <p>{data.name}</p>
              ) : (
                <p>
                  {data.user[0].firstName} {data.user[0].lastName}
                </p>
              )}
            </div>
            {/* <div className="inbox-contact__inbox-time">
              <p>11 ago</p>
            </div> */}
          </div>
          <div className="inbox-contact__inbox-flex">
            <div className="inbox-contact__inbox-last-messages">
              <p>{data.lastMessage ? data.lastMessage.bodyContent : ""}</p>
            </div>
            {/* <div className="inbox-contact__inbox-status">
              <p>New</p>
            </div> */}
          </div>
        </div>
      </div>
    </>
  );
}

export default InboxContact;
