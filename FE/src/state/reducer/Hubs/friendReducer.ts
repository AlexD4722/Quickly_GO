import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface Friend {
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
  notices: string | null;
  createAt: string;
  updateAt: string;
}

export interface Relationship {
  id: number;
  userId: string;
  friendId: string;
  alias: string | null;
  status: number;
  user: string | null;
  friend: Friend;
  createAt: string;
  updateAt: string;
}
interface  RelationshipState {
  relationships: Relationship[];
}    

const initialState: RelationshipState = {
  relationships: [],
};

export const friendSlice = createSlice({
  name: "relationships",
  initialState,
  reducers: {
    fetchFriend: (state, action: PayloadAction<Relationship[]>) => {
      state.relationships = action.payload;
    },
  },
  // add extra reducers here
  extraReducers: (builder) => {
  },
});

export const { fetchFriend } = friendSlice.actions;

export default friendSlice.reducer;