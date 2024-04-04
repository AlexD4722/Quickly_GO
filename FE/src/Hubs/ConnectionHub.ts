import { HubConnectionBuilder } from "@microsoft/signalr";
import Cookies from "js-cookie";
import { HubConnectionState } from "@microsoft/signalr";
const hubUrl = "https://localhost:7094/chatHub";
export const connection = new HubConnectionBuilder()
  .withUrl(hubUrl, { accessTokenFactory: () => Cookies.get("token") || "" })
  .build();

export const startConnection = async () => {
  if (connection.state === HubConnectionState.Disconnected) {
    try {
      await connection.start();
      console.log("Connected to the hub.");

      // Handle connection errors
      connection.onclose((err) => {
        if (err) {
          console.error(`Connection closed with error: ${err}`);
        } else {
          console.error("Connection closed");
        }
      });
    } catch (error) {
      console.error("Failed to connect to the hub.", error);
    }
  } else {
    console.warn("Attempted to start an already connected hub.");
  }
};

// const dispatch = useDispatch();
export const sendMessageInConversation = async (data: any) => {
  const response = await connection.invoke("HandleMessage", data);
  return response;

};

export const GetConversationData = async (data: any) => {
  const response = await connection.invoke("GetConversationData", data);
  return response;

};

export const sendAddFriend = async (id: string) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await connection.invoke("SendFriendRequest", id);
    return response;
  }
};

export const requestMessagesInConversation = async (id: string) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await connection.invoke("GetMessageInConversation", id);
    console.log("requestMessagesInConversation", response);
    return response;
  }
};

export const responseMakeFriend = async (data: any) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await  connection.invoke("ResponseFriendRequest", data);
    return response;
  }
};

export const LookForUser = async (key: string) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await connection.invoke("LookForUser", key);
    return response;
  }
};

export const CheckTwoUserInConversation = async (
  itemId: string,
  ConversationId: String
) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await connection.invoke("CheckTwoUserInConversation", itemId, ConversationId);
    return response;
  }
};

export const CreateConversation = async (request: any) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await connection.invoke("CreateConversation", request);
    return response;
  }
};
export const EditProfile = async (request: any) => {
  if (connection.state === HubConnectionState.Connected) {
    const response = await connection.invoke("UpdateUserData", request);
    return response;
  }
}
