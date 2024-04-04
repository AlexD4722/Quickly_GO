import { createSlice, PayloadAction, createAsyncThunk } from "@reduxjs/toolkit";
import { Account } from "../../pages/auth/Auth";
import UserService, { formLogin } from "../../services/userService";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
interface Auth {
  isAuthenticated: boolean;
}

const initialState: Auth = {
  isAuthenticated: false,
};

const authSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
      setFalseAuth: (state) => {
        state.isAuthenticated = false;
      },
  },
  extraReducers: (builder) => {
    // Add reducers for additional action types here, and handle loading state as needed
    builder.addCase(logIn.rejected, (state, action) => {
      state.isAuthenticated = false;
    });
    builder.addCase(logIn.fulfilled, (state, action) => {
      state.isAuthenticated = true;
    });
  },
});

export const logIn = createAsyncThunk("user/logIn", async (data: formLogin) => {
  const response = await UserService.LoginHandler(data);
  return response;
});
export const { setFalseAuth } = authSlice.actions;
// export const { abc } = authSlice.actions;
export default authSlice.reducer;
