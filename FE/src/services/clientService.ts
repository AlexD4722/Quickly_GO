import http from "./http-common";

const GetDataUserInfomationView = async () => {
  return await http.get("Users/ViewInfoUser");
};
const ClientService = {
  GetDataUserInfomationView,
};

export default ClientService;
