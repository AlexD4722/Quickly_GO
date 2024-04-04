import { useDispatch } from 'react-redux';
import { AppDispatch } from './store/configureStore';
// Import the type of your store's dispatch function

export const useAppDispatch = () => useDispatch<AppDispatch>();