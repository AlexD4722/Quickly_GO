// slices/userSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
export interface MessageNotify {
  content: string;
  status: number;
}

// Define the initial state using that type
const initialState: MessageNotify = {
  content: "",
  status: 0,
};

const messageNotifySlice = createSlice({
  name: "messageNotify",
  initialState,
  reducers: {
    activeNotify: (state, action: PayloadAction<MessageNotify>) => {
      state.content = action.payload.content;
      state.status = action.payload.status;
    },
    deActiveNotify: (state) => {
      state.content = "";
      state.status = 0;
    },
  },
});

export const { activeNotify, deActiveNotify } = messageNotifySlice.actions;
export default messageNotifySlice.reducer;
