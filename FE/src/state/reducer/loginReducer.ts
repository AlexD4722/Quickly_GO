import UserService, { formLogin } from '../../services/userService';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
interface AuthLogin{
    userName: string;
    password: string;
}

interface userLoginState {
    userLogin: AuthLogin | null;
}

const initialState: userLoginState = {
    userLogin: null
};
  

export const DataLoginSlice = createSlice({
  name: 'userLoginData',
  initialState,
  reducers: {
    setDataLogin: (state, action: PayloadAction<AuthLogin>) => {
      state.userLogin = action.payload;
    },
  },
});
export const { setDataLogin } = DataLoginSlice.actions;
export default DataLoginSlice.reducer;