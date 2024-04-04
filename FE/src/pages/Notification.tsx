import React, { useState, useEffect } from "react";
import "../styles/tab-content.scss";
import "../styles/button-add.scss";
import "../styles/notification.scss";
import { UrlHost } from "../services/http-common";
import { useDispatch, useSelector } from "react-redux";
import { fetchFriend, Relationship } from "../state/reducer/Hubs/friendReducer";
import { AppDispatch, RootState } from "../state/store/configureStore";
import { responseMakeFriend } from "../Hubs/ConnectionHub";

function Notification() {
  const dispatch: AppDispatch = useDispatch();
  const handleResponseMakeFriend = async (id: string, option: boolean) => {
    // updateRelationship(id);

    let response = await responseMakeFriend(
      {
        FriendId: id,
        IsAccepted: option,
      }
    )
    dispatch(fetchFriend(response.relationships));
  }
  const handleCancelMakeFriend = (id: string) => {
    // cancelMakeFriend(id);
    
  }
  const listRelationship: Relationship[] = useSelector(
    (state: RootState) => state.friendUser.relationships
  );
  return (
    <>
      <div className="tab-content">
        <div className="tab-content__header">
          <div className="tab-content__flex tab-content__flex--header-title">
            <div className="tab-content__title">
              <h4>Notification</h4>
            </div>
          </div>
        </div>
        <div className="notification">
          <ul className="notification__list-item">
            {Array.isArray(listRelationship) && listRelationship && listRelationship
              .filter((relationship) => relationship.status === 3)
              .map((relationship, index) => (
                <li key={index} className="notification__item-detail">
                  <div className="notification__img-wrapper">
                    <img
                      src= {`${UrlHost}${relationship.friend.urlImgAvatar}`}
                      alt=""
                      className="notification__img-detail"
                    />
                  </div>
                  <div className="notification__content-wrapper">
                    <div className="notification__info-detail">
                      <h5 className="notification__name-user">
                        {relationship.friend.firstName}{" "}
                        {relationship.friend.lastName}
                      </h5>
                      <div className="notification__content">
                        <p>send friend request to you</p>
                      </div>
                    </div>
                    <div className="notification__btn-option-wrapper">
                      <button className="notification__btn notification__btn--accept" onClick={() => handleResponseMakeFriend (relationship.friend.id, true)}>
                        <p>Accept</p>
                      </button>
                      <button className="notification__btn notification__btn--reject" onClick={() => handleResponseMakeFriend (relationship.friend.id, false)}>
                        <p>Cancel</p>
                      </button>
                    </div>
                  </div>
                </li>
              ))}
          </ul>
        </div>
      </div>
    </>
  );
}

export default Notification;
