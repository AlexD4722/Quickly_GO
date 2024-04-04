import axios from "axios";
import Cookies from 'js-cookie';
const token = Cookies.get('token');
export let UrlHost = "https://localhost:7094/";
const instance = axios.create({
  baseURL: "https://localhost:7094/api/",
  headers: {
    "Content-type": "application/json",
    "Authorization": `Bearer ${token}`
  }
});

instance.interceptors.request.use(function (config) {
  const token = Cookies.get('token');
  config.headers.Authorization =  token ? `Bearer ${token}` : '';
  return config;
});

export default instance;