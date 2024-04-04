// rootReducer.ts
import { combineReducers, createSlice } from '@reduxjs/toolkit';
import userReducer from './clientReducer';
import authReducer, { logIn } from './authReducer';
import messageNotifyReducer from './mesageNotifiReducer';
import dataLoginReducer  from './loginReducer';
import chatsReducer from './Hubs/chatsReducer';
import messageReducer from './Hubs/messageReducer';
import headerChatReducer from './Hubs/headerChatReducer';
import showMessageReducer from './showMessageReducer';
import { LOGOUT } from '../actions/logout';
import friendReducer from './Hubs/friendReducer';
import lookforUserReducer from './Hubs/lookforUserReducer';

const rootReducer = combineReducers({
  client: userReducer,
  auth: authReducer,
  messageNotify: messageNotifyReducer,
  dataLogin: dataLoginReducer,
  chats: chatsReducer,
  messageConversation: messageReducer,
  headerChat:  headerChatReducer,
  showMessage:  showMessageReducer,
  friendUser: friendReducer,
  lookForUser: lookforUserReducer
});
const rootAppReducer = (state: any, action: any) => {
  if (action.type === LOGOUT) {
    state = undefined;
  }

  return rootReducer(state, action);
};

export default rootAppReducer;