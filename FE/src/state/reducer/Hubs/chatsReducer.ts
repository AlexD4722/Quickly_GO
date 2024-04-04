import { createSlice, PayloadAction } from "@reduxjs/toolkit";
export interface User {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string | null;
  email: string | null;
  role: number;
  password: string | null;
  verifyCode: string | null;
  urlImgAvatar: string;
  urlBackground: string;
  description: string | null;
  location: string | null;
  status: number;
  readMessages: string | null;
  userConversations: string | null;
  relationships: string | null;
  friendRelationships: string | null;
  messages: string | null;
  createAt: string;
  updateAt: string;
}

interface LastMessage {
  creatorId: string;
  conversationId: string | null;
  bodyContent: string;
  id: number;
}
export interface Chat {
  id: string;
  name: string;
  urlImg: string;
  description: string;
  group: boolean;
  createAt: string;
  updateAt: string;
  user: User[];
  lastMessage: LastMessage;
  lastReadMessage: string;
}
// Here is a list of conversations
const initialState: { chats: Chat[] } = {
  chats: [],
};

export const chatSlice = createSlice({
  name: "chats",
  initialState,
  reducers: {
    // addConversation: (state, action: PayloadAction<Conversation>) => {
    //   state.conversations.push(action.payload);
    // },
    setChats: (state, action: PayloadAction<Chat[]>) => {
      state.chats = action.payload;
    },
    // Add other reducers here
  },
  // add extra reducers here
  extraReducers: (builder) => {
  },
});

export const { setChats } = chatSlice.actions;

export default chatSlice.reducer;
