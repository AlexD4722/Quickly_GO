import { createSlice, PayloadAction } from "@reduxjs/toolkit";
interface Message {
  id: number;
  CreatorId: string;
  ConversationId: string;
  BodyContent: string;
  // .......
}

interface MessageState {
  messages: Message[];
}

// Here is a list of messages
const initialState : any = {
    messages: [],
};
export const messageSlice = createSlice({
  name: "message",
  initialState,
  reducers: {
    addMessage: (state, action: PayloadAction<any>) => {
      state.messages.push(action.payload);
    },
    getAllMessage : (state, action: PayloadAction<[]>) => {
      state.messages = action.payload;
    },
    getDefaultMessage: (state, action: PayloadAction<[]>) => 
    // Add other reducers here
    {
      state.messages = action.payload;
    }
  },
});

export const { addMessage ,getAllMessage, getDefaultMessage} = messageSlice.actions;
export default messageSlice.reducer;
