import { createSlice, PayloadAction } from "@reduxjs/toolkit";
interface HeaderChatState {
  dataHeaderChat: {
    id: string;
    name: string;
    urlImg: string;
  } | null;
}

const initialState: HeaderChatState = {
  dataHeaderChat: null,
};


export const headerChatSlice = createSlice({
  name: "headerChat",
  initialState,
  reducers: {
    setDataHeaderChat : (state, action: PayloadAction<any>) => {
      state.dataHeaderChat = action.payload;
    }
  },
});

export const { setDataHeaderChat} = headerChatSlice.actions;
export default headerChatSlice.reducer;
