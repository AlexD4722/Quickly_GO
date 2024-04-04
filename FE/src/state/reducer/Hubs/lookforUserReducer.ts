import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface UserLookFor {
    id: string;
    firstName: string;
    lastName: string;
    userName: string | null;
    phoneNumber: string;
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
    isFriend: boolean;
    isMakeFriend: boolean;
    isAcceptFriend: boolean;
    isDeclinedFriend: boolean;
  }

interface ListUserState {
    users: UserLookFor[];
}

const initialState: ListUserState = {
    users: [],
};

export const lookForUserSlice = createSlice({
  name: "lookForUser",
  initialState,
  reducers: {
    fetchUserLookFor: (state, action: PayloadAction<UserLookFor[]>) => {
      state.users = action.payload;
    },
  },
  // add extra reducers here
  extraReducers: (builder) => {
  },
});

export const { fetchUserLookFor } = lookForUserSlice.actions;

export default lookForUserSlice.reducer;