import axios from "@/plugins/axios.js";
const API_URL = "users";

// TODO GetBookmakerBets i UpdateBookmakerBets - UserId jest już w Store
// const userId = "35CE5DF3-CBCD-4A43-9582-A51CBEC26B91";

export default {
  async LogIn(login, password) {
    return axios.post("login", { login, password });
  },
  async Register(login, password) {
    return axios.post(API_URL, { login, password });
  },
  async GetBookmakerLeaguesJoined(userId) {
    return axios.get(`${API_URL}/${userId}/bookmaker-leagues-joined`);
  },
  async BookmakerLeagueJoin(userId, leagueId) {
    return axios.post(`${API_URL}/${userId}/bookmaker-league-join`, {
      userId,
      leagueId,
    });
  },
  async BookmakerLeagueLeave(userId, leagueId) {
    return axios.post(`${API_URL}/${userId}/bookmaker-league-leave`, {
      userId,
      leagueId,
    });
  },
  GetBookmakerBets() {
    return axios.get(
      `${API_URL}/35CE5DF3-CBCD-4A43-9582-A51CBEC26B91/bookmaker-bets`
    );
  },
  UpdateBookmakerBets(items) {
    return axios.patch(
      `${API_URL}/35CE5DF3-CBCD-4A43-9582-A51CBEC26B91/bookmaker-bets`,
      items
    );
  },
};
