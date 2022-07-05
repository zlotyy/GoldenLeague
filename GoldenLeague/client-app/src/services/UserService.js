import axios from "@/plugins/axios.js";
import store from "../store";
const API_URL = "/users";

export default {
  async LogIn(login, password) {
    return axios.post("login", { login, password });
  },
  async Register(login, password) {
    return axios.post(API_URL, { login, password });
  },
  async RefreshToken(token) {
    return axios.post("login/refresh-token", { token });
  },
  async GetBookmakerLeaguesJoined() {
    return axios.get(
      `${API_URL}/${store.state.user.id}/bookmaker-leagues-joined`
    );
  },
  async BookmakerLeagueJoin(leagueId) {
    return axios.post(
      `${API_URL}/${store.state.user.id}/bookmaker-league-join`,
      {
        userId: store.state.user.id,
        leagueId,
      }
    );
  },
  async BookmakerLeagueLeave(leagueId) {
    return axios.post(
      `${API_URL}/${store.state.user.id}/bookmaker-league-leave`,
      {
        userId: store.state.user.id,
        leagueId,
      }
    );
  },
  async GetBookmakerBets() {
    return axios.get(`${API_URL}/${store.state.user.id}/bookmaker-bets`);
  },
  async UpdateBookmakerBets(items) {
    return axios.patch(
      `${API_URL}/${store.state.user.id}/bookmaker-bets`,
      items
    );
  },
};
