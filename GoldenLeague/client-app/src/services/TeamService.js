import axios from "@/plugins/axios.js";
const API_URL = "teams";

export default {
  GetTeamsRanking() {
    return axios.get(`${API_URL}/ranking`);
  },
};
