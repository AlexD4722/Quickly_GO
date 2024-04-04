// slices/userSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

// Define the initial state using that type
const initialState = {
  status: false
};

const showMessageSlice = createSlice({
  name: "showMessage",
  initialState,
  reducers: {
    activeShowMessage: (state) => {
      state.status = true;
    },
    deactiveShowMessage: (state) => {
      state.status = false;
    },
  },
});

export const { activeShowMessage, deactiveShowMessage } = showMessageSlice.actions;
export default showMessageSlice.reducer;
