// slices/userSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
export interface UserLogin {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  role: number;
  urlImgAvatar: string;
  urlBackground: string;
  description: string;
  location: string;
}

interface UserState {
  user: UserLogin | null;
}

const initialState: UserState = {
  user: null,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    fetchUsers: (state, action: PayloadAction<UserLogin>) => {
      state.user = action.payload;
    },
    // addUser: (state, action: PayloadAction<User>) => {
    //   state.users.push(action.payload);
    // },
  },
});

export const { fetchUsers } = userSlice.actions;
export default userSlice.reducer;
