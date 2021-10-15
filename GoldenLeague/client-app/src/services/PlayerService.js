import axios from "@/plugins/axios.js";
const API_URL = "player";

export default {
  GetMatchBettings(playerId) {
    return axios.get(`${API_URL}/${playerId}/match-bettings`);
  },
};
