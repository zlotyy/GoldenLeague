import axios from "@/plugins/axios.js";
const API_URL = "users";

export default {
  GetMatchBetting(userId) {
    return axios.get(`${API_URL}/${userId}/match-betting`);
  },
};
