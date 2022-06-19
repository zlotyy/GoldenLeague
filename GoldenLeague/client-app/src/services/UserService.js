import axios from "@/plugins/axios.js";
const API_URL = "users";

const userId = "35CE5DF3-CBCD-4A43-9582-A51CBEC26B91";

export default {
  async LogIn(login, password) {
    return axios.post("login", { login, password });
  },
  async Register(login, password) {
    return axios.post(API_URL, { login, password });
  },
  GetBookmakerBets() {
    return axios.get(`${API_URL}/${userId}/bookmaker-bets`);
  },
  UpdateBookmakerBets(items) {
    return axios.patch(`${API_URL}/${userId}/bookmaker-bets`, items);
  },
};
