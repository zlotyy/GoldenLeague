import axios from "@/plugins/axios.js";
const API_URL = "matches";

export default {
  GetCurrentGameweek() {
    return axios.get(`${API_URL}/current-gameweek`);
  },
};
